using AmmoFinder.Common.Interfaces;
using AmmoFinder.Parsers.Models;

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
                    foreach (var indicator in caliber.SearchIndicators)
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
