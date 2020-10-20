using System.Collections.Generic;
using Xunit;

namespace AmmoFinder.Common.UnitTests
{
    public class ExtensionTestData : TheoryData<string, IEnumerable<string>, bool>
    {
        public ExtensionTestData()
        {
            Add("1 2 3", new List<string> { "1", "2" }, true);
            Add("1 2 3", new List<string> { "4", "5" }, false);
        }
    }
}
