using AmmoFinder.Common.Models;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.BulkAmmo
{
    /// <summary>
    /// https://www.bulkammo.com/handgun?p=1
    /// </summary>
    public class ProductService : ProductServiceBase
    {
        private static List<string> _categories = new List<string>
        {
            "handgun",
            "rimfire",
            "rifle",
            "shotgun",
        };

        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ProductService(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public override string Retailer => RetailerNames.BulkAmmo;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            var products = new List<ProductModel>();

            foreach (var category in _categories)
            {
                var categoryProducts = await FetchProducts(category);

                products.AddRange(categoryProducts);
            }

            return products;
        }

        private async Task<IEnumerable<ProductModel>> FetchProducts(string category)
        {
            var response = await _httpClient.GetAsync($"{category}?limit=all", HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var productSections = document.QuerySelectorAll<IHtmlListItemElement>("li.item");

            var products = new List<ProductModel>();

            foreach (var productSection in productSections)
            {
                var productUrl = productSection.QuerySelector<IHtmlAnchorElement>("a.product-name").Href;
                var details = await GetProductDetails(productUrl);
                var mappedProduct = _mapper.Map<ProductModel>(Tuple.Create(productSection, details));
                
                products.Add(mappedProduct);
            }

            return products;
        }

        private async Task<string> GetProductDetails(string url)
        {
            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
                return string.Empty;

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var details = document.QuerySelector<IHtmlDivElement>("div.std").Text();

            return details;
        }
    }
}
