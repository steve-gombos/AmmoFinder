﻿using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class ProjectileTypeParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData("<p>Red Army Standard 7.62x39 122grn FMJ ammunition. Features a Polymer coated steel case. Packaged 20rds to a box, 50boxes per case (1,000rds). 122 grain FMJ bullet, and Berdan primed (Non-corrosive) Steel case. Made in Russia.&nbsp;&nbsp;</p>", "fmj")]
        [InlineData("<p>Beautiful 1980's Surplus Korean .30 Caliber Carbine ammunition. Clean, bright, non-corrosive, boxer primed, brass case ammo on 10rd chargers, each with retractable spoon. Packed 10rds to a charger, 120rds in to a cloth bandolier (12 chargers) and 1,080rds (9 bandoliers) in a resealable M2A1 ammo can. You don't see this kind of quality surplus often, don't miss out. (must buy 9 bandoliers to get Can)</p>", null)]
        public void ProjectileTypeParser_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new ProjectileTypeParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}