using AmmoFinder.Parsers.Models;
using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class RoundContainerParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData("Red Army Standard 7.62x39 122grn FMJ Ammunition 20rd box", RoundContainers.Box)]
        [InlineData("Prvi Partizan PPU .308/7.62x51 M80 145grn FMJ 500RD CAN", RoundContainers.Can)]
        [InlineData("12 ga - 2-3/4\" - 1 oz #7.5 Lead Shot - Fiocchi - 250 Round Case", RoundContainers.Case)]
        public void RoundContainer_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new RoundContainerParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
