using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    /// <summary>
    /// 国内快递公司
    /// </summary>
    public class LocalLogisticsCompany {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("companyCode")]
        public string Code { get; set; }

        [JsonProperty("companyId")]
        public int ID { get; set; }
    }
}
