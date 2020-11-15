using AmmoFinder.Common.Interfaces;
using AmmoFinder.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmmoFinder.Persistence.UnitTests.Services
{
    public class TestProductService : IProductService
    {
        public string Retailer => "Test Retailer";

        public async Task<IEnumerable<ProductModel>> GetProductsAsync()
        {
            await Task.CompletedTask;

            return new List<ProductModel>
            {
                new ProductModel
                {
                    Brand = "test",
                    Caliber = "9mm",
                    Casing = "brass",
                    Description = "9mm ammo description",
                    Grain = "120",
                    Inventory = 10,
                    IsAvailable = true,
                    Name = "9mm Ammo",
                    Price = 10.99m,
                    RoundCount = "50",
                    RoundType = "FMJ",
                    RetailerProductId = Retailer,
                    UpdatedOn = DateTime.Now,
                    Url = "https://test.test/test-product"
                }
            };
        }

        public Task<ProductModel> GetProductDetailsAsync(string productUrl)
        {
            throw new NotImplementedException();
        }
    }
}
