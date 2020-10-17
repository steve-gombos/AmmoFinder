using AmmoFinder.Common.Interfaces;
using AmmoFinder.Retailers.AimSurplus;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System.IO;
using System.Linq;
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
                .AddAimSurplusClient()
                .BuildServiceProvider();

            // Act
            var productService = provider.GetRequiredService<IProductService>();

            // Assert
            Assert.IsType<ProductService>(productService);
            Assert.Equal(RetailerNames.AimSurplus, productService.Retailer);
        }

        [Fact]
        public async Task ProductService_Fetch_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();
            var mockedHttp = new MockHttpMessageHandler();
            mockedHttp.When(Extension.BaseUrl + "search/?q=null&g=6&category=30&filter=&sort_by=best&page=1&mode=category")
                .Respond("application/json", File.OpenRead("AimSurplus/products-1.json"));
            mockedHttp.When(Extension.BaseUrl + "search/?q=null&g=6&category=30&filter=&sort_by=best&page=2&mode=category")
                .Respond("application/json", File.OpenRead("AimSurplus/products-2.json"));
            mockedHttp.When("https://aimsurplus.com/wolf-7-62x39-123grn-hp-20rd-box/")
                .Respond("text/html", File.OpenRead("AimSurplus/product-detail.html"));
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new System.Uri(Extension.BaseUrl);
            var productService = new ProductService(mockedHttpClient, mapper);

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
            var mockedHttp = new MockHttpMessageHandler();
            var mockedHttpClient = mockedHttp.ToHttpClient();
            mockedHttpClient.BaseAddress = new System.Uri(Extension.BaseUrl);
            var productService = new ProductService(mockedHttpClient, mapper);

            // Act
            var products = await productService.Fetch();

            // Assert
            Assert.True(!products.Any());
        }
    }
}
