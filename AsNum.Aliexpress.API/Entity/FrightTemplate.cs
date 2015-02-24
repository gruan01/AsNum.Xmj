using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class FreightTemplate {

        [JsonProperty("templateId")]
        public int ID { get; set; }

        [JsonProperty("default")]
        public bool IsDefault { get; set; }

        [JsonProperty("templateName")]
        public string Name { get; set; }
    }
}
