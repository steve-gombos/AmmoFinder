using AmmoFinder.Common.Interfaces;
using System.Collections.Generic;

namespace AmmoFinder.Parsers
{
    public class BulletTypeParser : IDataParser
    {
        public List<string> SearchIndicators => new List<string>
        {
            "JHP", // Jacketed Hallow Point
            "FMJ", // Full Metal Jacket
            "SP", // Soft Point
            "HP", // Hallow Point
        };

        public string Parse(string description)
        {
            foreach (var indicator in SearchIndicators)
            {
                var descriptionLower = description.ToLower();

                if (!descriptionLower.Contains(indicator.ToLower()))
                    continue;

                return indicator;
            }

            return null;
        }
    }
}
