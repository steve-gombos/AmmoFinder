using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Retailers.Academy.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.Academy
{
    public class ProductService : ProductServiceBase
    {
        private readonly List<string> _categories = new List<string>
        {
            "15808", //Handgun
            //"15807", //Rifle
            //"15806", //Shotgun
            //"15809", //Rimfire
            //"3074457345616934684", //subsonic
            //"202538", //bulk
        };
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        private const int PAGE_SIZE = 2000;

        public ProductService(HttpClient httpClient, IMapper mapper, ILogger<ProductService> logger) : base(httpClient, mapper, logger)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
        }

        public override string Retailer => RetailerNames.Academy;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            var products = new List<ProductModel>();

            foreach(var category in _categories)
            {
                var categoryProducts = await GetProducts(category);

                products.AddRange(categoryProducts);
            }
            
            _logger.LogInformation($"Product Count: {products.DistinctProducts().Count()}");

            return products.DistinctProducts();
        }

        private async Task<IEnumerable<ProductModel>> GetProducts(string category)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync($"api/search/?displayFacets=true&facets=ads_f49001_ntk_cs%3A%22Y%22&orderBy=7&categoryId={category}&pageSize={PAGE_SIZE}&pageNumber=1");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return products;
            }

            var result = await response.Content.ReadFromJsonAsync<ProductsResponse>();

            foreach (var product in result.Products)
            {
                var productDetails = await GetProductDetails(product.Id);

                if(productDetails != null)
                {
                    products.Add(productDetails);
                }
            }

            return products;
        }

        private async Task<ProductModel> GetProductDetails(long productId)
        {
            var response = await _httpClient.GetAsync($"api/product/{productId}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return null;
            }

            var productResponse = await response.Content.ReadFromJsonAsync<ProductResponse>();

            var mappedProduct = _mapper.Map<ProductModel>(productResponse);

            return mappedProduct;
        }
    }
}
