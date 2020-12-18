using AmmoFinder.Common.Interfaces;
using AmmoFinder.Retailers.Cabelas;
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

namespace AmmoFinder.Retailers.UnitTests.Cabelas
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
                .AddCabelasClient()
                .BuildServiceProvider();

            // Act
            var productService = provider.GetRequiredService<IProductService>();

            // Assert
            Assert.IsType<ProductService>(productService);
            Assert.Equal(RetailerNames.Cabelas, productService.Retailer);
        }

        [Fact]
        public async Task ProductService_GetProductsAsync_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(Extension.BaseUrl + "BVProductListingView?categoryId=3074457345616967890&resultsPerPage=36&storeId=10651")
                .Respond("text/html", File.OpenRead("Cabelas/products.html"));
            mockedHttp.When("https://www.cabelas.com/shop/en/winchester-usa-9mm-handgun-ammo")
                .Respond("text/html", File.OpenRead("Cabelas/product-details.html"));
            mockedHttp.When("https://www.cabelas.com/shop/BPSGetOnlineInventoryStatusByIDView")
                .Respond("text/html", File.OpenRead("Cabelas/inventory.txt"));
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
        public async Task ProductService_InvalidInventory_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(Extension.BaseUrl + "BVProductListingView?categoryId=3074457345616967890&resultsPerPage=36&storeId=10651")
                .Respond("text/html", File.OpenRead("Cabelas/products.html"));
            mockedHttp.When("https://www.cabelas.com/shop/en/winchester-usa-9mm-handgun-ammo")
                .Respond("text/html", File.OpenRead("Cabelas/product-details.html"));
            mockedHttp.When("https://www.cabelas.com/shop/BPSGetOnlineInventoryStatusByIDView")
                .Respond(HttpStatusCode.NotFound);
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
        public async Task ProductService_GetProductDetailsAsync_IsValid()
        {
            // Arrange
            var productUrl = "https://www.cabelas.com/shop/en/winchester-usa-9mm-handgun-ammo";
            var productIdentifier = "24057-114777";
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(productUrl)
                .Respond("text/html", File.OpenRead("Cabelas/product-details.html"));
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var product = await productService.GetProductDetailsAsync(productUrl, productIdentifier);

            // Assert
            Assert.NotNull(product);
        }

        [Fact]
        public async Task ProductService_GetProductDetailsAsync_NotFound_IsValid()
        {
            // Arrange
            var productUrl = "https://www.cabelas.com/shop/en/winchester-usa-9mm-handgun-ammo";
            var productIdentifier = "24057-114777";
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(productUrl).Respond(HttpStatusCode.NotFound);
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new Uri(Extension.BaseUrl);
            var browsingContext = BrowsingContext.New(Configuration.Default);
            var productService = new ProductService(mockedHttpClient, mapper, mockedLogger.Object, browsingContext);

            // Act
            var product = await productService.GetProductDetailsAsync(productUrl, productIdentifier);

            // Assert
            Assert.Null(product);
        }
    }
}
