using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Retailers.LuckyGunner.Models;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.LuckyGunner
{
    public class ProductService : ProductServiceBase
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(HttpClient httpClient, IMapper mapper, ILogger<ProductService> logger) : base(httpClient, mapper, logger)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
        }

        public override string Retailer => RetailerNames.LuckyGunner;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var products = new List<ProductModel>();

            var categories = await GetCategories();

            foreach (var category in categories)
            {
                foreach (var link in category.Value)
                {
                    var categoryProducts = await FetchProducts(link);

                    products.AddRange(categoryProducts);
                }
            }

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}; Product Count: {products.Count()}");

            return products;
        }

        private async Task<Dictionary<string, IEnumerable<string>>> GetCategories()
        {
            var categories = new Dictionary<string, IEnumerable<string>>();

            var response = await _httpClient.GetAsync("");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return categories;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var listItems = document.QuerySelectorAll<IHtmlListItemElement>("li.dropdown.subcat");

            foreach (var listItem in listItems)
            {
                var type = listItem.QuerySelector<IHtmlAnchorElement>("a.category-header").Title;

                if (!type.Contains("ammo", StringComparison.CurrentCultureIgnoreCase))
                    continue;

                var links = listItem.QuerySelectorAll<IHtmlAnchorElement>("a")
                    .Where(a => !string.IsNullOrWhiteSpace(a.Title) && !string.IsNullOrWhiteSpace(a.Href))
                    .Select(a => a.Href);

                categories.Add(type, links);
            }

            return categories;
        }

        private async Task<IEnumerable<ProductModel>> FetchProducts(string productsUrl)
        {
            var products = new List<ProductModel>();

            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var response = await _httpClient.GetAsync($"{productsUrl}?limit=all", HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var productList = document.QuerySelector<IHtmlOrderedListElement>("ol.products-list");

            if (productList == null)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; No Products for {productsUrl}");
                return products;
            }
            var productSections = productList.QuerySelectorAll<IHtmlListItemElement>("li.item");

            foreach (var productSection in productSections)
            {
                var mappedProduct = _mapper.Map<Product>(productSection);

                products.Add(mappedProduct);
            }

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}");

            return products;
        }
    }
}
