using AmmoFinder.Common.Interfaces;
using System.Collections.Generic;

namespace AmmoFinder.Parsers
{
    public class GrainParser : IDataParser
    {
        public List<string> SearchIndicators => new List<string>
        {
            " grain",
            "grain",
            "grn",
            "gr",
        };

        public string Parse(string description)
        {
            var descriptionLower = description.ToLower();

            foreach (var indicator in SearchIndicators)
            {
                if (!descriptionLower.Contains(indicator))
                    continue;

                var index = descriptionLower.IndexOf(indicator);
                var i = index;
                while (!char.IsWhiteSpace(descriptionLower[i]))
                {
                    i--;
                }

                var substring = descriptionLower.Substring(i, index - i).Trim();

                return substring;
            }

            return null;
        }
    }
}
