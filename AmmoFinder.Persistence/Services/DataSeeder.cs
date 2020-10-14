using AmmoFinder.Common.Interfaces;
using AmmoFinder.Data;
using AmmoFinder.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmmoFinder.Persistence.Services
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ProductsContext _productsContext;
        private readonly IEnumerable<IProductService> _productServices;

        public DataSeeder(ProductsContext productsContext, IEnumerable<IProductService> productServices)
        {
            _productsContext = productsContext;
            _productServices = productServices;
        }

        public async Task Seed()
        {
            await SeedRetailers();
        }

        private async Task SeedRetailers()
        {
            var persistedRetailers = _productsContext.Retailers.ToList();
            var additionalRetailers = new List<Retailer>();

            foreach (var productService in _productServices)
            {
                if (!persistedRetailers.Any(r => r.Name == productService.Retailer))
                {
                    additionalRetailers.Add(new Retailer
                    {
                        Name = productService.Retailer
                    });
                }
            }

            if (additionalRetailers.Any())
            {
                _productsContext.Retailers.AddRange(additionalRetailers);

                _productsContext.SaveChanges();
            }
        }
    }
}
