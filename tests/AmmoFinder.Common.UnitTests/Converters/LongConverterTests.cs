using AmmoFinder.Common.UnitTests.TestData;
using System.Text.Json;
using Xunit;

namespace AmmoFinder.Common.UnitTests.Converters
{
    public class LongConverterTests
    {
        [Fact]
        public void LongConverter_IsValid()
        {
            // Arrange
            var json = "{\"value\": \"123\"}";

            // Act
            var actual = JsonSerializer.Deserialize<LongTestDto>(json);

            // Assert
            Assert.IsType<LongTestDto>(actual);
            Assert.IsType<long>(actual.Value);
        }

        [Theory]
        [InlineData("{\"value\": \"\"}")]
        [InlineData("{\"value\": null}")]
        public void LongConverter_ThrowsException_IsValid(string json)
        {
            // Assert
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<LongTestDto>(json));
        }

        [Theory]
        [InlineData("{\"value\": \"123\"}", true)]
        [InlineData("{\"value\": null}", false)]
        [InlineData("{\"value\": \"\"}", false)]
        [InlineData("{\"value\": \"abc\"}", false)]
        public void NullableLongConverter_IsValid(string json, bool expected)
        {
            // Act
            var actual = JsonSerializer.Deserialize<NullableLongTestDto>(json);

            // Assert
            Assert.IsType<NullableLongTestDto>(actual);
            Assert.Equal(actual.Value.HasValue, expected);
        }
    }
}
