using AmmoFinder.Common.Interfaces;
using AmmoFinder.Data;
using AmmoFinder.Persistence.Mappers;
using AmmoFinder.Persistence.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AmmoFinder.Persistence.UnitTests.Services
{
    public class RefreshProductsTests
    {
        [Fact]
        public void RefreshProducts_IsValid()
        {
            // Arrange
            var provider = new ServiceCollection()
                .AddAutoMapper(config =>
                {
                    config.AddProfile<PersistenceMapper>();
                })
                .AddDbContext<ProductsContext>(options =>
                {
                    options.UseInMemoryDatabase(new Guid().ToString());
                    options.ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                })
                .BuildServiceProvider();
            var mockedLogger = new Mock<ILogger<RefreshProducts>>();
            var productServices = new List<IProductService> { new TestProductService() };
            var mapper = provider.GetService<IMapper>();
            var context = provider.GetService<ProductsContext>();
            var refreshProducts = new RefreshProducts(productServices, context, mapper, mockedLogger.Object);
            var expected = 1;

            // Act
            refreshProducts.Refresh();

            // Assert
            Assert.Equal(expected, context.Products.Count());
        }
    }
}
