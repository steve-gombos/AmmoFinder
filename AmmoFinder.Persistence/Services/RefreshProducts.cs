using AmmoFinder.Common.Interfaces;
using AmmoFinder.Data;
using AmmoFinder.Data.Models;
using AutoMapper;
using EFCore.BulkExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmmoFinder.Persistence.Services
{
    public class RefreshProducts : IRefreshProducts
    {
        private readonly IEnumerable<IProductService> _productServices;
        private readonly ProductsContext _productsContext;
        private readonly IMapper _mapper;

        public RefreshProducts(IEnumerable<IProductService> productServices, ProductsContext productsContext, IMapper mapper)
        {
            _productServices = productServices;
            _productsContext = productsContext;
            _mapper = mapper;
        }

        public async Task Refresh()
        {
            foreach (var productService in _productServices)
            {
                var products = await productService.Fetch();

                var dbProducts = _mapper.Map<IEnumerable<Product>>(products);

                var retailer = GetOrSetRetailer(productService.Retailer);

                foreach (var product in dbProducts)
                {
                    product.Retailer = retailer;
                }

                using (var transaction = _productsContext.Database.BeginTransaction())
                {
                    _productsContext.Products.Where(p => p.Retailer.Id == retailer.Id).BatchDelete();
                    _productsContext.BulkInsert(dbProducts.ToList());

                    _productsContext.SaveChanges();

                    transaction.Commit();
                }
            }
        }

        private Retailer GetOrSetRetailer(string retailerName)
        {
            var retailer = _productsContext.Retailers.FirstOrDefault(r => r.Name == retailerName);

            if (retailer == null)
            {
                var test = _productsContext.Retailers.Add(new Retailer
                {
                    Name = retailerName
                });

                _productsContext.SaveChanges();

                retailer = test.Entity;
            }

            return retailer;
        }
    }
}
