using AmmoFinder.Common.Comparers;
using AmmoFinder.Common.Models;
using AmmoFinder.Common.UnitTests.TestData;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AmmoFinder.Common.UnitTests
{
    public class ComparerTests
    {
        [Theory, ClassData(typeof(ComparerObjectTestData))]
        public void ProductEqualityComparer_Objects_IsValid(ProductModel product1, ProductModel product2, bool expected)
        {
            // Arrange
            var comparer = new ProductModelComparer();

            // Act
            var actual = comparer.Equals(product1, product2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory, ClassData(typeof(ComparerListTestData))]
        public void ProductEqualityComparer_List_IsValid(IEnumerable<ProductModel> list, int expectedLength)
        {
            // Arrange
            var comparer = new ProductModelComparer();

            // Act
            var actual = list.Distinct(comparer);

            // Assert
            Assert.Equal(expectedLength, actual.Count());
        }
    }
}
