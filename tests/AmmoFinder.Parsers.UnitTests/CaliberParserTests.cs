using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class CaliberParserTests
    {
        //TODO: Create tests
        /*
         * --32 win spc
            --224
            --308
            --350
            --7mm rem mag
            --280rem
        */
        [Theory]
        [InlineData("", null)]

        //Rimfire
        [InlineData("Norma .17 HMR 17grn V-MAX Polymer Tip Ammunition 50rd box", "17 HMR")]
        [InlineData("50 Rounds of .17HMR Ammo by Winchester Non-Toxic - 15.5gr Polymer Tipped", "17 HMR")]
        [InlineData("50 Rounds of .17 WSM Ammo by Federal - 20gr Polymer Tip", "17 WSM")]
        [InlineData("100 Rounds of .22 Long Ammo by CCI - 29gr CPRN", "22 Long")]
        [InlineData("Aguila .22LR High Velocity 40grn Copper Coated 50rd Box", "22 LR")]
        [InlineData("100 Rounds of .22 Short Ammo by CCI - 29gr CPRN", "22 Short")]
        [InlineData("50 Rounds of .22 WMR Ammo by Fiocchi - 40gr JSP", "22 WMR")]

        //Pistol
        [InlineData("Sellier & Bellot 10mm 180grn FMJ 50rd Box", "10mm")]
        [InlineData("1000 Rounds of .25 ACP Ammo by Aguila - 50gr FMJ", "25 ACP")]
        [InlineData("Prvi Partizan PPU .25 Auto 50grn FMJ 50rd Box", "25 ACP")]
        [InlineData("Prvi Partizan PPU 7.65 Para (.30 Luger) 93grn FMJ 50rd box", "30 Luger")]
        [InlineData("50 Rounds of .32 ACP Ammo by PMC - 60gr JHP", "32 ACP")]
        [InlineData("Norma .32 Auto 73grn FMJ Ammunition 50rd Box", "32 ACP")]
        [InlineData("Prvi Partizan PPU .32 S&W Long LRN 98grn 50rd Box", "32 S&W")]
        [InlineData("50 Rounds of .32S&W Long Ammo by Prvi Partizan - 98gr LRN", "32 S&W")]
        [InlineData("Prvi Partizan PPU .357 Magnum FPJ 158grn 50rd Box", "357 Mag")]
        [InlineData("50 Rounds of .357 SIG Ammo by Speer Gold Dot LE - 125gr JHP", "357 Sig")]
        [InlineData("Prvi Partizan PPU .38 S&W 145grn LRN 50rd Box", "38 S&W")]
        [InlineData("Prvi Partizan PPU .38 Special 130grn FMJ 50rd Box", "38 Special")]
        [InlineData("50 Rounds of .38 Spl Ammo by Fiocchi - 148gr Lead Wadcutter", "38 Special")]
        [InlineData("PMC . 38 Super +P 130grn FMJ Ammunition 50rd Box", "38 Super")]
        [InlineData("Prvi Partizan PPU .380 ACP FMJ 94grn 50rd Box", "380 ACP")]
        [InlineData("Prvi Partizan PPU .380 Auto JHP 94grn 50rd Box", "380 ACP")]
        [InlineData("Prvi Partizan PPU .40SW 165grn FMJ 50rd Box", "40 S&W")]
        [InlineData("Federal BALLISTICLEAN .40S&W 125grn Frangible RHT 50rd box", "40 S&W")]
        [InlineData("CCI Blazer Brass .40 S&W FMJ 180grn 50rd box", "40 S&W")]
        [InlineData("Prvi Partizan PPU .44 Magnum 240grn JHP 50rd Box", "44 Mag")]
        [InlineData("500 Rounds of .44 Mag Ammo by PMC - 180gr JHP", "44 Mag")]
        [InlineData("Magtech .44 Rem. Magnum 240grn Jacketed Soft Point 50rd Box", "44 Mag")]
        [InlineData("Fiocchi .44spl 200grn Jacketed Soft Point (44SA) 50rd box", "44 Special")]
        [InlineData("Prvi Partizan PPU .45 AUTO SJHP 185grn 50rd Box", "45 ACP")]
        [InlineData("Hotshot .45 Auto  230grn FMJ Ammunition 50rd box", "45 ACP")]
        //Prvi 45-70 SJFP 405grn 20rd Box (this ones seems like 45 ACP, research)
        [InlineData("50 Rounds of .45 GAP Ammo by Speer - 200gr TMJ", "45 GAP")]
        [InlineData("20 Rounds of .454 Casull Ammo by Magtech - 260gr SJSP", "454 Casull")]
        [InlineData("50 Rounds of 5.7x28 mm Ammo by FN Herstal - 40gr V-MAX", "5.7x28")]
        [InlineData("20 Rounds of .500 S&W Mag Ammo by Armscor - 300 gr XTP", "500 S&W")]
        [InlineData("Sellier & Bellot 7.62x25 Tokarev, FMJ, 85 grn  50rd Box", "7.62x25 Tokarev")]
        [InlineData("CCI Blazer Brass 9mm 115gr FMJ 50rd Box", "9mm")]
        [InlineData("1000 Rounds of 7.62x39mm Ammo by Tula - 122gr FMJ", "7.62x39")]
        [InlineData("750 Rounds of 5.45x39mm Ammo by Wolf WPA - 60gr FMJ", "5.45x39")]
        [InlineData("WOLF 9x18 Makarov FMJ 92grn 50rd Box", "9x18 Makarov")]

        //Shotgun
        [InlineData("WOLF Power Buckshot 12 Gauge 00 Buck 5rd Box", "12 Gauge")]
        [InlineData("5 Rounds of 12ga Ammo by Hornady - 300 grain SST Sabot Slug", "12 Gauge")]
        [InlineData("250 Rounds of 20ga Ammo by NobelSport - 7/8 ounce #7 steel shot", "20 Gauge")]

        //Rifle
        [InlineData("Prvi Partizan PPU .22 Hornet 45grn SP 50rd Box", "22 Hornet")]
        [InlineData("20 Rounds of .204 Ruger Ammo by Sellier & Bellot - 32 gr PTS", "204 Ruger")]
        [InlineData("20 Rounds of .22 Hornet Ammo by Sellier & Bellot - 45gr FMJ", "22 Hornet")]
        [InlineData("20 Rounds of .22-250 Rem Ammo by Prvi Partizan - 55gr SP", "22-250")]
        [InlineData("Prvi Partizan .222 Remington 50grn Soft Point 20rd Box", "222 Rem")]
        [InlineData("Prvi Partizan PPU .223 69grn Match HP 20rd Box", "223")]
        [InlineData("20 Rounds of .243 Win Ammo by Black Hills Gold Ammunition - 85gr TSX", "243 Win")]
        [InlineData("Prvi Partizan PPU .270 Winchester 150grn SP 20rd Box", "270 Win")]
        [InlineData("1080 Rounds of .30 Carbine Ammo in Ammo Can by Korean Military Surplus - 110gr FMJ", "30 Carbine")]
        [InlineData("Prvi Partizan PPU .30cal Carbine 110grn FMJ 50rd Box", "30 Carbine")]
        [InlineData("Prvi Partizan PPU .30-06 180grn SP 20rd Box", "30-06")]
        [InlineData("Prvi Partizan PPU .30-30 Winchester 150gn SP 20rd Box", "30-30")]
        [InlineData("Prvi Partizan PPU .300 BLACKOUT 125grn FMJ 20rd box", "300 AAC")]
        [InlineData("WOLF .300 AAC Blackout 145grn FMJ Ammunition 20rd box", "300 AAC")]
        [InlineData("20 Rounds of .300 Win Mag Ammo by Federal - 150gr SP", "300 Win Mag")]
        [InlineData("Prvi Partizan PPU .303 Brit 174grn FMJ 20rd Box", "303 Brit")]
        [InlineData("WPA (Wolf) .303 BRITISH 174grn FMJ 20rd Box", "303 Brit")]
        [InlineData("Prvi Partizan 308/7.62x51 M80 Mil. Spec. 145grn FMJ 20rd box", "308")]
        [InlineData("10 Rounds of .338 Lapua Ammo by Sellier & Bellot - 300 gr HPBT", "338 Lapua")]
        [InlineData("20 Rounds of .375 H&H Mag Ammo by Federal - 300 gr SP", "375 H&H")]
        [InlineData("Prvi 45-70 SJFP 405grn 20rd Box", "45-70")]
        [InlineData("WOLF 5.45x39 60grn FMJ 30rd box", "5.45x39")]
        [InlineData("Federal 5.56x45 XM193 55grn FMJ Ammunition 400rd Can", "5.56")]
        [InlineData("Prvi Partizan PPU .50cal BMG M33 625grn FMJ Ammunition 5rd Box", "50 BMG")]
        [InlineData("50 Rounds of .50 BMG Ammo by Lake City - 660gr FMJ M33", "50 BMG")]
        [InlineData("Sellier & Bellot 6.5 Creedmoor Ammo 140grn FMJ BT 20rd box", "6.5 Creedmoor")]
        [InlineData("20 Rounds of 6.5mm Creedmoor Ammo by Sellier & Bellot - 131gr SP", "6.5 Creedmoor")]
        [InlineData("Prvi Partizan PPU 6.5 Grendel 120grn HP 20rd Box", "6.5 Grendel")]
        [InlineData("20 Rounds of 6.5mm Grendel  Ammo by Hornady - 123gr SST", "6.5 Grendel")]
        [InlineData("Norma 6.5 Japanese Arisaka 156grn SP Ammunition 20rd Box", "6.5 Japanese")]
        [InlineData("Prvi Partizan PPU 6.5x55 Swede 139gr FMJ 20rd Box", "6.5 Swedish")]
        //Prvi Partizan PPU 6.5 Carcano 139grn FMJ 20rd Box (not sure about this one)
        [InlineData("Prvi Partizan PPU 6.8 Remington SPC FMJ 115grn 20rd box", "6.8 SPC")]
        [InlineData("Prvi Partizan PPU 7.5 Swiss 174gr Soft Point 20rd Box", "7.5 Swiss")]
        [InlineData("Prvi Partizan PPU 7.5 French 139gn FMJ 20rd Box", "7.5 French")]
        [InlineData("RED ARMY STANDARD 7.62x39 FMJ 124grn 20rd box", "7.62x39")]
        [InlineData("Prvi Partizan PPU 7.62x54R 150grn SP 20rd Box", "7.62x54R")]
        [InlineData("Prvi Partizan PPU 7mm Mauser 139gr SP 20rd Box", "7mm Mauser")]
        [InlineData("20 Rounds of 7x57mm Mauser Ammo by Sellier & Bellot - 140gr FMJ", "7mm Mauser")]
        [InlineData("Prvi Partizan PPU 7mm Magnum 174gr PSP 20rd Box", "7mm Mag")]
        [InlineData("Prvi Partizan PPU 8mm Mauser FMJ 198grn 20rd Box", "8mm Mauser")]
        [InlineData("Prvi 8mm Maus 196grn SP 20rd Box", "8mm Mauser")]
        public void Caliber_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new CaliberParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
