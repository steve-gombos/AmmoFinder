using AmmoFinder.Common.Interfaces;
using AmmoFinder.Retailers.Academy;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.Retailers.UnitTests.Academy
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
                .AddAcademyClient()
                .BuildServiceProvider();

            // Act
            var productService = provider.GetRequiredService<IProductService>();

            // Assert
            Assert.IsType<ProductService>(productService);
            Assert.Equal(RetailerNames.Academy, productService.Retailer);
        }

        [Fact]
        public async Task ProductService_Fetch_IsValid()
        {
            // Arrange
            var pageSize = 2000;
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(Extension.BaseUrl + $"api/search/?displayFacets=true&facets=ads_f49001_ntk_cs%3A%22Y%22&orderBy=7&categoryId=15808&pageSize={pageSize}&pageNumber=1")
                .Respond("application/json", File.OpenRead("Academy/products.json"));
            mockedHttp.When(Extension.BaseUrl + "api/product/1197837")
                .Respond("application/json", File.OpenRead("Academy/product-details-out-of-stock.json"));
            mockedHttp.When(Extension.BaseUrl + "api/product/4961501")
                .Respond("application/json", File.OpenRead("Academy/product-details-in-stock.json"));
            mockedHttp.When(Extension.BaseUrl + "api/product/6893269")
                .Respond("application/json", File.OpenRead("Academy/product-details-1.json"));
            mockedHttp.When(Extension.BaseUrl + "api/product/1234567")
                .Respond("application/json", File.OpenRead("Academy/product-details-2.json"));
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new System.Uri(Extension.BaseUrl);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object);

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
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object);

            // Act
            var products = await productService.Fetch();

            // Assert
            Assert.True(!products.Any());
        }
    }
}
