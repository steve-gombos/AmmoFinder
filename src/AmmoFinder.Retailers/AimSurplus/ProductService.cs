using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Retailers.AimSurplus.Models;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
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

        public ProductService(HttpClient httpClient, IMapper mapper, ILogger<ProductService> logger) : base(httpClient, mapper, logger)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
        }

        public override string Retailer => RetailerNames.AimSurplus;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            var products = await GetProducts();

            _logger.LogInformation($"Product Count: {products.DistinctProducts().Count()}");

            return products.DistinctProducts();
        }

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
                var details = await GetProductDetails(product.Url.ToString());
                var mappedProduct = _mapper.Map<ProductModel>(Tuple.Create(product, details));

                products.Add(mappedProduct);
            }

            if (page != result.Pages)
            {
                products.AddRange(await GetProducts(page + 1));
            }

            return products;
        }

        private async Task<string> GetProductDetails(string url)
        {
            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return string.Empty;
            }

            var source = await response.Content.ReadAsStringAsync();
            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var details = document.QuerySelector<IHtmlElement>("section.item-specs").Text();

            return details;
        }
    }
}
