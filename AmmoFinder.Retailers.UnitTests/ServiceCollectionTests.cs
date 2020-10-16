using AmmoFinder.Common.Interfaces;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace AmmoFinder.Retailers.UnitTests
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
                    config.AddMaps(new List<Assembly> { typeof(ProductServiceBase).Assembly });
                })
                .AddRetailers()
                .BuildServiceProvider();

            // Act
            var productServices = provider.GetRequiredService<IEnumerable<IProductService>>();

            // Assert
            Assert.True(productServices.Count() == 3);
        }
    }
}
