using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class Amount {

        [JsonProperty("amount")]
        public decimal Total { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }
    }
}
