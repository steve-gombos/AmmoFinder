using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Interfaces;
using System.Collections.Generic;

namespace AmmoFinder.Parsers
{
    public class GrainParser : IDataParser
    {
        public List<string> SearchIndicators => new List<string>
        {
            "grain",
            "grn",
            "gr",
        };

        public string Parse(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return null;
            }

            foreach (var indicator in SearchIndicators)
            {
                var descriptionLowered = description.ToLower();

                while (descriptionLowered.Contains(indicator))
                {
                    var index = descriptionLowered.IndexOf(indicator);

                    var left = descriptionLowered
                        .LeftFromIndex(index, 6)
                        .Trim().GetDigitsUntilWhiteSpace();

                    var right = descriptionLowered
                        .RightFromIndex(index + indicator.Length, 6)
                        .Trim().GetDigitsUntilWhiteSpace(false);

                    if (left != null)
                        return left;

                    if (right != null)
                        return right;

                    descriptionLowered = descriptionLowered.Substring(index + indicator.Length);
                }

            }

            return null;
        }
    }
}
