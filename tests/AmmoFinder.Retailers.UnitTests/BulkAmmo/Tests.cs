using AmmoFinder.Common.Interfaces;
using AmmoFinder.Retailers.BulkAmmo;
using AngleSharp;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.Retailers.UnitTests.BulkAmmo
{
    public class Tests
    {
        private IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<MapProfile>();
            });

            return new Mapper(mapperConfiguration);
        }

        [Fact]
        public void AutoMapperProfile_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();

            // Act

            // Assert
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void ServiceCollectionExtension_IsValid()
        {
            // Arrange
            var provider = new ServiceCollection()
                .AddAutoMapper(config =>
                {
                    config.AddProfile<MapProfile>();
                })
                .AddAngleSharp()
                .AddBulkAmmoClient()
                .BuildServiceProvider();

            // Act
            var productService = provider.GetRequiredService<IProductService>();

            // Assert
            Assert.IsType<ProductService>(productService);
            Assert.Equal(RetailerNames.BulkAmmo, productService.Retailer);
        }

        [Fact]
        public async Task ProductService_Fetch_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(Extension.BaseUrl + "handgun?limit=all")
                .Respond("text/html", File.OpenRead("BulkAmmo/products.html"));
            mockedHttp.When(Extension.BaseUrl + "rimfire?limit=all")
                .Respond("text/html", File.OpenRead("BulkAmmo/products.html"));
            mockedHttp.When("https://www.bulkammo.com/bulk-32s-w-long-ammo-32swlong98lrnpp-50")
                .Respond("text/html", File.OpenRead("BulkAmmo/product-detail.html"));
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new System.Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var products = await productService.Fetch();

            // Assert
            Assert.True(products.Any());
        }

        [Fact]
        public async Task ProductService_Fetch_NoProducts_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new System.Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var products = await productService.Fetch();

            // Assert
            Assert.True(!products.Any());
        }
    }
}
