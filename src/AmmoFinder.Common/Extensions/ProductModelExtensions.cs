using AmmoFinder.Common.Comparers;
using AmmoFinder.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace AmmoFinder.Common.Extensions
{
    public static class ProductModelExtensions
    {
        public static IEnumerable<ProductModel> DistinctProducts(this IEnumerable<ProductModel> products)
        {
            return products.Distinct(new ProductModelComparer());
        }
    }
}
