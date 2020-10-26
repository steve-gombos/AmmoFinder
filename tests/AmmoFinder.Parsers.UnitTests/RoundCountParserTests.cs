using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class RoundCountParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData("Invalid", null)]
        [InlineData("Red Army Standard 7.62x39 122grn FMJ Ammunition 20rd box", "20")]
        [InlineData("Prvi Partizan PPU .308/7.62x51 M80 145grn FMJ 500RD CAN", "500")]
        [InlineData("PMC 12GA One Shot High Velocity Buckshot 00 9 Pellet 2-3/4 in. 5rd Box", "5")]
        [InlineData("Federal Range Target Practice .45Auto 230grn FMJ (RTP45230) 50rd box", "50")]
        [InlineData("Red Army Standard 7.62x54R FMJ 148grn", null)]
        [InlineData("1000 Rounds of .25 ACP Ammo by Aguila - 50gr FMJ", "1000")]
        [InlineData("Box of 1000 of .25 ACP Ammo by Aguila - 50gr FMJ", "1000")]
        [InlineData("RED ARMY STANDARD 308 150grn FMJ 20rd Box", "20")]
        [InlineData("0.174 sectional density, .44 Mag caliber, 0.145 ballistic coefficient, High ballistic coefficient provides a flat trajectory for excellent downrange energy increases and bullet expansion at all ranges, 225 grains, Not recommended for storage in tubular magazines for an extended period of time, which can result in tip deformation, H.I.T.S. 485 for small game less than 50 lb., 20-round box, Elastomer Flex Tip technology for safe use in tubular magazines, Up to 250 fps faster muzzle velocity than conventional lever gun loads", "20")]
        [InlineData("Highly accurate target round, .308 Winchester caliber, Sierra MatchKing boat-tail bullet, 168 grains, 20-round box", "20")]
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
