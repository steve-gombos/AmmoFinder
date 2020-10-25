using AmmoFinder.Common.Interfaces;
using AmmoFinder.Common.Models;
using AmmoFinder.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AmmoFinder.Persistence.Services
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ProductsContext _productsContext;
        private readonly IMapper _mapper;

        public ProductsRepository(ProductsContext productsContext, IMapper mapper)
        {
            _productsContext = productsContext;
            _mapper = mapper;
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            var products = _productsContext.Products.Include("Retailer").AsEnumerable();

            var mappedProducts = _mapper.Map<IEnumerable<ProductModel>>(products);

            return mappedProducts;
        }

        public IEnumerable<RetailerModel> GetRetailers()
        {
            var retailers = _productsContext.Retailers.Include("Products").AsEnumerable();

            var mappedRetailers = _mapper.Map<IEnumerable<RetailerModel>>(retailers);

            return mappedRetailers;
        }
    }
}
