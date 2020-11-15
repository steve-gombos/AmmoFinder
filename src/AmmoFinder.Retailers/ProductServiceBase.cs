using AmmoFinder.Common.Interfaces;
using AmmoFinder.Common.Models;
using AngleSharp;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers
{
    public abstract class ProductServiceBase : IProductService
    {
        protected ProductServiceBase(HttpClient httpClient, IMapper mapper, ILogger logger, IBrowsingContext browsingContext)
        {
        }

        public abstract string Retailer { get; }

        public abstract Task<IEnumerable<ProductModel>> GetProductsAsync();

        public abstract Task<ProductModel> GetProductDetailsAsync(string productUrl);
    }
}
