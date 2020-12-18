using AmmoFinder.Common.Models;
using AmmoFinder.Data;
using AmmoFinder.Data.Models;
using AmmoFinder.Persistence.Mappers;
using AmmoFinder.Persistence.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AmmoFinder.Persistence.UnitTests.Services
{
    public class ProductsRepositoryTests
    {
        public ProductsContext GetContextWithInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ProductsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new ProductsContext(options);
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
            var context = GetContextWithInMemoryDb();
            context.Retailers.AddRange(expected);
            context.SaveChanges();
            var productsRepository = new ProductsRepository(context, mapper);

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
            var retailer = new Retailer
            {
                Name = "Test Retailer 1"
            };
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
                    RetailerProductId = "product1",
                    RetailerId = 1,
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
                    RetailerProductId = "product2",
                    RetailerId = 1,
                    UpdatedOn = DateTime.Now,
                    Url = "https://test.test/test-product"
                }
            };
            var mapper = CreateMapper();
            var context = GetContextWithInMemoryDb();
            context.Retailers.Add(retailer);
            context.SaveChanges();
            context.Products.AddRange(expected);
            context.SaveChanges();
            var productsRepository = new ProductsRepository(context, mapper);

            // Act
            var actual = productsRepository.GetProducts();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<ProductModel>>(actual);
            Assert.Equal(expected.Count(), actual.Count());
        }
    }
}
