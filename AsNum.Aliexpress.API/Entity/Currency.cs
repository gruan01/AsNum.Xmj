using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class Currency {

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
