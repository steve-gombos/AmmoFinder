using AmmoFinder.Common.Interfaces;
using AmmoFinder.Retailers.SportsmansGuide;
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

namespace AmmoFinder.Retailers.UnitTests.SportsmansGuide
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
                .AddSportsmansGuideClient()
                .BuildServiceProvider();

            // Act
            var productService = provider.GetRequiredService<IProductService>();

            // Assert
            Assert.IsType<ProductService>(productService);
            Assert.Equal(RetailerNames.SportsmansGuide, productService.Retailer);
        }

        [Fact]
        public async Task ProductService_GetProductsAsync_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(Extension.BaseUrl + "department/ammo?d=121")
                .Respond("text/html", File.OpenRead("SportsmansGuide/category.html"));
            mockedHttp.When(Extension.BaseUrl + "productlist/ammo/handgun-pistol-ammo?d=121&c=95&istko=1&ipp=96&pg=1")
                .Respond("text/html", File.OpenRead("SportsmansGuide/products.html"));
            mockedHttp.When(Extension.BaseUrl + "productlist/ammo/handgun-pistol-ammo?d=121&c=95&istko=1&ipp=96&pg=2")
                .Respond("text/html", File.OpenRead("SportsmansGuide/products.html"));
            mockedHttp.When(Extension.BaseUrl + "productlist/ammo/rimfire-ammo?d=121&c=417&istko=1&ipp=96&pg=1")
                .Respond("text/html", File.OpenRead("SportsmansGuide/products-empty.html"));
            mockedHttp.When(Extension.BaseUrl + "product/index/tulammo-762x54mmr-fmj-148-grain-20-rounds?a=2233527")
                .Respond("text/html", File.OpenRead("SportsmansGuide/product-details.html"));
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new System.Uri(Extension.BaseUrl);
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
            mockedHttpClient.BaseAddress = new System.Uri(Extension.BaseUrl);
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
            var productUrl = Extension.BaseUrl + "product/getproductcartdata?AdId=2233527";
            var mapper = CreateMapper();
            var mockedLogger = new Mock<ILogger<ProductService>>();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(productUrl)
                .Respond("text/html", File.OpenRead("SportsmansGuide/product-details.html"));
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
            var productUrl = Extension.BaseUrl + "product/getproductcartdata?AdId=2233527";
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
