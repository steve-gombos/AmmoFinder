using AmmoFinder.Parsers.Models;
using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class ExtensionTests
    {
        [Theory]
        [InlineData("brass", Casings.Brass)]
        public void CasingExtension_IsValid(string input, string expected)
        {
            // Arrange

            // Act
            var actual = input.GetCasing();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("100 rd", "100")]
        public void RoundCountExtension_IsValid(string input, string expected)
        {
            // Arrange

            // Act
            var actual = input.GetRoundCount();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("test 148grn", "148")]
        public void GrainExtension_IsValid(string input, string expected)
        {
            // Arrange

            // Act
            var actual = input.GetGrain();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("9mm", "9mm")]
        public void CaliberExtension_IsValid(string input, string expected)
        {
            // Arrange

            // Act
            var actual = input.GetCaliber();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("FMJ", "FMJ")]
        public void BulletTypeExtension_IsValid(string input, string expected)
        {
            // Arrange

            // Act
            var actual = input.GetBulletType();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("federal", Brands.Federal)]
        public void BrandExtension_IsValid(string input, string expected)
        {
            // Arrange

            // Act
            var actual = input.GetBrand();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
