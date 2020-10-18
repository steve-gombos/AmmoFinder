using AmmoFinder.Common.Extensions;
using AmmoFinder.Common.Interfaces;
using AmmoFinder.Parsers.Models;
using System.Linq;

namespace AmmoFinder.Parsers
{
    public class CaliberParser : IDataParser
    {
        public string Parse(string description)
        {
            var descriptionLower = description.ToLower().Replace(" ", "");

            foreach (var firearmGrouping in new Calibers())
            {
                foreach (var caliber in firearmGrouping.Value)
                {
                    var excludedIndicators = caliber.SearchIndicators.Where(x => x.StartsWith("!")).Select(x => x.Replace("!", ""));
                    var includedIndicators = caliber.SearchIndicators.Where(x => !x.StartsWith("!"));

                    if (descriptionLower.ContainsAny(excludedIndicators))
                        continue;

                    foreach (var indicator in includedIndicators)
                    {
                        if (!descriptionLower.Contains(indicator.ToLower()))
                            continue;

                        return caliber.Name;
                    }
                }
            }

            return null;
        }
    }
}
