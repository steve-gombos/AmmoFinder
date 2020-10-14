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
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ProductServiceBase(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public abstract string Retailer { get; }

        public abstract Task<IEnumerable<ProductModel>> Fetch();
    }
}
