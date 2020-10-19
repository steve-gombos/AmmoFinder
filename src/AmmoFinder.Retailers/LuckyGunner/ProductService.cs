using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
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

            //foreach (var category in _categories)
            //{
            //    var categoryProducts = await FetchProducts(category);

            //    products.AddRange(categoryProducts);
            //}

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

            //li.dropdown.subcat

            var listItems = document.QuerySelectorAll<IHtmlListItemElement>("li.dropdown.subcat");

            foreach(var listItem in listItems)
            {
                var calibers = listItem.QuerySelectorAll<IHtmlAnchorElement>("a");
            }

            return categories;
        }

        private async Task<IEnumerable<ProductModel>> FetchProducts(string category)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync($"{category}?limit=all", HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var productSections = document.QuerySelectorAll<IHtmlListItemElement>("li.item");

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
            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return string.Empty;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var details = document.QuerySelector<IHtmlDivElement>("div.std").Text();

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}");

            return details;
        }
    }
}
