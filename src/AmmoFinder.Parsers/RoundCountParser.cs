using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Interfaces;
using System.Collections.Generic;

namespace AmmoFinder.Parsers
{
    public class RoundCountParser : IDataParser
    {
        public const int VARIANCE = 6;
        public List<string> SearchIndicators => new List<string>
        {
            "round",
            "rd",
            "rnd",
            "box of",
            //"per",
            "box"
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
                        .LeftFromIndex(index, VARIANCE)
                        .Trim().GetDigitsUntilWhiteSpace();

                    var right = descriptionLowered
                        .RightFromIndex(index + indicator.Length, VARIANCE)
                        .Trim().GetDigitsUntilWhiteSpace(false);

                    if (left != null && int.Parse(left) % 5 == 0)
                        return left;

                    if (right != null && int.Parse(right) % 5 == 0)
                        return right;

                    descriptionLowered = descriptionLowered.Substring(index + indicator.Length);
                }

            }

            return null;
        }
    }
}
