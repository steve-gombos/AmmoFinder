using Xunit;

namespace AmmoFinder.Parsers.UnitTests.TestData
{
    public class RimfireCaliber : TheoryData<string, string>
    {
        public RimfireCaliber()
        {
            Add("Norma .17 HMR 17grn V-MAX Polymer Tip Ammunition 50rd box", "17 HMR");
            Add("50 Rounds of .17HMR Ammo by Winchester Non-Toxic - 15.5gr Polymer Tipped", "17 HMR");
            Add("50 Rounds of .17 WSM Ammo by Federal - 20gr Polymer Tip", "17 WSM");
            Add("17 Win Super Mag - 20 gr - Polymer Tip - Federal American Eagle - 50 Rounds", "17 WSM");
            Add("17 Mach 2 (HM2) - 15.5 Grain NTX - Hornady Varmint Express - 500 Rounds", "17 HM2");
            Add("17 Hornady Mach 2 (HM2) - 17 gr V-MAX - Hornady - 50 Rounds", "17 HM2");
            Add("100 Rounds of .22 Long Ammo by CCI - 29gr CPRN", "22 Long");
            Add("Aguila .22LR High Velocity 40grn Copper Coated 50rd Box", "22 LR");
            Add("100 Rounds of .22 Short Ammo by CCI - 29gr CPRN", "22 Short");
            Add("50 Rounds of .22 WMR Ammo by Fiocchi - 40gr JSP", "22 WMR");
            Add("22 Winchester Automatic - 45 gr LRN - Aguila - 50 Rounds - (Model 1903 Rifle Only!)", "22 WMR");
            Add("5mm Rem Mag - 30 Grain SJHP - Aguila - 50 Rounds", "5mm Rem Mag");
        }
    }
}
