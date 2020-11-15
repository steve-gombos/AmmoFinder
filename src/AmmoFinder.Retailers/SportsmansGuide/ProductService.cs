using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Retailers.SportsmansGuide.Models;
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

namespace AmmoFinder.Retailers.SportsmansGuide
{
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

        public override string Retailer => RetailerNames.SportsmansGuide;

        #region Public Methods

        public async override Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            var products = new List<ProductModel>();

            var links = await GetCategoryLinks();

            foreach (var link in links)
            {
                var categoryProducts = await GetProducts(link);

                products.AddRange(categoryProducts);
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

        private async Task<IEnumerable<string>> GetCategoryLinks()
        {
            var links = new List<string>();

            var response = await _httpClient.GetAsync("department/ammo?d=121");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return links;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source).Address(Extension.BaseUrl));

            var listItems = document.QuerySelectorAll<IHtmlDivElement>("div.visual-nav-item");

            foreach (var listItem in listItems)
            {
                var link = listItem.QuerySelector<IHtmlAnchorElement>("a").Href;

                links.Add(link);
            }

            return links;
        }

        private async Task<IEnumerable<ProductModel>> GetProducts(string productsUrl, int page = 1)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync($"{productsUrl}&istko=1&ipp=96&pg={page}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source).Address(Extension.BaseUrl));

            var productList = document.QuerySelector<IHtmlDivElement>("div.products-grid.products-grouping");

            if (productList == null)
            {
                _logger.LogWarning("No Products Found");
                return products;
            }

            var productSections = productList.QuerySelectorAll<IHtmlDivElement>("div.product-tile");
            var lastPage = int.Parse(document.QuerySelector<IHtmlSpanElement>("span.total-pages").Text());

            foreach (var productSection in productSections)
            {
                // Skip over divs that are ads
                if (productSection.ClassList.Contains("double-wide"))
                    continue;

                var productUrl = productSection.QuerySelector<IHtmlAnchorElement>("a.anchor-container").Href;
                var productDetails = await GetProductDetailsAsync(productUrl);

                if (productDetails != null)
                {
                    products.Add(productDetails);
                }
            }

            if (page != lastPage)
            {
                products.AddRange(await GetProducts(productsUrl, page + 1));
            }

            return products;
        }

        #endregion
    }
}
