using Newtonsoft.Json;

namespace Stock.Service.Models
{
    public class StooqResult
    {
        [JsonProperty("symbols")]
        public IEnumerable<SymbolModel>? Symbols { get; set; }

        public class SymbolModel
        {
            [JsonProperty("symbol")]
            public string? Symbol { get; set; }

            [JsonProperty("date")]
            public string? Date { get; set; }

            [JsonProperty("time")]
            public string? Time { get; set; }

            [JsonProperty("open")]
            public string? Open { get; set; }

            [JsonProperty("high")]
            public string? High { get; set; }

            [JsonProperty("low")]
            public string? Low { get; set; }

            [JsonProperty("close")]
            public string? Close { get; set; }

            [JsonProperty("volume")]
            public string? Volume { get; set; }
        }
    }
}