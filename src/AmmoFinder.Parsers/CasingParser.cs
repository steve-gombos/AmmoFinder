using AmmoFinder.Common.Interfaces;
using AmmoFinder.Parsers.Models;
using System.Linq;

namespace AmmoFinder.Parsers
{
    public class CasingParser : IDataParser
    {
        public string Parse(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return null;
            }

            var casings = typeof(Casings).GetFields().Where(x => (x.GetValue(x) is string)).Select(x => (string)x.GetValue(x));

            foreach (var indicator in casings)
            {
                var descriptionLower = description.ToLower().Replace(" ", "");

                if (!descriptionLower.Contains(indicator.ToLower().Replace(" ", "")))
                    continue;

                return indicator;
            }

            return null;
        }
    }
}
