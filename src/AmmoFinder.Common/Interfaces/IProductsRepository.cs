using AmmoFinder.Common.Models;
using System.Collections.Generic;

namespace AmmoFinder.Common.Interfaces
{
    public interface IProductsRepository
    {
        IEnumerable<ProductModel> GetProducts();

        IEnumerable<RetailerModel> GetRetailers();
    }
}
