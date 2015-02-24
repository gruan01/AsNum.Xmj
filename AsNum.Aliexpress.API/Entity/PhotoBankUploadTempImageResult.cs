using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class PhotoBankUploadTempImageResult {

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
