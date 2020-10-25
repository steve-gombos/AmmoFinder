using AmmoFinder.Common.UnitTests.TestData;
using System.Text.Json;
using Xunit;

namespace AmmoFinder.Common.UnitTests.Converters
{
    public class BoolConverterTests
    {
        [Fact]
        public void BoolConverter_IsValid()
        {
            // Arrange
            var json = "{\"value\": \"true\"}";

            // Act
            var actual = JsonSerializer.Deserialize<BoolTestDto>(json);

            // Assert
            Assert.IsType<BoolTestDto>(actual);
            Assert.IsType<bool>(actual.Value);
        }

        [Theory]
        [InlineData("{\"value\": \"\"}")]
        [InlineData("{\"value\": null}")]
        public void BoolConverter_ThrowsException_IsValid(string json)
        {
            // Assert
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<LongTestDto>(json));
        }
    }
}
