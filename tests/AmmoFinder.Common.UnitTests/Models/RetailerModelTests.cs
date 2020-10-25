using AmmoFinder.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AmmoFinder.Common.UnitTests.Models
{
    public class RetailerModelTests
    {
        [Fact]
        public void RetailerId_IsValid()
        {
            // Arrange
            var expected = 1;
            var data = new RetailerModel
            {
                RetailerId = expected
            };

            // Assert
            Assert.Equal(expected, data.RetailerId);
        }

        [Fact]
        public void Name_IsValid()
        {
            // Arrange
            var expected = "test";
            var data = new RetailerModel
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
            var data = new RetailerModel
            {
                CreatedOn = expected
            };

            // Assert
            Assert.Equal(expected, data.CreatedOn);
        }

        [Fact]
        public void Products_IsValid()
        {
            // Arrange
            var data = new RetailerModel
            {
                Products = new List<ProductModel>
                {
                    new ProductModel()
                }
            };

            // Assert
            Assert.True(data.Products.Any());
        }
    }
}
