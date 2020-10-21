using AmmoFinder.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AmmoFinder.Api.UnitTests.Controllers
{
    public class CommonControllerTests
    {
        [Theory]
        [InlineData(typeof(OkObjectResult))]
        public async Task GetCalibers_ReturnsList(Type type)
        {
            // Arrange
            var controller = new CommonController();

            // Act
            var result = controller.GetCalibers();

            // Assert
            var actionResult = Assert.IsType<ActionResult<Dictionary<string, IEnumerable<string>>>>(result);
            Assert.IsType(type, actionResult.Result);
        }

        [Theory]
        [InlineData(typeof(OkObjectResult))]
        public async Task GetBrands_ReturnsList(Type type)
        {
            // Arrange
            var controller = new CommonController();

            // Act
            var result = controller.GetBrands();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<string>>>(result);
            Assert.IsType(type, actionResult.Result);
        }
    }
}
