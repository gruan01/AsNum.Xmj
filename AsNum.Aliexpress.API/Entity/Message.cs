using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API.Entity {
    /// <summary>
    /// 订单留言
    /// </summary>
    public class Message {

        [JsonProperty("id")]
        public int ID {
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

        [JsonProperty("gmtCreate"), JsonConverter(typeof(AliDateTimeConverter))]
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

        [JsonProperty("receiverName")]
        public string Receiver {
            get;
            set;
        }

        [JsonProperty("senderLoginId")]
        public string SenderID {
            get;
            set;
        }

        [JsonProperty("receiverLoginId")]
        public string ReceiverID {
            get;
            set;
        }
    }
}
