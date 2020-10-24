using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Models;
using AmmoFinder.Retailers.PalmettoStateArmory.Models;
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

namespace AmmoFinder.Retailers.PalmettoStateArmory
{
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

        public override string Retailer => RetailerNames.PalmettoStateArmory;

        public async override Task<IEnumerable<ProductModel>> Fetch()
        {
            _logger.LogInformation($"Started: {MethodBase.GetCurrentMethod().GetName()}");

            var products = await FetchProducts(1);

            _logger.LogInformation($"Completed: {MethodBase.GetCurrentMethod().GetName()}; Product Count: {products.Count()}");

            return products;
        }

        private async Task<IEnumerable<ProductModel>> FetchProducts(int page)
        {
            var products = new List<ProductModel>();

            var response = await _httpClient.GetAsync($"ammo.html?p={page}stock_filter=Show+Only+In+Stock");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Warning: {MethodBase.GetCurrentMethod().GetName()}; StatusCode: {response.StatusCode}");
                return products;
            }

            var source = await response.Content.ReadAsStringAsync();

            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(req => req.Content(source));

            var productSections = document.QuerySelectorAll<IHtmlListItemElement>("li.product-item");
            var lastPage = int.Parse(document.QuerySelector<IHtmlAnchorElement>("a.page.last").LastChild.Text());

            foreach (var productSection in productSections)
            {
                var mappedProduct = _mapper.Map<Product>(productSection);

                products.Add(mappedProduct);
            }

            if (page != lastPage)
            {
                products.AddRange(await FetchProducts(page + 1));
            }

            return products;
        }
    }
}
