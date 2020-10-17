using AmmoFinder.Common.Interfaces;
using AmmoFinder.Testing.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
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
    }
}
