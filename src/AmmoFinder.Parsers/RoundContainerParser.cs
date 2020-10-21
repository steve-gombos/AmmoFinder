using AmmoFinder.Common.Interfaces;
using AmmoFinder.Parsers.Models;
using System.Collections.Generic;
using System.Linq;

namespace AmmoFinder.Parsers
{
    public class RoundContainerParser : IDataParser
    {
        public List<string> SearchIndicators => new List<string>
        {
            "box",
            "can",
            "case"
        };

        public string Parse(string description)
        {
            var containers = typeof(RoundContainers).GetFields().Where(x => (x.GetValue(x) is string)).Select(x => (string)x.GetValue(x));

            foreach (var container in containers)
            {
                var descriptionLower = description.ToLower().Replace(" ", "");

                if (!descriptionLower.Contains(container.ToLower().Replace(" ", "")))
                    continue;

                return container;
            }

            return null;
        }
    }
}
