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
using System.Reflection;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.SportsmansGuide
{
    /// <summary>
    /// LuckGunner only shows "in stock" ammo, so everything collected here should be in stock
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

        public override string Retailer => RetailerNames.SportsmansGuide;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var products = new List<ProductModel>();

            var links = await GetCategoryLinks();

            foreach (var link in links)
            {
                var categoryProducts = await GetProducts(link);

                products.AddRange(categoryProducts);
            }

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}; Product Count: {products.Count()}");

            return products;
        }

        private async Task<IEnumerable<string>> GetCategoryLinks()
        {
            var links = new List<string>();

            var response = await _httpClient.GetAsync("department/ammo?d=121");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return links;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source).Address(Extension.BaseUrl));

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

            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var response = await _httpClient.GetAsync($"{productsUrl}&istko=1&ipp=96&pg={page}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source).Address(Extension.BaseUrl));

            var productList = document.QuerySelector<IHtmlDivElement>("div.products-grid.products-grouping");

            if (productList == null)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; No Products for {productsUrl}");
                return products;
            }

            var productSections = productList.QuerySelectorAll<IHtmlDivElement>("div.product-tile");
            var lastPage = int.Parse(document.QuerySelector<IHtmlSpanElement>("span.total-pages").Text());

            foreach (var productSection in productSections)
            {
                var productUrl = productSection.QuerySelector<IHtmlDivElement>("div.btn-quickview").QuerySelector<IHtmlAnchorElement>("a").Href;
                var product = await GetProductDetails(productUrl);

                if (product != null)
                {
                    products.Add(product);
                }
            }

            if(page != lastPage)
            {
                products.AddRange(await GetProducts(productsUrl, page + 1));
            }

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}");

            return products;
        }

        private async Task<ProductModel> GetProductDetails(string productUrl)
        {
            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var response = await _httpClient.GetAsync(productUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return null;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source).Address(Extension.BaseUrl));

            var product = _mapper.Map<Product>(Tuple.Create(document, productUrl));

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}");

            return product;
        }
    }
}
