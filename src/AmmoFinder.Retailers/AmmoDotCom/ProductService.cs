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
using System.Reflection;
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

        public ProductService(HttpClient httpClient, IMapper mapper, ILogger<ProductService> logger) : base(httpClient, mapper, logger)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
        }

        public override string Retailer => RetailerNames.AmmoDotCom;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var products = new List<ProductModel>();

            foreach (var category in _categories)
            {
                var links = await GetCaliberLinks(category);

                foreach(var link in links)
                {
                    var linkProducts = await FetchProducts(link);

                    if (linkProducts.Any())
                    {
                        products.AddRange(linkProducts);
                    }
                }
            }

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}; Product Count: {products.Count()}");

            return products;
        }

        private async Task<IEnumerable<string>> GetCaliberLinks(string category)
        {
            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var calibers = new List<string>();

            var response = await _httpClient.GetAsync(category);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return calibers;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var listItems = document.QuerySelectorAll<IHtmlListItemElement>("li.content-box__subcat-wrapper");

            foreach (var listItem in listItems)
            {
                var link = listItem.QuerySelector<IHtmlAnchorElement>("a.content-box__subcat-link").Href;

                calibers.Add(link);
            }            

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}");

            return calibers;
        }

        private async Task<IEnumerable<ProductModel>> FetchProducts(string productsUrl)
        {
            var products = new List<ProductModel>();

            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var response = await _httpClient.GetAsync($"{productsUrl}?limit=all");

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
