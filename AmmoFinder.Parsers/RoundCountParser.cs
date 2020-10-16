using AmmoFinder.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
            "per",
            "box"
        };

        public string Parse(string description)
        {
            foreach (var indicator in SearchIndicators)
            {
                var descriptionLowered = description.ToLower();

                while (descriptionLowered.Contains(indicator))
                {
                    var index = descriptionLowered.IndexOf(indicator);

                    var leftStart = index - VARIANCE > 0 ? index - VARIANCE : 0;
                    var rightStart = index + indicator.Length;
                    var rightTest = descriptionLowered.Length - rightStart;
                    var rightEnd = rightTest < VARIANCE ? rightTest : VARIANCE;

                    if (leftStart >= 0 && descriptionLowered.Length >= leftStart + VARIANCE)
                    {
                        var left = GetValue(descriptionLowered, leftStart, VARIANCE);

                        if (left != null)
                            return left;
                    }

                    if (descriptionLowered.Length >= rightStart + rightEnd)
                    {
                        var right = GetValue(descriptionLowered, rightStart, rightEnd, false);

                        if (right != null)
                            return right;
                    }

                    descriptionLowered = descriptionLowered.Substring(rightStart);
                }

            }

            return null;
        }

        private string GetValue(string description, int start, int end, bool isLeft = true)
        {
            var sub = description.Substring(start, end);

            string numeric = null;
            if (isLeft)
            {
                for (var i = sub.Length - 1; i >= 0; i--)
                {
                    if (char.IsWhiteSpace(sub[i]) && !string.IsNullOrWhiteSpace(numeric))
                        break;

                    if (char.IsDigit(sub[i]))
                        numeric += sub[i];
                }

                if (numeric == null)
                    return null;

                numeric = string.Join("", numeric.Reverse());
            }
            else
            {
                for (var i = 0; i < sub.Length; i++)
                {
                    if (char.IsWhiteSpace(sub[i]) && !string.IsNullOrWhiteSpace(numeric))
                        break;

                    if (char.IsDigit(sub[i]))
                        numeric += sub[i];
                }
            }
            //var numeric = string.Join("", sub.Where(char.IsDigit).ToArray());
            var result = int.TryParse(numeric, out int intValue);
            if (result && intValue % 5 == 0)
                return intValue.ToString();

            return null;
        }
    }
}
