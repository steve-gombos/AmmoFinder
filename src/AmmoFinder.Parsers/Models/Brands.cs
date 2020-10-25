using System.Collections.Generic;
using System.Collections.Immutable;

namespace AmmoFinder.Parsers.Models
{
    public static class Brands
    {
        public static readonly ImmutableDictionary<string, List<string>> Variances = new Dictionary<string, List<string>>
        {
            {EstateCartridge, new List<string>{"Estate Cartidge", "Estate"} },
            {NobleSport, new List<string>{"Nobel Sport" } },
            {PrviPartizan, new List<string>{"PPU", "Prvi"} },
            {SellierBellot, new List<string>{ "Sellier and Bellot" } },
            {SigSauer, new List<string>{"Sig"} }
        }.ToImmutableDictionary();

        public const string Unknown = "Unknown";

        public const string Aguila = "Aguila";
        public const string AlexanderArms = "Alexander Arms";
        public const string AmmoInc = "Ammo Inc";
        public const string Armscor = "Armscor";
        public const string AtomicAmmunition = "Atomic Ammunition";
        public const string Barnes = "Barnes";
        public const string BlackHills = "Black Hills";
        public const string Blazer = "Blazer";
        public const string BrownBear = "Brown Bear";
        public const string Browning = "Browning";
        public const string BuffaloBore = "Buffalo Bore";
        public const string CCI = "CCI";
        public const string Challenger = "Challenger";
        public const string Corbon = "Corbon";
        public const string Eley = "Eley";
        public const string EstateCartridge = "Estate Cartridge";
        public const string Federal = "Federal";
        public const string Fiocchi = "Fiocchi";
        public const string FNHerstal = "FN Herstal";
        public const string Frontier = "Frontier";
        public const string Geco = "Geco";
        public const string Herters = "Herter's";
        public const string HeviShot = "HeviShot";
        public const string Hornady = "Hornady";
        public const string Hotshot = "Hotshot";
        public const string HSM = "HSM";
        public const string Inceptor = "Inceptor";
        public const string Kent = "Kent";
        public const string Korea = "Korea";
        public const string KynochSurplus = "Kynoch Surplus";
        public const string LakeCity = "Lake City";
        public const string Liberty = "Liberty";
        public const string Lightfield = "Lightfield";
        public const string MagTech = "MagTech";
        public const string MaxxTech = "MaxxTech";
        public const string MEDEF = "MEDEF";
        public const string MilitarySurplus = "Military Surplus";
        public const string MTM = "MTM";
        public const string NobleSport = "Noble Sport";
        public const string Norma = "Norma";
        public const string Nosler = "Nosler";
        public const string PMC = "PMC";
        public const string PrviPartizan = "Prvi Partizan";
        public const string RedArmyStandard = "Red Army Standard";
        public const string RedMountainArsenal = "Red Mountain Arsenal";
        public const string Remington = "Remington";
        public const string Russian = "Russian";
        public const string Rio = "Rio";
        public const string SellierBellot = "Sellier & Bellot";
        public const string Sierra = "Sierra";
        public const string SigSauer = "Sig Sauer";
        public const string SilverBear = "Silver Bear";
        public const string Sinterfire = "SinterFire";
        public const string Speer = "Speer";
        public const string STVTechnology = "STV Technology";
        public const string Surplus = "Surplus";
        public const string Tula = "Tula";
        public const string Underwood = "Underwood";
        public const string Weatherby = "Weatherby";
        public const string Winchester = "Winchester";
        public const string Wolf = "Wolf";
    }
}
