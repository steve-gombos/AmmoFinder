﻿using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
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
        private readonly IBrowsingContext _browsingContext;

        public ProductService(HttpClient httpClient, IMapper mapper, ILogger<ProductService> logger, IBrowsingContext browsingContext) : base(httpClient, mapper, logger, browsingContext)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _logger = logger;
            _browsingContext = browsingContext;
        }

        public override string Retailer => RetailerNames.Cabelas;

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

            var identifiers = identifier.Split("-");
            var productId = identifiers[0];
            var catEntryId = identifiers[1];

            var inventoryData = await GetInventoryData(productId);

            inventoryData.TryGetValue(catEntryId, out var inventory);

            var product = _mapper.Map<Product>(Tuple.Create(document, new MapperData(productId, catEntryId, productUrl, inventory)));

            return product;
        }

        #endregion

        #region Private Methods

        private async Task<IEnumerable<ProductModel>> GetProducts()
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.PostAsync($"BVProductListingView?categoryId=3074457345616967890&resultsPerPage=36&storeId=10651", null);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source));

            var list = document.QuerySelector<IHtmlElement>("ul.grid");
            var productSections = list.QuerySelectorAll<IHtmlListItemElement>("li");

            foreach (var productSection in productSections)
            {
                var jsonString = productSection.Children.ToCollection().FirstOrDefault(x => x.Id.Contains("entitledItem"));
                var attributeData = JsonSerializer.Deserialize<IEnumerable<AttributeData>>(jsonString.Text());

                var productUrl = productSection.QuerySelector<IHtmlDivElement>("div.product_name").QuerySelector<IHtmlAnchorElement>("a").Href;

                var mappedProducts = await GetProductDetails(productUrl, attributeData);

                if (mappedProducts.Any())
                {
                    products.AddRange(mappedProducts);
                }
            }

            return products;
        }

        private async Task<IEnumerable<ProductModel>> GetProductDetails(string url, IEnumerable<AttributeData> attributes)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();
            var document = await _browsingContext.OpenAsync(req => req.Content(source));

            var inventoryData = await GetInventoryData(attributes.First().productId);

            foreach (var attribute in attributes)
            {
                inventoryData.TryGetValue(attribute.catentry_id, out var inventory);

                var product = _mapper.Map<Product>(Tuple.Create(document, new MapperData(attribute.productId, attribute.catentry_id, url, inventory)));

                if (!string.IsNullOrWhiteSpace(product.Name))
                {
                    products.Add(product);
                }
            }

            return products;
        }

        private async Task<Dictionary<string, InventoryData>> GetInventoryData(string productId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "BPSGetOnlineInventoryStatusByIDView")
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("productId", productId),
                    new KeyValuePair<string, string>("storeId", "10651"),
                    new KeyValuePair<string, string>("catalogId", "10551"),
                    new KeyValuePair<string, string>("langId", "-1"),
                })
            };

            httpRequest.Headers.Add("Accept", "application/json");

            var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"StatusCode: {response.StatusCode}");
                return new Dictionary<string, InventoryData>();
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            jsonString = jsonString.Replace("/*", "").Replace("*/", "");
            var inventoryData = JsonSerializer.Deserialize<InventoryWrapper>(jsonString);

            return inventoryData.onlineInventory;
        }

        #endregion
    }
}