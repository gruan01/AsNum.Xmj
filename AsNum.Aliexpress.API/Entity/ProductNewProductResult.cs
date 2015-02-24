using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class ProductNewProductResult {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("productId")]
        public string ProductID { get; set; }
    }
}
