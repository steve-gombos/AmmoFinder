using System.Buffers;
using System.Buffers.Text;

namespace System.Text.Json.Serialization
{
    public class BoolConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                ReadOnlySpan<byte> span = reader.HasValueSequence ? reader.ValueSequence.ToArray() : reader.ValueSpan;
                if (Utf8Parser.TryParse(span, out bool boolean, out int bytesConsumed) && span.Length == bytesConsumed)
                    return boolean;

                if (Boolean.TryParse(reader.GetString(), out boolean))
                    return boolean;
            }

            return reader.GetBoolean();
        }

        public override void Write(Utf8JsonWriter writer, bool boolValue, JsonSerializerOptions options)
        {
            writer.WriteStringValue(boolValue.ToString());
        }
    }
}
