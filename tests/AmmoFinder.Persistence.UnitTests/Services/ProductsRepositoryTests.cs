using AmmoFinder.Common.Models;
using AmmoFinder.Data.Models;
using AmmoFinder.Persistence.Mappers;
using AmmoFinder.Persistence.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.Persistence.UnitTests.Services
{
    public class ProductsRepositoryTests : SqlLiteContext
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }

        private IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<PersistenceMapper>();
            });

            return new Mapper(mapperConfiguration);
        }

        [Fact]
        public void GetRetailers_IsValid()
        {
            // Arrange
            var expected = new List<Retailer>
            {
                new Retailer
                {
                    Name = "Test Retailer 1"
                },
                new Retailer
                {
                    Name = "Test Retailer 2"
                },
            };
            var mapper = CreateMapper();
            DbContext.Retailers.AddRange(expected);
            DbContext.SaveChanges();
            var productsRepository = new ProductsRepository(DbContext, mapper);

            // Act
            var actual = productsRepository.GetRetailers();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<RetailerModel>>(actual);
            Assert.Equal(expected.Count(), actual.Count());
        }

        [Fact]
        public void GetProducts_IsValid()
        {
            // Arrange
            var expected = new List<Product>
            {
                new Product
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
                    RoundContainer = "Box",
                    RetailerProductId = "Test Retailer 1",
                    UpdatedOn = DateTime.Now,
                    Url = "https://test.test/test-product"
                },
                new Product
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
                    RoundContainer = "Box",
                    RetailerProductId = "Test Retailer 2",
                    UpdatedOn = DateTime.Now,
                    Url = "https://test.test/test-product"
                }
            };
            var mapper = CreateMapper();
            DbContext.Products.AddRange(expected);
            DbContext.SaveChanges();
            var productsRepository = new ProductsRepository(DbContext, mapper);

            // Act
            var actual = productsRepository.GetProducts();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<ProductModel>>(actual);
            Assert.Equal(expected.Count(), actual.Count());
        }
    }
}
