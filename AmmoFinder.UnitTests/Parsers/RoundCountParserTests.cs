using AmmoFinder.Parsers;
using Xunit;

namespace AmmoFinder.UnitTests.Parsers
{
    public class RoundCountParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData("Red Army Standard 7.62x39 122grn FMJ Ammunition 20rd box", "20")]
        [InlineData("Prvi Partizan PPU .308/7.62x51 M80 145grn FMJ 500RD CAN", "500")]
        [InlineData("PMC 12GA One Shot High Velocity Buckshot 00 9 Pellet 2-3/4 in. 5rd Box", "5")]
        [InlineData("Federal Range Target Practice .45Auto 230grn FMJ (RTP45230) 50rd box", "50")]
        [InlineData("Red Army Standard 7.62x54R FMJ 148grn", null)]
        [InlineData("1000 Rounds of .25 ACP Ammo by Aguila - 50gr FMJ", "1000")]
        [InlineData("Box of 1000 of .25 ACP Ammo by Aguila - 50gr FMJ", "1000")]
        public void RoundCount_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new RoundCountParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
