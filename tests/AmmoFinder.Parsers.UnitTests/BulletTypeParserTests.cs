using AmmoFinder.Parsers.Models;
using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class BulletTypeParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData("Invalid", null)]
        [InlineData("<p>Red Army Standard 7.62x39 122grn FMJ ammunition. Features a Polymer coated steel case. Packaged 20rds to a box, 50boxes per case (1,000rds). 122 grain FMJ bullet, and Berdan primed (Non-corrosive) Steel case. Made in Russia.&nbsp;&nbsp;</p>", BulletTypes.FMJ)]
        [InlineData("50 Rounds of 9mm Ammo by Speer - 147gr TMJ", BulletTypes.TMJ)]
        [InlineData("50 Rounds of .45 GAP Ammo by Speer Gold Dot - 185gr JHP", BulletTypes.JHP)]
        [InlineData("20 Rounds of .308 Win Ammo by Prvi Partizan - 180gr SP", BulletTypes.SP)]
        [InlineData("20 Rounds of .308 Win Ammo by Black Hills Match Ammunition - 175gr HPBT", BulletTypes.HPBT)]
        public void BulletTypeParser_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new BulletTypeParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
