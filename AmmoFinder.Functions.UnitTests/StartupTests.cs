using AmmoFinder.Common.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AmmoFinder.Functions.UnitTests
{
    public class StartupTests
    {
        [Fact]
        public void Startup_ConfigurationBuilder_IsValid()
        {
            // Arrange
            var startup = new Startup();
            var mockedBuilder = new Mock<IFunctionsConfigurationBuilder>();
            var mockedConfiguration = new ConfigurationBuilder();
            mockedBuilder.Setup(b => b.ConfigurationBuilder).Returns(mockedConfiguration);

            // Act
            startup.ConfigureAppConfiguration(mockedBuilder.Object);

            // Assert
            Assert.True(mockedConfiguration.Sources.Any());
        }

        [Fact]
        public void Startup_Configure_IsValid()
        {
            // Arrange
            var startup = new Startup();
            var mockedBuilder = new Mock<IFunctionsHostBuilder>();
            var services = new ServiceCollection();
            mockedBuilder.Setup(b => b.Services).Returns(services);
            startup.Configure(mockedBuilder.Object);
            var provider = services.BuildServiceProvider();

            // Act
            var productServices = provider.GetRequiredService<IEnumerable<IProductService>>();

            // Assert
            Assert.True(productServices.Count() == 3);
        }
    }
}
