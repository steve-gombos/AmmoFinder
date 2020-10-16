using AmmoFinder.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            var sb = new StringBuilder();
            if (isLeft)
            {
                for (var i = sub.Length - 1; i >= 0; i--)
                {
                    if (char.IsWhiteSpace(sub[i]) && !string.IsNullOrWhiteSpace(sb.ToString()))
                        break;

                    if (char.IsDigit(sub[i]))
                        sb.Append(sub[i]);
                }

                if (string.IsNullOrWhiteSpace(sb.ToString()))
                    return null;

                var reverseCopy = sb.ToString().Reverse();
                sb.Clear();
                sb.Append(string.Join("", reverseCopy));
            }
            else
            {
                for (var i = 0; i < sub.Length; i++)
                {
                    if (char.IsWhiteSpace(sub[i]) && !string.IsNullOrWhiteSpace(sb.ToString()))
                        break;

                    if (char.IsDigit(sub[i]))
                        sb.Append(sub[i]);
                }
            }
            //var numeric = string.Join("", sub.Where(char.IsDigit).ToArray());
            var result = int.TryParse(sb.ToString(), out int intValue);
            if (result && intValue % 5 == 0)
                return intValue.ToString();

            return null;
        }
    }
}
