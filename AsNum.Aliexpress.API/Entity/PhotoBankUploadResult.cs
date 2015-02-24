using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class PhotoBankUploadResult {

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("status")]
        public string Stauts { get; set; }

        //photobankTotalSize : "1500.00"

        //fileName
        [JsonProperty("photobankUrl")]
        public string Url { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
