using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class Receiver {

        /// <summary>
        /// 收件人
        /// </summary>
        [JsonProperty("contactPerson")]
        public string ContactPerson { get; set; }

        [JsonProperty("zip")]
        public string ZipCode { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("detailAddress")]
        public string Address { get; set; }

        [JsonProperty("country")]
        public string CountryCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("phoneNumber")]
        public string Phone { get; set; }

        [JsonProperty("phoneArea")]
        public string PhoneArea { get; set; }

        [JsonProperty("phoneCountry")]
        public string PhoneCountry {
            get;
            set;
        }

        [JsonProperty("mobileNo")]
        public string Mobi { get; set; }
    }
}
