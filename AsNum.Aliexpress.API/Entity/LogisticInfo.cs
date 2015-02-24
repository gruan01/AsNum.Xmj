using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API.Entity {
    /// <summary>
    /// 物流信息
    /// </summary>
    public class LogisticInfo {

        [JsonProperty("LogisticsTypeCode")]
        public string TypeCode {
            get;
            set;
        }

        /// <summary>
        /// 物流方式
        /// </summary>
        [JsonProperty("logisticsServiceName")]
        public string Type {
            get;
            set;
        }

        [JsonProperty("logisticsNo")]
        public string TrackingNo {
            get;
            set;
        }

        [JsonProperty("gmtSend"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime? SendOn {
            get;
            set;
        }
    }
}
