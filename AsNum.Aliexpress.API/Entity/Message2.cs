using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API.Entity {
    /// <summary>
    /// 站内信
    /// </summary>
    public class Message2 {

        [JsonProperty("id")]
        public long ID {
            get;
            set;
        }

        [JsonProperty("senderName")]
        public string Sender {
            get;
            set;
        }

        [JsonProperty("senderLoginId")]
        public string SenderID {
            get;
            set;
        }

        [JsonProperty("receiverName")]
        public string Receiver {
            get;
            set;
        }

        [JsonProperty("receiverLoginId")]
        public string ReceiverID {
            get;
            set;
        }

        [JsonProperty("gmtCreate"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime CreateOn {
            get;
            set;
        }

        [JsonProperty("haveFile")]
        public bool HaveFile {
            get;
            set;
        }

        [JsonProperty("fileUrl")]
        public string FileUrl {
            get;
            set;
        }

        [JsonProperty("productId")]
        public long ProductID {
            get;
            set;
        }

        [JsonProperty("productName")]
        public string ProductName {
            get;
            set;
        }

        [JsonProperty("orderId")]
        public string OrderNO {
            get;
            set;
        }

        [JsonProperty("content")]
        public string Content {
            get;
            set;
        }

        public string Account {
            get;
            set;
        }
    }
}
