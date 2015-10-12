using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Entity {
    public class MessageRelation {

        [JsonProperty("lastMessageId")]
        public string LastMessageID { get; set; }

        [JsonProperty("otherName")]
        public string Customer { get; set; }

        [JsonProperty("otherLoginId")]
        public string CustomerID { get; set; }


        [JsonProperty("rank")]
        public MessageRanks Rank { get; set; }

        /// <summary>
        /// 通道ID，即关系ID(订单留言为订单号，站内信为站内信的关系ID)
        /// </summary>
        [JsonProperty("channelId")]
        public string ChannelID { get; set; }

        [JsonProperty("dealStat"), JsonConverter(typeof(BoolConverter))]
        public bool Dealeded { get; set; }

        [JsonProperty("readStat"), JsonConverter(typeof(BoolConverter))]
        public bool Readed { get; set; }

        [JsonProperty("messageTime"), JsonConverter(typeof(UnixTimeStampToDateTimeConverter))]
        public DateTime LastMessageCreateOn { get; set; }

        /// <summary>
        /// 未读数
        /// </summary>
        [JsonProperty("unreadCount")]
        public int UnReadCount { get; set; }

        /// <summary>
        /// 最后一条消息是否自己这边发送(true是，false否)
        /// </summary>
        [JsonProperty("lastMessageIsOwn"), JsonConverter(typeof(BoolConverter))]
        public bool LastMessageIsOwn { get; set; }


        [JsonProperty("lastMessageContent")]
        public string LastMessageContent { get; set; }

        public string Account { get; set; }
    }

    public enum MessageRanks {

        //标签值(0,1,2,3,4,5)依次表示为白，红，橙，绿，蓝，紫

        White = 0,
        Red = 1,
        Orange = 2,
        Green = 3,
        Blue = 4,
        Purple = 5
    }
}
