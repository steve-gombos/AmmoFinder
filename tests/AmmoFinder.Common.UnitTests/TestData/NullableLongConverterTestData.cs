using Xunit;

namespace AmmoFinder.Common.UnitTests.TestData
{
    public class NullableLongConverterTestData : TheoryData<NullableLongTestDto, string>
    {
        public NullableLongConverterTestData()
        {
            Add(new NullableLongTestDto() { Value = 123 }, "{\"value\":\"123\"}");
            Add(new NullableLongTestDto() { Value = null }, "{\"value\":null}");
            Add(new NullableLongTestDto(), "{\"value\":null}");
        }
    }
}
