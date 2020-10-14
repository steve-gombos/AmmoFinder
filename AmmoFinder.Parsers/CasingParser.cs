using AmmoFinder.Common.Interfaces;
using System.Collections.Generic;

namespace AmmoFinder.Parsers
{
    public class CasingParser : IDataParser
    {
        public List<string> SearchIndicators => new List<string>
        {
            "brass-plated steel",
            "nickel-plated brass",
            "aluminum",
            "brass",
            "steel"
        };

        public string Parse(string description)
        {
            foreach (var indicator in SearchIndicators)
            {
                var descriptionLower = description.ToLower().Replace(" ", "");

                if (!descriptionLower.Contains(indicator.ToLower()))
                    continue;

                return indicator;
            }

            return null;
        }
    }
}
