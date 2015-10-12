using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Entity {
    public class MessageDetail {

        [JsonProperty("id")]
        public long ID {
            get;
            set;
        }

        //api.queryOrderMsgList queryOrderMsgListByOrderId 这两个接口都有 typeID 这个数据.
        //[JsonProperty("orderId")]
        [JsonProperty("typeId")]
        public string OrderNO {
            get;
            set;
        }

        [JsonProperty("gmtCreate"), JsonConverter(typeof(UnixTimeStampToDateTimeConverter))]
        public DateTime CreateOn {
            get;
            set;
        }

        [JsonProperty("content")]
        public string Content {
            get;
            set;
        }

        [JsonProperty("senderName")]
        public string Sender {
            get;
            set;
        }

    }
}
