using AmmoFinder.Parsers.Models;
using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class CasingParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData("Invalid", null)]
        [InlineData("<p>Red Army Standard 7.62x39 122grn FMJ ammunition. Features a Polymer coated steel case. Packaged 20rds to a box, 50boxes per case (1,000rds). 122 grain FMJ bullet, and Berdan primed (Non-corrosive) Steel case. Made in Russia.&nbsp;&nbsp;</p>", Casings.Steel)]
        [InlineData("<p>Beautiful 1980's Surplus Korean .30 Caliber Carbine ammunition. Clean, bright, non-corrosive, boxer primed, brass case ammo on 10rd chargers, each with retractable spoon. Packed 10rds to a charger, 120rds in to a cloth bandolier (12 chargers) and 1,080rds (9 bandoliers) in a resealable M2A1 ammo can. You don't see this kind of quality surplus often, don't miss out. (must buy 9 bandoliers to get Can)</p>", Casings.Brass)]
        [InlineData("Winchester® USA 9mm Handgun Ammo is a preferred choice by serious handgunners who demand dependable ammunition that delivers consistent accuracy shot after shot. USA ammo is an ideal choice for training or extended sessions at the range. Full metal jacket bullets (FMJ) ensure reliable feeding and prevent leading of feed ramp and barrel. Winchester USA brand ammunition provides great performance at a value price. Reloadable brass cases.", Casings.Brass)]
        [InlineData("<div class=\"desc std\"><ul><li>Quantity - 50 rounds per box</li><li>Manufacturer - Speer</li><li>Bullets - 124 grain jacketed hollow point (JHP)</li><li>Casings - Boxer-primed nickel-plated brass</li></ul></div>", Casings.NickelPlatedBrass)]
        public void Casing_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new CasingParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
