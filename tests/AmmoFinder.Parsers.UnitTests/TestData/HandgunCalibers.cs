using Xunit;

namespace AmmoFinder.Parsers.UnitTests.TestData
{
    public class HandgunCalibers : TheoryData<string, string>
    {
        public HandgunCalibers()
        {
            Add("Sellier & Bellot 10mm 180grn FMJ 50rd Box", "10mm");
            Add("1000 Rounds of .25 ACP Ammo by Aguila - 50gr FMJ", "25 ACP");
            Add("Prvi Partizan PPU .25 Auto 50grn FMJ 50rd Box", "25 ACP");
            Add("Prvi Partizan PPU 7.65 Para (.30 Luger) 93grn FMJ 50rd box", "30 Luger");
            Add("50 Rounds of .32 ACP Ammo by PMC - 60gr JHP", "32 ACP");
            Add("Norma .32 Auto 73grn FMJ Ammunition 50rd Box", "32 ACP");
            Add("Prvi Partizan PPU .32 S&W Long LRN 98grn 50rd Box", "32 S&W");
            Add("50 Rounds of .32S&W Long Ammo by Prvi Partizan - 98gr LRN", "32 S&W");
            Add("Prvi Partizan PPU .357 Magnum FPJ 158grn 50rd Box", "357 Mag");
            Add("50 Rounds of .357 SIG Ammo by Speer Gold Dot LE - 125gr JHP", "357 Sig");
            Add("Prvi Partizan PPU .38 S&W 145grn LRN 50rd Box", "38 S&W");
            Add("Prvi Partizan PPU .38 Special 130grn FMJ 50rd Box", "38 Special");
            Add("50 Rounds of .38 Spl Ammo by Fiocchi - 148gr Lead Wadcutter", "38 Special");
            Add("PMC . 38 Super +P 130grn FMJ Ammunition 50rd Box", "38 Super");
            Add("Prvi Partizan PPU .380 ACP FMJ 94grn 50rd Box", "380 ACP");
            Add("Prvi Partizan PPU .380 Auto JHP 94grn 50rd Box", "380 ACP");
            Add("Prvi Partizan PPU .40SW 165grn FMJ 50rd Box", "40 S&W");
            Add("Federal BALLISTICLEAN .40S&W 125grn Frangible RHT 50rd box", "40 S&W");
            Add("CCI Blazer Brass .40 S&W FMJ 180grn 50rd box", "40 S&W");
            Add("Prvi Partizan PPU .44 Magnum 240grn JHP 50rd Box", "44 Mag");
            Add("500 Rounds of .44 Mag Ammo by PMC - 180gr JHP", "44 Mag");
            Add("Magtech .44 Rem. Magnum 240grn Jacketed Soft Point 50rd Box", "44 Mag");
            Add("Fiocchi .44spl 200grn Jacketed Soft Point (44SA) 50rd box", "44 Special");
            Add("Prvi Partizan PPU .45 AUTO SJHP 185grn 50rd Box", "45 ACP");
            Add("Hotshot .45 Auto  230grn FMJ Ammunition 50rd box", "45 ACP");
            //Prvi 45-70 SJFP 405grn 20rd Box (this ones seems like 45 ACP, research)
            Add("50 Rounds of .45 GAP Ammo by Speer - 200gr TMJ", "45 GAP");
            Add("50 Rounds of .45 Long-Colt Ammo by Fiocchi - 250gr LRNFP", "45 Long Colt");
            Add("20 Rounds of .454 Casull Ammo by Magtech - 260gr SJSP", "454 Casull");
            Add("50 Rounds of 5.7x28 mm Ammo by FN Herstal - 40gr V-MAX", "5.7x28");
            Add("20 Rounds of .500 S&W Mag Ammo by Armscor - 300 gr XTP", "500 S&W");
            Add("Sellier & Bellot 7.62x25 Tokarev, FMJ, 85 grn  50rd Box", "7.62x25 Tokarev");
            Add("PPU 7.62x25 85grn FMJ 50rd Box", "7.62x25 Tokarev");
            Add("CCI Blazer Brass 9mm 115gr FMJ 50rd Box", "9mm");
            Add("1000 Rounds of 7.62x39mm Ammo by Tula - 122gr FMJ", "7.62x39mm");
            Add("750 Rounds of 5.45x39mm Ammo by Wolf WPA - 60gr FMJ", "5.45x39mm");
            Add("WOLF 9x18 Makarov FMJ 92grn 50rd Box", "9x18 Makarov");
            Add("Prvi Partizan PPU 9x18 95grn JHP 50rd Box", "9x18 Makarov");
            Add("41 Mag - 210 Grain Semi-Jacketed Soft Point - Remington HTP - 50 Rounds", "41 Mag");
            Add("7.62mm Nagant - 98 gr FPJ - Prvi Partizan - 50 Rounds", "7.62mm Nagant");
            Add("7.62 Nagant - 97 Grain FMJ - Fiocchi - 50 Rounds", "7.62mm Nagant");
            Add("9x23mm Winchester - 124 gr JSP - Winchester USA - 50 Rounds", "9x23mm Winchester");
            Add("Prvi Partizan PPU 7.63 Mauser 85grn FMJ 50rd Box", "7.63mm Mauser");
        }
    }
}
