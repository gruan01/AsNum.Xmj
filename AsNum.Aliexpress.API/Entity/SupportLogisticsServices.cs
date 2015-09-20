using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Entity {

    /// <summary>
    /// 平台所支持的物流服务
    /// </summary>
    public class SupportLogisticsServices {

        [JsonProperty("trackingNoRegex")]
        public string CheckRule { get; set; }

        [JsonProperty("logisticsCompany")]
        public string Company { get; set; }

        [JsonProperty("minProcessDay")]
        public int MinProcessDays { get; set; }

        [JsonProperty("maxProcessDay")]
        public int MaxProcessDays { get; set; }

        [JsonProperty("displayName")]
        public string Name { get; set; }


        [JsonProperty("serviceName")]
        public string Code { get; set; }
    }
}
