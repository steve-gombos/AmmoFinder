using AmmoFinder.Common.Interfaces;
using AmmoFinder.Retailers.AmmoDotCom;
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

namespace AmmoFinder.Retailers.UnitTests.AmmoDotCom
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
                .AddAmmoDotComClient()
                .BuildServiceProvider();

            // Act
            var productService = provider.GetRequiredService<IProductService>();

            // Assert
            Assert.IsType<ProductService>(productService);
            Assert.Equal(RetailerNames.AmmoDotCom, productService.Retailer);
        }

        [Fact]
        public async Task ProductService_GetProductsAsync_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When("https://ammo.com/handgun")
                .Respond("text/html", File.OpenRead("AmmoDotCom/category.html"));
            mockedHttp.When("https://ammo.com/handgun/9mm-ammo?limit=all")
                .Respond("text/html", File.OpenRead("AmmoDotCom/products.html"));
            mockedHttp.When("https://ammo.com/handgun/380-acp-ammo?limit=all")
                .Respond("text/html", File.OpenRead("AmmoDotCom/products-empty.html"));
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
            var productUrl = "https://ammo.com/handgun/45-auto-ammo#federal-45-auto-ammo-50-rounds-230-grain-fmj-45-auto-ammunition-from-federal-23248";
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(productUrl)
                .Respond("text/html", File.OpenRead("AmmoDotCom/product-details.html"));
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
            var productUrl = "https://ammo.com/handgun/45-auto-ammo#federal-45-auto-ammo-50-rounds-230-grain-fmj-45-auto-ammunition-from-federal-23248";
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

        [Fact]
        public async Task ProductService_GetProductDetailsAsync_EmptyList_IsValid()
        {
            // Arrange
            var productUrl = "https://ammo.com/handgun/45-auto-ammo#federal-45-auto-ammo-50-rounds-230-grain-fmj-45-auto-ammunition-from-federal-23248";
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(productUrl)
                .Respond("text/html", File.OpenRead("AmmoDotCom/product-details-empty-list.html"));
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var product = await productService.GetProductDetailsAsync(productUrl);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public async Task ProductService_GetProductDetailsAsync_NoList_IsValid()
        {
            // Arrange
            var productUrl = "https://ammo.com/handgun/45-auto-ammo#federal-45-auto-ammo-50-rounds-230-grain-fmj-45-auto-ammunition-from-federal-23248";
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(productUrl)
                .Respond("text/html", File.OpenRead("AmmoDotCom/product-details-no-list.html"));
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
