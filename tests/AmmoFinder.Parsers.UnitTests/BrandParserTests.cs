using AmmoFinder.Parsers.Models;
using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class BrandParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData("Norma .17 HMR 17grn V-MAX Polymer Tip Ammunition 50rd box", Brands.Norma)]
        [InlineData("Aguila .22LR High Velocity 40grn Copper Coated 50rd Box", Brands.Aguila)]
        [InlineData("RED ARMY STANDARD 7.62x39 FMJ 124grn 20rd box", Brands.RedArmyStandard)]
        [InlineData("CCI Standard Velocity .22LR 40grn LRN 50rd box", Brands.CCI)]
        [InlineData("20 Rounds of 10mm Ammo by Corbon - 165gr JHP", Brands.Corbon)]
        [InlineData("Geco 7.62X39 Target 124grn FMJ 20rd box", Brands.Geco)]
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
        [InlineData("250 Rounds of 20ga Ammo by NobelSport - 7/8 ounce #7 steel shot", Brands.NobleSport)]
        [InlineData("12 Gauge - 2-3/4\" 9 Pellet 00 Buckshot - Noble Sport Low Recoil - 250 Rounds", Brands.NobleSport)]
        [InlineData("250 Rounds of 12ga Ammo by Winchester - 1 ounce #8 shot", Brands.Winchester)]
        [InlineData("250 Rounds of 12ga Ammo by Browning - 1-1/8 Ounce #7.5 Shot", Brands.Browning)]
        [InlineData("50 Beowulf - 300 Grain Bonded JHP - Underwood - 20 Rounds", Brands.Underwood)]
        [InlineData("Surplus Russian 7.62x54R 147grn FMJ 440rd Can", Brands.Russian)]
        [InlineData("38 Special - 77 Grain ARX - Inceptor - 20 Rounds", Brands.Inceptor)]
        [InlineData("22 LR - 38 gr HP - Eley Subsonic - 500 Rounds", Brands.Eley)]
        [InlineData("30-06 - 148 Grain FMJ - Kynoch Surplus (1950s Production) - 20 Rounds", Brands.KynochSurplus)]
        [InlineData("5.56x45 - 55 Grain Frangible - SinterFire - 250 Rounds", Brands.Sinterfire)]
        [InlineData("Silver Bear .380ACP FMJ 94grn 50rd Box", Brands.SilverBear)]
        [InlineData("Prvi 8mm Maus 196grn SP 20rd Box", Brands.PrviPartizan)]
        [InlineData("12 Gauge - 2-3/4\" 1oz. #7.5 Shot - Challenger - 250 Rounds", Brands.Challenger)]
        [InlineData("300 AAC Blackout - 150 Grain FMJ - Red Mountain Arsenal - 20 Rounds", Brands.RedMountainArsenal)]
        [InlineData("STV Technology Scorpio 9mm 124grn FMJ Ammunition 50rd Box", Brands.STVTechnology)]
        [InlineData("Surplus 7.62x54R 147grn FMJ 440rd Can", Brands.Surplus)]
        [InlineData("50 Rounds of 5.7x28 mm Ammo by FN Herstal - 40gr V-MAX", Brands.FNHerstal)]
        [InlineData("Sellier and Bellot 6.5x57mm Ammo - 20 Rounds of 131 Grain SP Ammunition", Brands.SellierBellot)]
        [InlineData("MAXXTech 9mm Ammo - 50 Rounds of 115 Grain FMJ Ammunition", Brands.MaxxTech)]
        [InlineData("Liberty Civil Defense, .40 S&W, HP, 60 Grain, 20 Rounds", Brands.Liberty)]
        [InlineData("Lightfield Home Defender, 12 Gauge, 2 3/4\", 130 Grain, Rubber Slug Rounds, 5 Rounds", Brands.Lightfield)]
        [InlineData("Monarch® .30 Carbine FMJ 110-Grain Ammunition", Brands.Monarch)]
        [InlineData("American Eagle .38 Special 158-Grain Lead Round Nose Handgun Ammunition", Brands.Federal)]
        [InlineData("FN 5.7 x 28mm 40-Grain V-Max Cartridges", Brands.FNHerstal)]
        [InlineData("Traditions LED Muzzleloader Bore Light", Brands.Traditions)]
        [InlineData("HEVI-Shot HEVI-Hammer 20 Gauge Shotshells", Brands.HeviShot)]
        [InlineData("Hodgdon H110 1 lb Spherical Pistol/Shotgun Powder", Brands.Hodgdon)]
        [InlineData("Top Brass Premium Reconditioned .223 Rem Brass Headstamps", Brands.TopBrass)]
        [InlineData("Frankford Arsenal Platinum Series Multi Caliber Case Prep Center", Brands.FrankfordArsenal)]
        [InlineData("HEVI-Metal™ 12 Gauge Turkey Shotshells", Brands.HeviMetal)]
        [InlineData("Brenneke Magnum Crush 12 Gauge Shotgun Slugs", Brands.Brenneke)]
        [InlineData("PowerBelt AeroTip™ Copper .50 Caliber 295-Grain Ammunition", Brands.PowerBelt)]
        [InlineData("Thompson/Center Maxi Hunter .50 350-Grain Black Powder Ammunition", Brands.ThompsonCenter)]
        [InlineData("Independence® Aluminum .45 Auto 230-Grain Handgun Ammunition", Brands.Independence)]
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
