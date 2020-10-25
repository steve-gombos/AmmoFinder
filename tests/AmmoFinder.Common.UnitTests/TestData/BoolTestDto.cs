using System.Text.Json.Serialization;

namespace AmmoFinder.Common.UnitTests.TestData
{
    public class BoolTestDto
    {
        [JsonPropertyName("value")]
        [JsonConverter(typeof(BoolConverter))]
        public bool Value { get; set; }
    }
}
