using AmmoFinder.Common.Models;
using System.Collections.Generic;
using Xunit;

namespace AmmoFinder.Common.UnitTests.TestData
{
    public class ComparerObjectTestData : TheoryData<ProductModel, ProductModel, bool>
    {
        public ComparerObjectTestData()
        {
            Add(new ProductModel { RetailerProductId = "Test" }, new ProductModel { RetailerProductId = "Test" }, true);
            Add(new ProductModel { RetailerProductId = "Test1" }, new ProductModel { RetailerProductId = "Test2" }, false);
            Add(new ProductModel { RetailerProductId = "Test1" }, new ProductModel { RetailerProductId = null }, false);
            Add(new ProductModel { RetailerProductId = null }, new ProductModel { RetailerProductId = "Test2" }, false);
        }
    }

    public class ComparerListTestData : TheoryData<IEnumerable<ProductModel>, int>
    {
        public ComparerListTestData()
        {
            Add(new List<ProductModel> { new ProductModel { RetailerProductId = "Test" }, new ProductModel { RetailerProductId = "Test" } }, 1);
            Add(new List<ProductModel> { new ProductModel { RetailerProductId = "Test1" }, new ProductModel { RetailerProductId = "Test2" } }, 2);
            Add(new List<ProductModel>(), 0);
        }
    }
}
