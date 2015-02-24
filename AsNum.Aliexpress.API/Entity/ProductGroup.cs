using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class ProductGroup {

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
