using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AmmoFinder.Retailers.AimSurplus.Models
{
    [ExcludeFromCodeCoverage]
    internal partial class Response
    {
        [JsonPropertyName("page")]
        [JsonConverter(typeof(LongConverter))]
        public long Page { get; set; }

        [JsonPropertyName("pages")]

        public long Pages { get; set; }

        [JsonPropertyName("featured")]
        public object Featured { get; set; }

        [JsonPropertyName("suggestions")]
        public object[] Suggestions { get; set; }

        [JsonPropertyName("products")]
        public Product[] Products { get; set; }

        [JsonPropertyName("facets")]
        public Facet[] Facets { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }

        [JsonPropertyName("category")]
        public Category Category { get; set; }
    }

    [ExcludeFromCodeCoverage]
    internal partial class Category
    {
        [JsonPropertyName("parents")]
        public object[] Parents { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("children")]
        public Child[] Children { get; set; }
    }

    [ExcludeFromCodeCoverage]
    internal partial class Child
    {
        [JsonPropertyName("bc_id")]
        public long BcId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }
    }

    [ExcludeFromCodeCoverage]
    internal partial class Facet
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("handle")]
        public string Handle { get; set; }

        [JsonPropertyName("is_enabled")]
        public long IsEnabled { get; set; }

        [JsonPropertyName("category_id")]
        public long CategoryId { get; set; }

        [JsonPropertyName("position")]
        public long Position { get; set; }

        [JsonPropertyName("sort_by")]
        public string SortBy { get; set; }

        //[JsonPropertyName("created_at")]
        //[JsonConverter(typeof(DateTimeConverter))]
        //public DateTime? CreatedAt { get; set; }

        //[JsonPropertyName("updated_at")]
        //[JsonConverter(typeof(DateTimeConverter))]
        //public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("products")]
        public long Products { get; set; }

        [JsonPropertyName("options")]
        public Option[] Options { get; set; }
    }

    [ExcludeFromCodeCoverage]
    internal partial class Option
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("products")]
        public long Products { get; set; }

        [JsonPropertyName("chosen")]
        public bool Chosen { get; set; }
    }

    [ExcludeFromCodeCoverage]
    internal partial class Product
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("thumbnail")]
        public Uri Thumbnail { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("sale_price")]
        public string SalePrice { get; set; }

        [JsonPropertyName("total_sold")]
        public long TotalSold { get; set; }

        [JsonPropertyName("review_score")]
        public string ReviewScore { get; set; }

        [JsonPropertyName("review_count")]
        public long ReviewCount { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        //[JsonPropertyName("created_at")]
        //public CreatedAt CreatedAt { get; set; }

        [JsonPropertyName("inventory")]
        public long Inventory { get; set; }

        [JsonPropertyName("score")]
        public long Score { get; set; }
    }

    [ExcludeFromCodeCoverage]
    internal partial class CreatedAt
    {
        [JsonPropertyName("date")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonPropertyName("timezone_type")]
        public long TimezoneType { get; set; }

        [JsonPropertyName("timezone")]
        [JsonConverter(typeof(TimeZoneConverter))]
        public Timezone Timezone { get; set; }
    }

    internal enum Timezone { AmericaNewYork };

    [ExcludeFromCodeCoverage]
    internal class TimeZoneConverter : JsonConverter<Timezone>
    {
        public override Timezone Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return Timezone.AmericaNewYork;
            var value = reader.GetString();
            if (value == "America//New_York")
            {
                return Timezone.AmericaNewYork;
            }
            throw new Exception("Cannot unmarshal type Timezone");
        }

        public override void Write(Utf8JsonWriter writer, Timezone value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    //internal static class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters =
    //        {
    //            TimezoneConverter.Singleton,
    //            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //        },
    //    };
    //}

    //internal class TimezoneConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(Timezone) || t == typeof(Timezone?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        if (value == "America//New_York")
    //        {
    //            return Timezone.AmericaNewYork;
    //        }
    //        throw new Exception("Cannot unmarshal type Timezone");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (Timezone)untypedValue;
    //        if (value == Timezone.AmericaNewYork)
    //        {
    //            serializer.Serialize(writer, "America//New_York");
    //            return;
    //        }
    //        throw new Exception("Cannot marshal type Timezone");
    //    }

    //    public static readonly TimezoneConverter Singleton = new TimezoneConverter();
}
