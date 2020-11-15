using AmmoFinder.Common.Interfaces;
using AmmoFinder.Parsers.Models;
using System.Linq;

namespace AmmoFinder.Parsers
{
    public class BulletTypeParser : IDataParser
    {
        public string Parse(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return null;
            }

            var bulletTypes = typeof(BulletTypes).GetFields().Where(x => (x.GetValue(x) is string)).Select(x => (string)x.GetValue(x));

            foreach (var bulletType in bulletTypes)
            {
                var descriptionLower = description.ToLower();

                if (!descriptionLower.Contains(bulletType.ToLower()))
                    continue;

                return bulletType;
            }

            return null;
        }
    }
}
