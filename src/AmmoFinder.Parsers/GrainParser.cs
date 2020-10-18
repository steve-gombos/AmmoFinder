using AmmoFinder.Common.Extensions;
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

            //foreach (var indicator in SearchIndicators)
            //{
            //    if (!descriptionLower.Contains(indicator))
            //        continue;

            //    var index = descriptionLower.IndexOf(indicator);
            //    var i = index;
            //    while (!char.IsWhiteSpace(descriptionLower[i]))
            //    {
            //        i--;
            //    }

            //    var substring = descriptionLower.Substring(i, index - i).Trim();

            //    return substring;
            //}

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
