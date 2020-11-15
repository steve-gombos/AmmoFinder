using AmmoFinder.Common.Models;
using System;
using Xunit;

namespace AmmoFinder.Common.UnitTests.Models
{
    public class ProductModelTests
    {
        [Fact]
        public void ProductId_IsValid()
        {
            // Arrange
            var expected = 1;
            var data = new ProductModel
            {
                ProductId = expected
            };

            // Assert
            Assert.Equal(expected, data.ProductId);
        }

        [Fact]
        public void Name_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                Name = expected
            };

            // Assert
            Assert.Equal(expected, data.Name);
        }

        [Fact]
        public void CreatedOn_IsValid()
        {
            // Arrange
            var expected = DateTime.Now;
            var data = new ProductModel
            {
                UpdatedOn = expected
            };

            // Assert
            Assert.Equal(expected, data.UpdatedOn);
        }

        [Fact]
        public void Brand_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                Brand = expected
            };

            // Assert
            Assert.Equal(expected, data.Brand);
        }

        [Fact]
        public void Caliber_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                Caliber = expected
            };

            // Assert
            Assert.Equal(expected, data.Caliber);
        }

        [Fact]
        public void Casing_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                Casing = expected
            };

            // Assert
            Assert.Equal(expected, data.Casing);
        }

        [Fact]
        public void Description_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                Description = expected
            };

            // Assert
            Assert.Equal(expected, data.Description);
        }

        [Fact]
        public void Grain_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                Grain = expected
            };

            // Assert
            Assert.Equal(expected, data.Grain);
        }

        [Fact]
        public void Inventory_IsValid()
        {
            // Arrange
            var expected = 1;
            var data = new ProductModel
            {
                Inventory = expected
            };

            // Assert
            Assert.Equal(expected, data.Inventory);
        }

        [Fact]
        public void IsAvailable_IsValid()
        {
            // Arrange
            var expected = true;
            var data = new ProductModel
            {
                IsAvailable = expected
            };

            // Assert
            Assert.Equal(expected, data.IsAvailable);
        }

        [Fact]
        public void Price_IsValid()
        {
            // Arrange
            var expected = 1.99m;
            var data = new ProductModel
            {
                Price = expected
            };

            // Assert
            Assert.Equal(expected, data.Price);
        }

        [Fact]
        public void RetailerId_IsValid()
        {
            // Arrange
            var expected = 1;
            var data = new ProductModel
            {
                RetailerId = expected
            };

            // Assert
            Assert.Equal(expected, data.RetailerId);
        }

        [Fact]
        public void Retailer_IsValid()
        {
            // Arrange
            var data = new ProductModel
            {
                Retailer = new RetailerModel()
            };

            // Assert
            Assert.IsType<RetailerModel>(data.Retailer);
        }

        [Fact]
        public void RetailerProductId_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                RetailerProductId = expected
            };

            // Assert
            Assert.Equal(expected, data.RetailerProductId);
        }

        [Fact]
        public void RoundCount_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                RoundCount = expected
            };

            // Assert
            Assert.Equal(expected, data.RoundCount);
        }

        [Fact]
        public void RoundType_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                RoundType = expected
            };

            // Assert
            Assert.Equal(expected, data.RoundType);
        }

        [Fact]
        public void Url_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new ProductModel
            {
                Url = expected
            };

            // Assert
            Assert.Equal(expected, data.Url);
        }
    }
}
