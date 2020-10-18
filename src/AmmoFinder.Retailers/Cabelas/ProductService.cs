using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Parsers;
using AmmoFinder.Retailers.Cabelas.Models;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers.Cabelas
{
    /// <summary>
    /// https://www.cabelas.com/shop/BVProductListingView?categoryId=3074457345616967890&resultsPerPage=36&storeId=10651
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

        public override string Retailer => RetailerNames.Cabelas;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var products = await FetchProducts();

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}; Product Count: {products.Count()}");

            return products;
        }

        private async Task<IEnumerable<ProductModel>> FetchProducts()
        {
            var products = new List<ProductModel>();
            var response = await _httpClient.PostAsync($"BVProductListingView?categoryId=3074457345616967890&resultsPerPage=36&storeId=10651", null);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var list = document.QuerySelector<IHtmlElement>("ul.grid");
            var productSections = list.QuerySelectorAll<IHtmlListItemElement>("li");

            foreach (var productSection in productSections)
            {
                var jsonString = productSection.Children.ToCollection().FirstOrDefault(x => x.Id.Contains("entitledItem"));
                var attributeData = JsonSerializer.Deserialize<IEnumerable<AttributeData>>(jsonString.Text());

                foreach (var attribute in attributeData)
                {
                    //var productUrl = productSection.QuerySelector<IHtmlDivElement>("div.product_name").QuerySelector<IHtmlAnchorElement>("a").Href;
                    //var productDetail = await GetProductDetails(productUrl, attribute);
                    var mappedProduct = _mapper.Map<ProductModel>(Tuple.Create(productSection, attribute));
                    products.Add(mappedProduct);
                }
            }

            return products;
        }

        //private async Task<ProductModel> GetProductDetails(string url, AttributeData attributes)
        //{
        //    _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

        //    var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
        //        return new ProductModel();
        //    }

        //    var source = await response.Content.ReadAsStringAsync();

        //    var context = BrowsingContext.New(Configuration.Default);
        //    var document = await context.OpenAsync(req => req.Content(source));

        //    var mainDiv = document.QuerySelector<IHtmlDivElement>($"div#WC_Sku_List_Row_Content_{attributes.catentry_id}");

        //    var caliber = mainDiv.QuerySelector<IHtmlDivElement>("div.CartridgeorGauge").Text().GetCaliber();
        //    var grain = mainDiv.QuerySelector<IHtmlDivElement>("div.Grain").Text().GetGrain();

        //    _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}");

        //    return new ProductModel();
        //}
    }
}
