using AmmoFinder.Common.Extensions;
using System;
using System.IO;
using Xunit;

namespace AmmoFinder.UnitTests.Common
{
    public class ExtensionTests
    {
        [Fact]
        public void GetXmlComments_IsValid()
        {
            // Arrange
            var assembly = typeof(ExtensionTests).Assembly;
            var assemblyName = assembly.GetName().Name;
            var expected = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");

            // Act
            var actual = assembly.GetXmlComments();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
