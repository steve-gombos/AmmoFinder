using Xunit;

namespace AmmoFinder.Parsers.UnitTests.TestData
{
    public class RifleCaliber : TheoryData<string, string>
    {
        public RifleCaliber()
        {
            Add("Prvi Partizan PPU .22 Hornet 45grn SP 50rd Box", "22 Hornet");
            Add("20 Rounds of .204 Ruger Ammo by Sellier & Bellot - 32 gr PTS", "204 Ruger");
            Add("20 Rounds of .22 Hornet Ammo by Sellier & Bellot - 45gr FMJ", "22 Hornet");
            Add("20 Rounds of .22-250 Rem Ammo by Prvi Partizan - 55gr SP", "22-250");
            Add("Prvi Partizan .222 Remington 50grn Soft Point 20rd Box", "222 Rem");
            Add("Prvi Partizan PPU .223 69grn Match HP 20rd Box", "223");
            Add("20 Rounds of .224 Valkyrie Ammo by Federal American Eagle - 75gr TMJ", "224 Valkyrie");
            Add("20 Rounds of .243 Win Ammo by Black Hills Gold Ammunition - 85gr TSX", "243");
            Add("243 - 70 Grain BlitzKing - Sierra Prairie Enemy - 20 Rounds", "243");
            Add("Prvi Partizan PPU .264 Win Mag 140grn PSP 20rd Box", "264 Win Mag");
            Add("Prvi Partizan PPU .270 Winchester 150grn SP 20rd Box", "270");
            Add("270 - 150 Grain PP - Winchester Super-X - 20 Rounds", "270");
            Add("1080 Rounds of .30 Carbine Ammo in Ammo Can by Korean Military Surplus - 110gr FMJ", "30 Carbine");
            Add("Prvi Partizan PPU .30cal Carbine 110grn FMJ 50rd Box", "30 Carbine");
            Add("Prvi Partizan PPU .30-06 180grn SP 20rd Box", "30-06");
            Add("Prvi Partizan PPU .30-30 Winchester 150gn SP 20rd Box", "30-30");
            Add("Prvi Partizan PPU .300 BLACKOUT 125grn FMJ 20rd box", "300 AAC");
            Add("WOLF .300 AAC Blackout 145grn FMJ Ammunition 20rd box", "300 AAC");
            Add("20 Rounds of .300 Win Mag Ammo by Federal - 150gr SP", "300 Win Mag");
            Add("Prvi Partizan PPU .303 Brit 174grn FMJ 20rd Box", "303 Brit");
            Add("WPA (Wolf) .303 BRITISH 174grn FMJ 20rd Box", "303 Brit");
            Add("Prvi Partizan 308/7.62x51 M80 Mil. Spec. 145grn FMJ 20rd box", "308");
            Add(".32 Winchester Special", "32 Win Special");
            Add("10 Rounds of .338 Lapua Ammo by Sellier & Bellot - 300 gr HPBT", "338 Lapua");
            Add("350 Legend - 180 Grain SP - Federal Non-Typical - 20 Rounds", "350 Legend");
            Add("20 Rounds of .375 H&H Mag Ammo by Federal - 300 gr SP", "375 H&H");
            Add("375 Win - 200 Grain Sierra Pro-Hunter JFP - HSM - 20 Rounds", "375 Win");
            Add("Prvi 45-70 SJFP 405grn 20rd Box", "45-70");
            Add("470 Nitro Express - 500 Grain DGX Bonded - Hornady - 20 Rounds", "470 Nitro");
            Add("WOLF 5.45x39 60grn FMJ 30rd box", "5.45x39mm");
            Add("Federal 5.56x45 XM193 55grn FMJ Ammunition 400rd Can", "5.56");
            Add("Prvi Partizan PPU .50cal BMG M33 625grn FMJ Ammunition 5rd Box", "50 BMG");
            Add("50 Rounds of .50 BMG Ammo by Lake City - 660gr FMJ M33", "50 BMG");
            Add("6mm Creedmoor - 105 Grain BTHP - Hornady BLACK - 20 Rounds", "6mm Creedmoor");
            Add("Sellier & Bellot 6.5 Creedmoor Ammo 140grn FMJ BT 20rd box", "6.5mm Creedmoor");
            Add("20 Rounds of 6.5mm Creedmoor Ammo by Sellier & Bellot - 131gr SP", "6.5mm Creedmoor");
            Add("Prvi Partizan PPU 6.5 Grendel 120grn HP 20rd Box", "6.5mm Grendel");
            Add("20 Rounds of 6.5mm Grendel  Ammo by Hornady - 123gr SST", "6.5mm Grendel");
            Add("Norma 6.5 Japanese Arisaka 156grn SP Ammunition 20rd Box", "6.5mm Japanese");
            Add("Prvi Partizan PPU 6.5x55 Swede 139gr FMJ 20rd Box", "6.5mm Swedish");
            //Prvi Partizan PPU 6.5 Carcano 139grn FMJ 20rd Box (not sure about this one)
            Add("Prvi Partizan PPU 6.8 Remington SPC FMJ 115grn 20rd box", "6.8 SPC");
            Add("Prvi Partizan PPU 7.5 Swiss 174gr Soft Point 20rd Box", "7.5 Swiss");
            Add("Prvi Partizan PPU 7.5 French 139gn FMJ 20rd Box", "7.5 French");
            Add("RED ARMY STANDARD 7.62x39 FMJ 124grn 20rd box", "7.62x39mm");
            Add("Prvi Partizan PPU 7.62x54R 150grn SP 20rd Box", "7.62x54R");
            Add("Prvi Partizan PPU 7mm Mauser 139gr SP 20rd Box", "7mm Mauser");
            Add("20 Rounds of 7x57mm Mauser Ammo by Sellier & Bellot - 140gr FMJ", "7mm Mauser");
            Add("Prvi Partizan PPU 7mm Magnum 174gr PSP 20rd Box", "7mm Rem Mag");
            Add("7mm Rem Mag - 150 Grain GameChanger - Sierra - 20 Rounds", "7mm Rem Mag");
            Add("Prvi Partizan PPU 8mm Mauser FMJ 198grn 20rd Box", "8mm Mauser");
            Add("Prvi 8mm Maus 196grn SP 20rd Box", "8mm Mauser");
            Add("5.6x52mm Rimmed - 70 Grain FMJ - Sellier & Bellot - 20 Rounds", "5.6x52mm");
            Add("50 Rounds of 7.62x38mm Nagant Ammo by Prvi Partizan - 98gr FMJFN", "7.62x38mm Nagant");
            Add("Prvi Partizan PPU 6.5 Carcano 139grn FMJ 20rd Box", "6.5 Carcano");
            Add("Prvi Partizan PPU 25.06 90grn JHP 20rd Box", "25-06");
            Add("Prvi Partizan PPU .22 Remington Jet Magnum 45grn SP 50rd Box", "22 Rem Jet");
            Add("7.62x51mm - 145 Grain M80 FMJBT - Prvi Partizan - 20 Rounds", "308");
            Add("6mm ARC - 105 Grain HPBT - Hornady BLACK - 20 Rounds", "6mm ARC");
            Add(".270 WSM Win Short Mag", "270");

        }
    }
}
