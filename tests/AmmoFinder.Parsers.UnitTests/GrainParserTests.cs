using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class GrainParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData("Invalid", null)]
        [InlineData("Red Army Standard 7.62x39 122grn FMJ Ammunition 20rd box", "122")]
        [InlineData("Prvi Partizan PPU .308/7.62x51 M80 145grn FMJ 500RD CAN", "145")]
        [InlineData("Federal Range Target Practice .45Auto 230grn FMJ (RTP45230) 50rd box", "230")]
        [InlineData("Red Army Standard 7.62x54R FMJ 148grn", "148")]
        [InlineData("1000 Rounds of .25 ACP Ammo by Aguila - 50gr FMJ", "50")]
        [InlineData("1000 Rounds of .25 ACP Ammo by Aguila - gr50 FMJ", "50")]
        [InlineData("1000 Rounds of .25 ACP Ammo by gr Aguila - gr50 FMJ", "50")]
        [InlineData("17 Mach 2 (HM2) - 15.5 Grain NTX - Hornady Varmint Express - 500 Rounds", "15.5")]
        [InlineData("Monarch Auto FMJ .380 94-Grain Centerfire Ammunition", "94")]
        public void Grain_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new GrainParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
