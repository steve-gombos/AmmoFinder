using AmmoFinder.Common.Interfaces;
using AmmoFinder.Parsers.Models;
using System.Linq;

namespace AmmoFinder.Parsers
{
    public class RoundContainerParser : IDataParser
    {
        public string Parse(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return null;
            }

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
