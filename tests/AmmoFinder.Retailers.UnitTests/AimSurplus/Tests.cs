using AmmoFinder.Common.Interfaces;
using AmmoFinder.Retailers.AimSurplus;
using AngleSharp;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.Retailers.UnitTests.AimSurplus
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
                .AddAimSurplusClient()
                .BuildServiceProvider();

            // Act
            var productService = provider.GetRequiredService<IProductService>();

            // Assert
            Assert.IsType<ProductService>(productService);
            Assert.Equal(RetailerNames.AimSurplus, productService.Retailer);
        }

        [Fact]
        public async Task ProductService_GetProductsAsync_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(Extension.BaseUrl + "search/?q=null&g=6&category=30&filter=&sort_by=best&page=1&mode=category")
                .Respond("application/json", File.OpenRead("AimSurplus/products-1.json"));
            mockedHttp.When(Extension.BaseUrl + "search/?q=null&g=6&category=30&filter=&sort_by=best&page=2&mode=category")
                .Respond("application/json", File.OpenRead("AimSurplus/products-2.json"));
            mockedHttp.When("https://aimsurplus.com/wolf-7-62x39-123grn-hp-20rd-box/")
                .Respond("text/html", File.OpenRead("AimSurplus/product-details.html"));
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var products = await productService.GetProductsAsync();

            // Assert
            Assert.True(products.Any());
        }

        [Fact]
        public async Task ProductService_GetProductsAsync_NoProducts_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var products = await productService.GetProductsAsync();

            // Assert
            Assert.True(!products.Any());
        }

        [Fact]
        public async Task ProductService_GetProductDetailsAsync_IsValid()
        {
            // Arrange
            var productUrl = "https://aimsurplus.com/wolf-7-62x39-123grn-hp-20rd-box/";
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(productUrl)
                .Respond("text/html", File.OpenRead("AimSurplus/product-details.html"));
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var product = await productService.GetProductDetailsAsync(productUrl);

            // Assert
            Assert.NotNull(product);
        }

        [Fact]
        public async Task ProductService_GetProductDetailsAsync_NotFound_IsValid()
        {
            // Arrange
            var productUrl = "https://aimsurplus.com/wolf-7-62x39-123grn-hp-20rd-box/";
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(productUrl).Respond(HttpStatusCode.NotFound);
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var product = await productService.GetProductDetailsAsync(productUrl);

            // Assert
            Assert.Null(product);
        }
    }
}
