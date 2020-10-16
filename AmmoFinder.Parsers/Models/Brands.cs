using System;
using System.Collections.Generic;
using System.Text;

namespace AmmoFinder.Parsers.Models
{
    public class Brands
    {
        public static readonly Dictionary<string, List<string>> Variances = new Dictionary<string, List<string>>
        {
            {Brands.EstateCartridge, new List<string>{"Estate Cartidge"} },
            {Brands.PrviPartizan, new List<string>{"PPU"} },
            {Brands.SigSauer, new List<string>{"Sig"} }
        };


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
        public const string EstateCartridge = "Estate Cartridge";
        public const string Federal = "Federal";
        public const string Fiocchi = "Fiocchi";
        public const string Frontier = "Frontier";
        public const string Geco = "Geco";
        public const string Herters = "Herter's";
        public const string HeviShot = "HeviShot";
        public const string Hornady = "Hornady";
        public const string Hotshot = "Hotshot";
        public const string HSM = "HSM";
        public const string Kent = "Kent";
        public const string Korea = "Korea";
        public const string LakeCity = "Lake City";
        public const string MagTech = "MagTech";
        public const string MEDEF = "MEDEF";
        public const string MilitarySurplus = "Military Surplus";
        public const string MTM = "MTM";
        public const string NobelSport = "Nobel Sport";
        public const string Norma = "Norma";
        public const string Nosler = "Nosler";
        public const string PMC = "PMC";
        public const string PrviPartizan = "Prvi Partizan";
        public const string PPU = "Prvi Partizan";
        public const string RedArmyStandard = "Red Army Standard";
        public const string Remington = "Remington";
        public const string Rio = "Rio";
        public const string SellierBellot = "Sellier & Bellot";
        public const string Sierra = "Sierra";
        public const string SigSauer = "Sig Sauer";
        public const string Speer = "Speer";
        public const string Tula = "Tula";
        public const string Weatherby = "Weatherby";
        public const string Winchester = "Winchester";
        public const string Wolf = "Wolf";
    }
}
