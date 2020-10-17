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
    public class RetailersControllerTests
    {
        [Theory, ClassData(typeof(RetailersTestData))]
        public async Task GetRetailers_ReturnsList(IEnumerable<RetailerModel> expected, Type type)
        {
            // Arrange
            var mockedProductsRepository = new Mock<IProductsRepository>();
            mockedProductsRepository.Setup(r => r.GetRetailers()).Returns(expected);
            var controller = new RetailersController(mockedProductsRepository.Object);

            // Act
            var result = controller.GetRetailers();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<RetailerModel>>>(result);
            Assert.IsType(type, actionResult.Result);
        }
    }
}
