using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Retailers.BulkAmmo.Models;
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
        private readonly ILogger<ProductService> _logger;
        private readonly IBrowsingContext _browsingContext;

        public ProductService(HttpClient httpClient, IMapper mapper, ILogger<ProductService> logger, IBrowsingContext browsingContext) : base(httpClient, mapper, logger, browsingContext)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
            _browsingContext = browsingContext;
        }

        public override string Retailer => RetailerNames.BulkAmmo;

        #region Public Methods

        public override async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = new List<ProductModel>();

            //TODO: Implement dynamic categories from base URL and parsing DOM.
            foreach (var category in _categories)
            {
                var categoryProducts = await GetProducts(category);

                products.AddRange(categoryProducts);
            }

            _logger.LogInformation($"Product Count: {products.DistinctProducts().Count()}");

            return products.DistinctProducts();
        }

        public override async Task<ProductModel> GetProductDetailsAsync(string productUrl, string identifier = null)
        {
            var response = await _httpClient.GetAsync(productUrl, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return null;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source));

            var product = _mapper.Map<Product>(Tuple.Create(document, productUrl));

            return product;
        }

        #endregion

        #region Private Methods

        private async Task<IEnumerable<ProductModel>> GetProducts(string category)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync($"{category}?limit=all", HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source));

            var productSections = document.QuerySelectorAll<IHtmlListItemElement>("li.item");

            foreach (var productSection in productSections)
            {
                var productUrl = productSection.QuerySelector<IHtmlAnchorElement>("a.product-name").Href;
                var productDetails = await GetProductDetailsAsync(productUrl);

                if (productDetails != null)
                {
                    products.Add(productDetails);
                }
            }

            return products;
        }

        #endregion
    }
}
