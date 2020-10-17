using AmmoFinder.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmmoFinder.Common.Interfaces
{
    public interface IProductService
    {
        string Retailer { get; }
        Task<IEnumerable<ProductModel>> Fetch();
    }
}
