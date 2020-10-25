using AmmoFinder.Common.Models;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AmmoFinder.Common.Comparers
{
    public class ProductModelComparer : IEqualityComparer<ProductModel>
    {
        public bool Equals([AllowNull] ProductModel x, [AllowNull] ProductModel y)
        {
            return x.RetailerProductId == y.RetailerProductId;
        }

        public int GetHashCode([DisallowNull] ProductModel obj)
        {
            return obj.RetailerProductId.GetHashCode();
        }
    }
}
