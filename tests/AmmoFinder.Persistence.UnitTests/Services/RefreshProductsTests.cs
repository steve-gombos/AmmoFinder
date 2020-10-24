using AmmoFinder.Common.Interfaces;
using AmmoFinder.Persistence.Mappers;
using AmmoFinder.Persistence.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.Persistence.UnitTests.Services
{
    public class RefreshProductsTests : SqlLiteContext
    {

        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }

        [Fact]
        public void RefreshProducts_IsValid()
        {
            // Arrange
            var provider = new ServiceCollection()
                .AddAutoMapper(config =>
                {
                    config.AddProfile<PersistenceMapper>();
                })
                .BuildServiceProvider();
            var mockedLogger = new Mock<ILogger<RefreshProducts>>();
            var productServices = new List<IProductService>{ new TestProductService() };
            var mapper = provider.GetService<IMapper>();
            var refreshProducts = new RefreshProducts(productServices, DbContext, mapper, mockedLogger.Object);
            var expected = 1;

            // Act
            refreshProducts.Refresh();

            // Assert
            Assert.Equal(expected, DbContext.Products.Count());
        }
    }
}
