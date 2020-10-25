using System.Text.Json.Serialization;

namespace AmmoFinder.Common.UnitTests.TestData
{
    public class LongTestDto
    {
        [JsonPropertyName("value")]
        [JsonConverter(typeof(LongConverter))]
        public long Value { get; set; }
    }

    public class NullableLongTestDto
    {
        [JsonPropertyName("value")]
        [JsonConverter(typeof(NullableLongConverter))]
        public long? Value { get; set; }
    }
}
