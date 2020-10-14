using AmmoFinder.Common.Models;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AutoMapper;
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

        public ProductService(HttpClient httpClient, IMapper mapper) : base(httpClient, mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public override string Retailer => RetailerNames.Cabelas;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            var products = await FetchProducts();

            return products;
        }

        private async Task<IEnumerable<ProductModel>> FetchProducts()
        {
            var response = await _httpClient.PostAsync($"BVProductListingView?categoryId=3074457345616967890&resultsPerPage=36&storeId=10651", null);

            response.EnsureSuccessStatusCode();

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            //var productSections = document.QuerySelectorAll<IHtmlDivElement>("div.product");

            var list = document.QuerySelector<IHtmlElement>("ul.grid");
            var productSections = list.QuerySelectorAll<IHtmlListItemElement>("li");


            var products = new List<ProductModel>();

            foreach (var productSection in productSections)
            {
                var jsonString = productSection.Children.ToCollection().Where(x => x.Id.Contains("entitledItem")).FirstOrDefault();
                var attributeData = JsonSerializer.Deserialize<IEnumerable<AttributeData>>(jsonString.Text());
                
                foreach(var attribute in attributeData)
                {
                    var mappedProduct = _mapper.Map<ProductModel>(Tuple.Create(productSection, attribute));
                    products.Add(mappedProduct);
                }
            }

            return products;
        }
    }
}
