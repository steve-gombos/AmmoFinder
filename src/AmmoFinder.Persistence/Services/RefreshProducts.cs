using AmmoFinder.Common.Interfaces;
using AmmoFinder.Data;
using AmmoFinder.Data.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly ILogger<RefreshProducts> _logger;

        public RefreshProducts(IEnumerable<IProductService> productServices, ProductsContext productsContext, IMapper mapper, ILogger<RefreshProducts> logger)
        {
            _productServices = productServices;
            _productsContext = productsContext;
            _mapper = mapper;
            _logger = logger;
        }

        public void Refresh()
        {
            _logger.LogInformation($"Started product refresh for all retailers");

            var tasks = new List<Task>();

            foreach (var productService in _productServices)
            {
                tasks.Add(Refresh(productService));
            }

            Task.WaitAll(tasks.ToArray());

            _logger.LogInformation($"Completed product refresh for all retailers");
        }

        private async Task Refresh(IProductService productService)
        {
            _logger.LogInformation($"Started Refresh Products; Retailer: {productService.Retailer}");

            var products = await productService.GetProductsAsync();

            var dbProducts = _mapper.Map<IEnumerable<Product>>(products);

            var retailer = GetOrSetRetailer(productService.Retailer);

            foreach (var product in dbProducts)
            {
                product.Retailer = retailer;
            }

            using (var transaction = _productsContext.Database.BeginTransaction())
            {
                var deleteProducts = _productsContext.Products.Where(p => p.Retailer.RetailerId == retailer.RetailerId);
                _productsContext.RemoveRange(deleteProducts);
                _productsContext.AddRange(dbProducts);
                _productsContext.SaveChanges();

                transaction.Commit();
            }

            _logger.LogInformation($"Completed Refresh Products; Retailer: {productService.Retailer}");
        }

        private Retailer GetOrSetRetailer(string retailerName)
        {
            _logger.LogInformation($"Started {nameof(GetOrSetRetailer)}; Retailer: {retailerName}");

            var retailer = _productsContext.Retailers.FirstOrDefault(r => r.Name == retailerName);

            if (retailer == null)
            {
                var test = _productsContext.Retailers.Add(new Retailer
                {
                    Name = retailerName,
                    CreatedOn = DateTime.Now
                });

                _productsContext.SaveChanges();

                retailer = test.Entity;

                _logger.LogInformation($"Added retailer: {retailerName}");
            }

            _logger.LogInformation($"Completed {nameof(GetOrSetRetailer)}; Retailer: {retailerName}");

            return retailer;
        }
    }
}
