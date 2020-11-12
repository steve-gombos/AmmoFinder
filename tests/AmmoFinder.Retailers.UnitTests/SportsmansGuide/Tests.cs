using AmmoFinder.Common.Interfaces;
using AmmoFinder.Retailers.SportsmansGuide;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System.IO;
using System.Linq;
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
                .AddSportsmansGuideClient()
                .BuildServiceProvider();

            // Act
            var productService = provider.GetRequiredService<IProductService>();

            // Assert
            Assert.IsType<ProductService>(productService);
            Assert.Equal(RetailerNames.SportsmansGuide, productService.Retailer);
        }

        [Fact]
        public async Task ProductService_Fetch_IsValid()
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
            mockedHttp.When(Extension.BaseUrl + "product/getproductcartdata?AdId=2233527")
                .Respond("text/html", File.OpenRead("SportsmansGuide/product-details.html"));
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
