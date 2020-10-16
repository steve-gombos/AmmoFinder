using AmmoFinder.Common.Interfaces;
using AmmoFinder.Parsers.Models;

namespace AmmoFinder.Parsers
{
    public class BrandParser : IDataParser
    {
        public string Parse(string description)
        {
            var brands = typeof(Brands).GetFields();

            foreach (var brand in brands)
            {
                if (brand.GetValue(brand).GetType() != typeof(string))
                    continue;

                var indicator = (string)brand.GetValue(brand);
                var descriptionLower = description.ToLower().Replace(" ", "");

                if (!descriptionLower.Contains(indicator.ToLower().Replace(" ", "")))
                {
                    //Check Variances
                    if (!Brands.Variances.ContainsKey(indicator))
                        continue;

                    foreach (var variance in Brands.Variances[indicator])
                    {
                        if (!descriptionLower.Contains(variance.ToLower().Replace(" ", "")))
                            continue;

                        return indicator;
                    }

                    continue;
                }


                return indicator;
            }

            return null;
        }
    }
}
