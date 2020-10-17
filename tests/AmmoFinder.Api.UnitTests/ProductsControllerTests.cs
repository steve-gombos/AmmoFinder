using AmmoFinder.Api.Controllers;
using AmmoFinder.Common.Interfaces;
using AmmoFinder.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.Api.UnitTests
{
    public class ProductsControllerTests
    {
        [Theory, ClassData(typeof(ProductsTestData))]
        public async Task GetProducts_ReturnsList(IEnumerable<ProductModel> expected, Type type)
        {
            // Arrange
            var mockedProductsRepository = new Mock<IProductsRepository>();
            mockedProductsRepository.Setup(r => r.GetProducts()).Returns(expected);
            var controller = new ProductsController(mockedProductsRepository.Object);

            // Act
            var result = controller.GetProducts();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ProductModel>>>(result);
            Assert.IsType(type, actionResult.Result);
        }
    }
}
