// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using DigitalPrice_Json;
//
//    var DigitalPriceConvert = DallarSerialization.FromJson(jsonString);

namespace DigitalPrice_Json
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class DallarSerialization
    {
        [J("url")] public string Url { get; set; }
        [J("mini_currency")] public string MiniCurrency { get; set; }
        [J("currency")] public string Currency { get; set; }
        [J("base_currency")] public string BaseCurrency { get; set; }
        [J("volume")] public long Volume { get; set; }
        [J("volume_market")] public decimal VolumeMarket { get; set; }
        [J("price")] public decimal Price { get; set; }
        [J("price_change")] public string PriceChange { get; set; }
        [J("class_change")] public string ClassChange { get; set; }
        [J("low")] public decimal? Low { get; set; }
        [J("high")] public decimal? High { get; set; }
    }

    public partial class DallarSerialization
    {
        public static DallarSerialization[] FromJson(string json) => JsonConvert.DeserializeObject<DallarSerialization[]>(json, DigitalPrice_Json.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DallarSerialization[] self) => JsonConvert.SerializeObject(self, DigitalPrice_Json.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
