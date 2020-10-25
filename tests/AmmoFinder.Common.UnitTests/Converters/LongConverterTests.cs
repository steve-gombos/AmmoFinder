using AmmoFinder.Common.UnitTests.TestData;
using System.Text.Json;
using Xunit;

namespace AmmoFinder.Common.UnitTests.Converters
{
    public class LongConverterTests
    {
        [Theory]
        [InlineData("{\"value\": \"123\"}")]
        [InlineData("{\"value\": 123}")]
        public void LongConverter_Read_IsValid(string json)
        {
            // Act
            var actual = JsonSerializer.Deserialize<LongTestDto>(json);

            // Assert
            Assert.IsType<LongTestDto>(actual);
            Assert.IsType<long>(actual.Value);
        }

        [Theory]
        [InlineData("{\"value\": \"\"}")]
        [InlineData("{\"value\": null}")]
        public void LongConverter_Read_ThrowsException_IsValid(string json)
        {
            // Assert
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<LongTestDto>(json));
        }

        [Fact]
        public void LongConverter_Write_IsValid()
        {
            // Arrange
            var expected = "{\"value\":\"123\"}";
            var obj = new LongTestDto() { Value = 123 };

            // Act
            var actual = JsonSerializer.Serialize(obj);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("{\"value\": \"123\"}", true)]
        [InlineData("{\"value\": 123}", true)]
        [InlineData("{\"value\": null}", false)]
        [InlineData("{\"value\": \"\"}", false)]
        [InlineData("{\"value\": \"abc\"}", false)]
        public void NullableLongConverter_Read_IsValid(string json, bool expected)
        {
            // Act
            var actual = JsonSerializer.Deserialize<NullableLongTestDto>(json);

            // Assert
            Assert.IsType<NullableLongTestDto>(actual);
            Assert.Equal(actual.Value.HasValue, expected);
        }
    }
}
