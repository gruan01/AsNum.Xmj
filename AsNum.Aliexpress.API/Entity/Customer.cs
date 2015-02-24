using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class Customer {
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("loginId")]
        public string LoginID { get; set; }

        [JsonProperty("country")]
        public string CountryCode { get; set; }
    }
}
