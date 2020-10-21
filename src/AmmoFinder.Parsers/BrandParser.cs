using AmmoFinder.Common.Interfaces;
using AmmoFinder.Parsers.Models;
using System.Linq;

namespace AmmoFinder.Parsers
{
    public class BrandParser : IDataParser
    {
        public string Parse(string description)
        {
            var brands = typeof(Brands).GetFields().Where(x => (x.GetValue(x) is string)).Select(x => (string)x.GetValue(x));

            foreach (var brand in brands)
            {
                var descriptionLower = description.ToLower().Replace(" ", "");

                if (!descriptionLower.Contains(brand.ToLower().Replace(" ", "")))
                {
                    //Check Variances
                    if (!Brands.Variances.ContainsKey(brand))
                        continue;

                    foreach (var variance in Brands.Variances[brand])
                    {
                        if (!descriptionLower.Contains(variance.ToLower().Replace(" ", "")))
                            continue;

                        return brand;
                    }

                    continue;
                }


                return brand;
            }

            return null;
        }
    }
}
