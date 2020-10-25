using AmmoFinder.Common.UnitTests.TestData;
using System.Text.Json;
using Xunit;

namespace AmmoFinder.Common.UnitTests.Converters
{
    public class BoolConverterTests
    {
        [Theory]
        [InlineData("{\"value\": \"true\"}")]
        [InlineData("{\"value\": true}")]
        public void BoolConverter_Read_IsValid(string json)
        {
            // Act
            var actual = JsonSerializer.Deserialize<BoolTestDto>(json);

            // Assert
            Assert.IsType<BoolTestDto>(actual);
            Assert.IsType<bool>(actual.Value);
        }

        [Theory]
        [InlineData("{\"value\": \"\"}")]
        [InlineData("{\"value\": null}")]
        public void BoolConverter_Read_ThrowsException_IsValid(string json)
        {
            // Assert
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<LongTestDto>(json));
        }

        [Fact]
        public void BoolConverter_Write_IsValid()
        {
            // Arrange
            var expected = "{\"value\":\"true\"}";
            var obj = new BoolTestDto() { Value = true };

            // Act
            var actual = JsonSerializer.Serialize(obj);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
