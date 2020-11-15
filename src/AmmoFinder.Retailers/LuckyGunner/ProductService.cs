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
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.LuckyGunner
{
    /// <summary>
    /// LuckGunner only shows "in stock" ammo, so everything collected here should be in stock
    /// </summary>
    public class ProductService : ProductServiceBase
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly IBrowsingContext _browsingContext;

        public ProductService(HttpClient httpClient, IMapper mapper, ILogger<ProductService> logger, IBrowsingContext browsingContext) : base(httpClient, mapper, logger, browsingContext)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
            _browsingContext = browsingContext;
        }

        public override string Retailer => RetailerNames.LuckyGunner;

        #region Public Methods

        public override async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = new List<ProductModel>();

            var categories = await GetCategories();

            foreach (var category in categories)
            {
                foreach (var link in category.Value)
                {
                    var categoryProducts = await GetProducts(link);

                    products.AddRange(categoryProducts);
                }
            }

            _logger.LogInformation($"Product Count: {products.DistinctProducts().Count()}");

            return products.DistinctProducts();
        }

        public override async Task<ProductModel> GetProductDetailsAsync(string productUrl)
        {
            var response = await _httpClient.GetAsync(productUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return null;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source).Address(Extension.BaseUrl));

            var product = _mapper.Map<Product>(Tuple.Create(document, productUrl));

            return product;
        }

        #endregion

        #region Private Methods

        private async Task<Dictionary<string, IEnumerable<string>>> GetCategories()
        {
            var categories = new Dictionary<string, IEnumerable<string>>();

            var response = await _httpClient.GetAsync("");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return categories;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source));

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

        private async Task<IEnumerable<ProductModel>> GetProducts(string productsUrl)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync($"{productsUrl}?limit=all", HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source));

            var productList = document.QuerySelector<IHtmlOrderedListElement>("ol.products-list");

            if (productList == null)
            {
                _logger.LogWarning("No Products Found");
                return products;
            }
            var productSections = productList.QuerySelectorAll<IHtmlListItemElement>("li.item");

            foreach (var productSection in productSections)
            {
                var productUrl = productSection.QuerySelector("h3.product-name").QuerySelector<IHtmlAnchorElement>("a").Href;
                var productDetails = await GetProductDetailsAsync(productUrl);

                if(productDetails != null)
                {
                    products.Add(productDetails);
                }

                //var mappedProduct = _mapper.Map<Product>(productSection);

                //products.Add(mappedProduct);
            }

            return products;
        }

        #endregion
    }
}
