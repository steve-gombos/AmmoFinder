using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Retailers.AmmoDotCom.Models;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.AmmoDotCom
{
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

        public override string Retailer => RetailerNames.AmmoDotCom;

        #region Public Methods

        public override async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = new List<ProductModel>();

            foreach (var category in _categories)
            {
                var links = await GetCaliberLinks(category);

                foreach (var link in links)
                {
                    var linkProducts = await GetProducts(link);

                    if (linkProducts.Any())
                    {
                        products.AddRange(linkProducts);
                    }
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
            var document = await _browsingContext.OpenAsync(req => req.Content(source));

            var productList = document.QuerySelector<IHtmlOrderedListElement>("ol.products-list");

            if (productList == null)
            {
                _logger.LogWarning("No Product Found");
                return null;
            }

            var productSection = productList.QuerySelectorAll<IHtmlListItemElement>("li.item").FirstOrDefault();

            if (productSection == null)
            {
                _logger.LogWarning("No Product Found");
                return null;
            }

            var product = _mapper.Map<Product>(productSection);

            return product;
        }

        #endregion

        #region Private Methods

        private async Task<IEnumerable<string>> GetCaliberLinks(string category)
        {
            var calibers = new List<string>();

            var response = await _httpClient.GetAsync(category);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return calibers;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source));

            var listItems = document.QuerySelectorAll<IHtmlListItemElement>("li.content-box__subcat-wrapper");

            foreach (var listItem in listItems)
            {
                var link = listItem.QuerySelector<IHtmlAnchorElement>("a.content-box__subcat-link").Href;

                calibers.Add(link);
            }

            return calibers;
        }

        private async Task<IEnumerable<ProductModel>> GetProducts(string productsUrl)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync($"{productsUrl}?limit=all");

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
                var mappedProduct = _mapper.Map<Product>(productSection);

                products.Add(mappedProduct);
            }

            return products;
        }

        #endregion
    }
}