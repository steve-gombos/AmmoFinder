using AmmoFinder.Common.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace AmmoFinder.Common.UnitTests.Models
{
    public class ModelTests
    {
        [Fact]
        public void ProductModel_IsValid()
        {
            // Arrange
            var data = new ProductModel
            {
                Brand = "test",
                Caliber = "test",
                Casing = "test",
                Description = "test",
                Grain = "test",
                Inventory = 0,
                IsAvailable = false,
                Name = "test",
                Price = 1.99m,
                ProductId = 1,
                Retailer = new RetailerModel
                {
                    CreatedOn = DateTime.Now,
                    Name = "test",
                    RetailerId = 1
                },
                RetailerId = 1,
                RetailerProductId = "test",
                RoundContainer = "test",
                RoundCount = "test",
                RoundType = "test",
                UpdatedOn = DateTime.Now,
                Url = "test"
            };

            // Act

            // Assert
            Assert.IsType<ProductModel>(data);
        }

        [Fact]
        public void RetailerModel_IsValid()
        {
            // Arrange
            var data = new RetailerModel
            {
                CreatedOn = DateTime.Now,
                Name = "test",
                RetailerId = 1,
                Products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Brand = "test",
                        Caliber = "test",
                        Casing = "test",
                        Description = "test",
                        Grain = "test",
                        Inventory = 0,
                        IsAvailable = false,
                        Name = "test",
                        Price = 1.99m,
                        ProductId = 1,
                        RetailerId = 1,
                        RetailerProductId = "test",
                        RoundContainer = "test",
                        RoundCount = "test",
                        RoundType = "test",
                        UpdatedOn = DateTime.Now,
                        Url = "test"
                    }
                }
            };

            // Act
            var test1 = data.RetailerId;
            var test2 = data.Name;
            var test3 = data.CreatedOn;
            var test4 = data.Products;

            // Assert
            Assert.IsType<RetailerModel>(data);
        }
    }
}
