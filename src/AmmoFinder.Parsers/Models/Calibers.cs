using System.Collections.Generic;

namespace AmmoFinder.Parsers.Models
{
    public class Calibers : Dictionary<string, IEnumerable<SearchCriteria>>
    {
        public Calibers()
        {
            Add("RimFire", new List<SearchCriteria>
                {
                    new SearchCriteria
                    {
                        Name = "17 HMR",
                        SearchIndicators = new List<string>
                        {
                            "17hmr"
                        }
                    },
                    //new SearchCriteria
                    //{
                    //    Name = "17 Mach2",
                    //    SearchIndicators = new List<string>
                    //    {
                    //        "17mach2"
                    //    }
                    //},
                    new SearchCriteria
                    {
                        Name = "17 WSM",
                        SearchIndicators = new List<string>
                        {
                            "17wsm"
                        }
                    },
                    new SearchCriteria
                    {
                        Name = "22 Long",
                        SearchIndicators = new List<string>
                        {
                            "22long"
                        }
                    },
                     new SearchCriteria
                    {
                        Name = "22 LR",
                        SearchIndicators = new List<string>
                        {
                            "22lr",
                            "22longrifle"
                        }
                    },
                    new SearchCriteria
                    {
                        Name = "22 Short",
                        SearchIndicators = new List<string>
                        {
                            "22short"
                        }
                    },
                    new SearchCriteria
                    {
                        Name = "22 WMR",
                        SearchIndicators = new List<string>
                        {
                            "22wmr"
                        }
                    }
                });

            Add("Pistol", new List<SearchCriteria>
                {
                    new SearchCriteria
                        {
                            Name = "22 TCM",
                            SearchIndicators = new List<string>
                            {
                                "22tcm"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "25 ACP",
                            SearchIndicators = new List<string>
                            {
                                "25acp",
                                "25auto"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "30 Luger",
                            SearchIndicators = new List<string>
                            {
                                "30luger"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "32 ACP",
                            SearchIndicators = new List<string>
                            {
                                "32acp",
                                "32auto"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "32 Long",
                            SearchIndicators = new List<string>
                            {
                                "32long",
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "32 S&W",
                            SearchIndicators = new List<string>
                            {
                                "32sw",
                                "32s&w",
                                "32s&amp;w"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "32 H&R Magnum",
                            SearchIndicators = new List<string>
                            {
                                "32hr",
                                "32h&r",
                                "32h&amp;r"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "327 Magnum",
                            SearchIndicators = new List<string>
                            {
                                "327fed",
                                "327mag"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "357 Magnum",
                            SearchIndicators = new List<string>
                            {
                                "357mag",
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "357 Sig",
                            SearchIndicators = new List<string>
                            {
                                "357sig"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "38 S&W",
                            SearchIndicators = new List<string>
                            {
                                "38sw",
                                "38s&w",
                                "38s&amp;w"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "38 Special",
                            SearchIndicators = new List<string>
                            {
                                "38special",
                                "38spl"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "38 Super",
                            SearchIndicators = new List<string>
                            {
                                "38super"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "38 Long Colt",
                            SearchIndicators = new List<string>
                            {
                                "38lc",
                                "38longcolt",
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "380 ACP",
                            SearchIndicators = new List<string>
                            {
                                "380acp",
                                "380auto"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "40 S&W",
                            SearchIndicators = new List<string>
                            {
                                "40sw",
                                "40s&w",
                                "40s&amp;w"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "41 Rem Magnum",
                            SearchIndicators = new List<string>
                            {
                                "41remmag"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "44 Magnum",
                            SearchIndicators = new List<string>
                            {
                                "44mag",
                                "44rem"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "44 Special",
                            SearchIndicators = new List<string>
                            {
                                "44special",
                                "44spl"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "45 ACP",
                            SearchIndicators = new List<string>
                            {
                                "45acp",
                                "45auto"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "45 GAP",
                            SearchIndicators = new List<string>
                            {
                                "45gap"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "45 Long Colt",
                            SearchIndicators = new List<string>
                            {
                                "45lc",
                                "45longcolt"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "454 Casull",
                            SearchIndicators = new List<string>
                            {
                                "454casull"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "460 S&W",
                            SearchIndicators = new List<string>
                            {
                                "460sw",
                                "460s&w",
                                "460s&amp;w"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "480 Ruger",
                            SearchIndicators = new List<string>
                            {
                                "480"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "5.7x28",
                            SearchIndicators = new List<string>
                            {
                                "5.7x28"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "50 AE",
                            SearchIndicators = new List<string>
                            {
                                "50ae"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "500 S&W",
                            SearchIndicators = new List<string>
                            {
                                "500sw",
                                "500s&w",
                                "500s&amp;w"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "7.62x25 Tokarev",
                            SearchIndicators = new List<string>
                            {
                                "7.62x25tokarev"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "9mm",
                            SearchIndicators = new List<string>
                            {
                                "9mm",
                                "!5.45x39",
                                "!7.62x39"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "9x18 Makarov",
                            SearchIndicators = new List<string>
                            {
                                "9x18makarov"
                            }
                        },
                    new SearchCriteria
                        {
                            Name = "10mm",
                            SearchIndicators = new List<string>
                            {
                                "10mm"
                            }
                        }
                });

            Add("Shotgun", new List<SearchCriteria>
            {
                new SearchCriteria
                    {
                        Name = "10 Gauge",
                        SearchIndicators = new List<string>
                        {
                            "10ga"
                        }
                    },
                new SearchCriteria
                    {
                        Name = "12 Gauge",
                        SearchIndicators = new List<string>
                        {
                            "12ga"
                        }
                    },
                new SearchCriteria
                    {
                        Name = "16 Gauge",
                        SearchIndicators = new List<string>
                        {
                            "16ga"
                        }
                    },
                new SearchCriteria
                    {
                        Name = "20 Gauge",
                        SearchIndicators = new List<string>
                        {
                            "20ga"
                        }
                    },
                new SearchCriteria
                    {
                        Name = "28 Gauge",
                        SearchIndicators = new List<string>
                        {
                            "28ga"
                        }
                    },
                new SearchCriteria
                    {
                        Name = "410",
                        SearchIndicators = new List<string>
                        {
                            "410"
                        }
                    }
                });

            Add("Rifle", new List<SearchCriteria>
            {
                 new SearchCriteria
                    {
                        Name = "17 Hornet",
                        SearchIndicators = new List<string>
                        {
                            "17hornet"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "17rem",
                        SearchIndicators = new List<string>
                        {
                            "17rem"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "204 Ruger",
                        SearchIndicators = new List<string>
                        {
                            "204ruger"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "22 Hornet",
                        SearchIndicators = new List<string>
                        {
                            "22hornet"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "22-250",
                        SearchIndicators = new List<string>
                        {
                            "22-250"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "220 Swift",
                        SearchIndicators = new List<string>
                        {
                            "220swift"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "222 Rem",
                        SearchIndicators = new List<string>
                        {
                            "222rem"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "223",
                        SearchIndicators = new List<string>
                        {
                            "223"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "224",
                        SearchIndicators = new List<string>
                        {
                            "224"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "243 Win",
                        SearchIndicators = new List<string>
                        {
                            "243win"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "243 WSSM",
                        SearchIndicators = new List<string>
                        {
                            "243wssm"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "25-06",
                        SearchIndicators = new List<string>
                        {
                            "25-06"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "260 Rem",
                        SearchIndicators = new List<string>
                        {
                            "260rem"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "270 Win",
                        SearchIndicators = new List<string>
                        {
                            "270win",
                            "270wsmwin"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "280 Rem",
                        SearchIndicators = new List<string>
                        {
                            "280rem"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "30 Carbine",
                        SearchIndicators = new List<string>
                        {
                            "30carbine",
                            "30calcarbine"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "30-06",
                        SearchIndicators = new List<string>
                        {
                            "30-06"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "30-30",
                        SearchIndicators = new List<string>
                        {
                            "30-30"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "30-40",
                        SearchIndicators = new List<string>
                        {
                            "30-40"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "300 AAC",
                        SearchIndicators = new List<string>
                        {
                            "300aac",
                            "300black",
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "300 Rem",
                        SearchIndicators = new List<string>
                        {
                            "300rem"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "300 Win Mag",
                        SearchIndicators = new List<string>
                        {
                            "300win",
                            "300winchestermag"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "300 WSM",
                        SearchIndicators = new List<string>
                        {
                            "300wsm"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "303 Brit",
                        SearchIndicators = new List<string>
                        {
                            "303brit"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "307 Win",
                        SearchIndicators = new List<string>
                        {
                            "307win"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "308",
                        SearchIndicators = new List<string>
                        {
                            "308"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "32 Win Special",
                        SearchIndicators = new List<string>
                        {
                            "32winspc",
                            "32winchesterspecial"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "338 Lapua",
                        SearchIndicators = new List<string>
                        {
                            "338lapua"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "338 Win Mag",
                        SearchIndicators = new List<string>
                        {
                            "338winmag"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "350 Legend",
                        SearchIndicators = new List<string>
                        {
                            "350legend",
                            "350"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "356 Win",
                        SearchIndicators = new List<string>
                        {
                            "356win"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "375 H&H",
                        SearchIndicators = new List<string>
                        {
                            "375hh",
                            "375h&h",
                            "375h&amp;h"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "38-55",
                        SearchIndicators = new List<string>
                        {
                            "38-55"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "44-40",
                        SearchIndicators = new List<string>
                        {
                            "44-40"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "45-70",
                        SearchIndicators = new List<string>
                        {
                            "45-70"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "450 Bushmaster",
                        SearchIndicators = new List<string>
                        {
                            "450Bushmaster"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "458 Socom",
                        SearchIndicators = new List<string>
                        {
                            "458socom"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "5.45x39",
                        SearchIndicators = new List<string>
                        {
                            "5.45x39"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "5.56",
                        SearchIndicators = new List<string>
                        {
                            "5.56"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "50 Beowulf",
                        SearchIndicators = new List<string>
                        {
                            "50beowulf"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "50 BMG",
                        SearchIndicators = new List<string>
                        {
                            "50calbmg",
                            "50bmg"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "6.5 Creedmoor",
                        SearchIndicators = new List<string>
                        {
                            "6.5creedmoor",
                            "6.5mmcreedmoor",
                            "6.5x55creedmoor"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "6.5 Grendel",
                        SearchIndicators = new List<string>
                        {
                            "6.5grendel",
                            "6.5mmgrendel",
                            "6.5x55grendel"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "6.5 Japanese",
                        SearchIndicators = new List<string>
                        {
                            "6.5japanese",
                            "6.5mmjapanese",
                            "6.5x55japanese"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "6.5 Swedish",
                        SearchIndicators = new List<string>
                        {
                            "6.5swedish",
                            "6.5mmswedish",
                            "6.5swedish",
                            "6.5mmswedish",
                            "6.5swede",
                            "6.5mmswede",
                            "6.5x55swede"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "6.8 SPC",
                        SearchIndicators = new List<string>
                        {
                            "6.8spc",
                            "6.8remspc",
                            "6.8remingtonspc"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "7.5 French",
                        SearchIndicators = new List<string>
                        {
                            "7.5french"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "7.5 Swiss",
                        SearchIndicators = new List<string>
                        {
                            "7.5swiss"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "7.62x39",
                        SearchIndicators = new List<string>
                        {
                            "7.62x39"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "7.62x54R",
                        SearchIndicators = new List<string>
                        {
                            "7.62x54r"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "7x64mm",
                        SearchIndicators = new List<string>
                        {
                            "7x64mm"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "7mm Mauser",
                        SearchIndicators = new List<string>
                        {
                            "7mmmauser",
                            "7x57mmmauser"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "7mm Mag",
                        SearchIndicators = new List<string>
                        {
                            "7mmmag",
                            "7mmremingtonmagnum"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "7mm-08",
                        SearchIndicators = new List<string>
                        {
                            "7mm-08"
                        }
                    },
                 new SearchCriteria
                    {
                        Name = "8mm Mauser",
                        SearchIndicators = new List<string>
                        {
                            "8mmmauser",
                            "8mmmaus"
                        }
                    }
                });
        }
    }
}
