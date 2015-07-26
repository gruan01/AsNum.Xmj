using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Entity {
    public class Issue {

        [JsonProperty("id")]
        public string IssueID { get; set; }

        [JsonProperty("orderId")]
        public string OrderNO { get; set; }

        [JsonProperty("gmtModified"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime ModifiedOn { get; set; }

        [JsonProperty("issueStatus")]
        public IssueStatus Status { get; set; }

        [JsonProperty("gmtCreate"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime CreateOn { get; set; }


        [JsonProperty("reasonChinese")]
        public string ReasonCn { get; set; }

        [JsonProperty("reasonEnglish")]
        public string ReasonEn { get; set; }
    }
}
