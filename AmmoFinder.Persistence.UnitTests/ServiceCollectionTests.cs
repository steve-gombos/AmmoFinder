using AmmoFinder.Common.Interfaces;
using AmmoFinder.Persistence.Mappers;
using AmmoFinder.Persistence.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AmmoFinder.Persistence.UnitTests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void ServiceCollectionExtension_IsValid()
        {
            // Arrange
            var provider = new ServiceCollection()
                .AddAutoMapper(config =>
                {
                    config.AddProfile<PersistenceMapper>();
                })
                .AddProductPersistence(options =>
                {
                    options.UseInMemoryDatabase("Products");
                })
                .AddLogging()
                .BuildServiceProvider();

            // Act
            var refreshProducts = provider.GetService<IRefreshProducts>();
            var productsRepository = provider.GetService<IProductsRepository>();
            var dataSeeder = provider.GetService<IDataSeeder>();

            // Assert
            Assert.IsType<RefreshProducts>(refreshProducts);
            Assert.IsType<ProductsRepository>(productsRepository);
            Assert.IsType<DataSeeder>(dataSeeder);
        }
    }
}
