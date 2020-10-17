using AmmoFinder.Common.Interfaces;
using AmmoFinder.Common.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmmoFinder.Retailers
{
    public abstract class ProductServiceBase : IProductService
    {
        protected ProductServiceBase(HttpClient httpClient, IMapper mapper)
        {
        }

        public abstract string Retailer { get; }

        public abstract Task<IEnumerable<ProductModel>> Fetch();
    }
}
