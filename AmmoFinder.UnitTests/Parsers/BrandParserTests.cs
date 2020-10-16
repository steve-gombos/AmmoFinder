using AmmoFinder.Parsers;
using AmmoFinder.Parsers.Models;
using Xunit;

namespace AmmoFinder.UnitTests.Parsers
{
    public class BrandParserTests
    {
        [Theory]
        [InlineData("Norma .17 HMR 17grn V-MAX Polymer Tip Ammunition 50rd box", Brands.Norma)]
        [InlineData("Aguila .22LR High Velocity 40grn Copper Coated 50rd Box", Brands.Aguila)]
        [InlineData("RED ARMY STANDARD 7.62x39 FMJ 124grn 20rd box", Brands.RedArmyStandard)]
        [InlineData("CCI Standard Velocity .22LR 40grn LRN 50rd box", Brands.CCI)]
        [InlineData("Geco 7.62X39 Target 124grn FMJ 20rd box", Brands.Geco)]
        //Surplus Russian 7.62x54R 147grn FMJ 440rd Can
        [InlineData("CCI Blazer 9mm FMJ 124grn Brass 50rd box", Brands.Blazer)]
        [InlineData("Speer Gold Dot Personal Protection .40 SW 180grn HP Ammunition 20rd box", Brands.Speer)]
        [InlineData("MEDEF Venom 9mm 115grn FMJ Ammunition 50rd box", Brands.MEDEF)]
        [InlineData("Brown Bear 9x18 94grn FMJ 50rd Box", Brands.BrownBear)]
        [InlineData("Estate Cartidge 12 GAUGE 00 Buck 2 3/4 Ammunition 25rd box", Brands.EstateCartridge)]
        [InlineData("Hotshot .45 Auto  230grn FMJ Ammunition 50rd box", Brands.Hotshot)]
        [InlineData("Remington UMC .380 Auto FMJ 95grn Ammunition 50rd Box", Brands.Remington)]
        [InlineData("Magtech 10mm 180grn FMJ Ammunition 50rd box", Brands.MagTech)]
        [InlineData("20 Rounds of .308 Win Ammo by Wolf Performance - 150gr FMJ", Brands.Wolf)]
        [InlineData("20 Rounds of .223 Ammo by PMC - 55gr FMJBT", Brands.PMC)]
        [InlineData("20 Rounds of 7.62x39mm Ammo by Fiocchi - 124gr FMJ", Brands.Fiocchi)]
        [InlineData("20 Rounds of .22 Hornet Ammo by Sellier & Bellot - 45gr FMJ", Brands.SellierBellot)]
        [InlineData("20 Rounds of 5.56x45 Ammo by Federal American Eagle - 62gr FMJ", Brands.Federal)]
        [InlineData("500 Rounds of 7.62x51mm Ammo by Lake City - 149gr FMJ", Brands.LakeCity)]
        [InlineData("20 Rounds of 30-06 Springfield Ammo by Prvi Partizan - 150gr SP", Brands.PrviPartizan)]
        [InlineData("20 Rounds of .350 Legend Ammo by Hornady American Whitetail - 170gr InterLock", Brands.Hornady)]
        [InlineData("20 Rounds of .270 Win Ammo by Black Hills Gold Ammunition - 130gr Hornady SST", Brands.BlackHills)]
        [InlineData("250 Rounds of 12ga Ammo by Rio Target Load Trap - 7/8 ounce #7.5 shot", Brands.Rio)]
        [InlineData("250 Rounds of 20ga Ammo by NobelSport - 7/8 ounce #7 steel shot", Brands.NobelSport)]
        [InlineData("250 Rounds of 12ga Ammo by Winchester - 1 ounce #8 shot", Brands.Winchester)]
        [InlineData("250 Rounds of 12ga Ammo by Browning - 1-1/8 Ounce #7.5 Shot", Brands.Browning)]
        [InlineData("", null)]
        public void Brand_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new BrandParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
