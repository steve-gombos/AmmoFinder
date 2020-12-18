using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Retailers.AimSurplus.Models;
using AngleSharp;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.AimSurplus
{
    /// <summary>
    /// https://web.aimsurplus.com/search/?q=null&g=6&category=30&filter=&sort_by=best&page=1&mode=category
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

        public override string Retailer => RetailerNames.AimSurplus;

        #region Public Methods

        public override async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = await GetProducts();

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

        private async Task<IEnumerable<ProductModel>> GetProducts(int page = 1)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync($"search/?q=null&g=6&category=30&filter=&sort_by=best&page={page}&mode=category");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return products;
            }

            var result = await response.Content.ReadFromJsonAsync<Response>();

            foreach (var product in result.Products)
            {
                var productDetails = await GetProductDetailsAsync(product.Url.ToString());

                if (productDetails != null)
                {
                    products.Add(productDetails);
                }
            }

            if (page != result.Pages)
            {
                products.AddRange(await GetProducts(page + 1));
            }

            return products;
        }

        #endregion
    }
}
