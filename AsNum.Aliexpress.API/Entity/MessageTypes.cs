using AsNum.Xmj.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Entity {
    public enum MessageTypes {
        /// <summary>
        /// 消息
        /// </summary>
        [SpecifyNameValue(Name = "message_center")]
        MessageCenter,

        /// <summary>
        /// 订单留言
        /// </summary>
        [SpecifyNameValue(Name = "order_msg")]
        Order
    }
}
