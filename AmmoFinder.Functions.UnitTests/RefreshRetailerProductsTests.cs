using AmmoFinder.Common.Interfaces;
using AmmoFinder.Testing.Extensions;
using AngleSharp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace AmmoFinder.Functions.UnitTests
{
    public class RefreshRetailerProductsTests
    {
        [Fact]
        public void Function_Log_IsValid()
        {
            // Arrange
            var mockedLogger = new Mock<ILogger<RefreshRetailerProducts>>();
            var mockedRefreshProducts = new Mock<IRefreshProducts>();
            var refreshRetailerProductsFunction = new RefreshRetailerProducts(mockedRefreshProducts.Object);

            // Act
            refreshRetailerProductsFunction.Run(null, mockedLogger.Object);

            // Assert
            mockedLogger.VerifyLog(LogLevel.Information, "C# Timer trigger function executed.", Times.Once());
        }

        [Fact]
        public void Function_CatchesException()
        {
            // Arrange
            var mockedLogger = new Mock<ILogger<RefreshRetailerProducts>>();
            var mockedRefreshProducts = new Mock<IRefreshProducts>();
            mockedRefreshProducts.Setup(rf => rf.Refresh()).Throws(new Exception("Error"));
            var refreshRetailerProductsFunction = new RefreshRetailerProducts(mockedRefreshProducts.Object);

            // Act
            refreshRetailerProductsFunction.Run(null, mockedLogger.Object);

            // Assert
            mockedLogger.VerifyLog(LogLevel.Error, "Error", Times.Once());
        }
    }
}
