using AmmoFinder.Common.Interfaces;
using System.Collections.Generic;

namespace AmmoFinder.Parsers
{
    public class ProjectileTypeParser : IDataParser
    {
        public List<string> SearchIndicators => new List<string>
        {
            "hunting",
            "fmj",
            "self defense"
        };

        public string Parse(string description)
        {
            foreach (var indicator in SearchIndicators)
            {
                var descriptionLower = description.ToLower();

                if (!descriptionLower.Contains(indicator))
                    continue;

                return indicator;
            }

            return null;
        }
    }
}
