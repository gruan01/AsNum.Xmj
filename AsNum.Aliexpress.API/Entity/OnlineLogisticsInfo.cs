using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    /// <summary>
    /// API getOnlineLogisticsInfo 返回结果
    /// </summary>
    public class OnlineLogisticsInfo {

        [JsonProperty("logisticsStatus")]
        public OnlineLogisticStatus Status { get; set; }

        [JsonProperty("internationalLogisticsType")]
        public string LogisticType { get; set; }

        [JsonProperty("internationallogisticsId")]
        public string TrackNO { get; set; }

        [JsonProperty("orderId")]
        public string OrderID { get; set; }

        [JsonProperty("onlineLogisticsId")]
        public string LogisticID { get; set; }
    }
}
