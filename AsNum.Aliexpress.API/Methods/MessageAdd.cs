using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {

    /// <summary>
    /// 新增站内信/订单留言
    /// </summary>
    public class MessageAdd : MethodBase<object> {
        protected override string APIName {
            get {
                return "api.addMsg";
            }
        }

        [Param("channelId")]
        public string ChannelID { get; set; }

        [Param("buyerId", Required = true)]
        public string BuyerID { get; set; }

        [Param("content", Required = true)]
        public string Ctx { get; set; }

        [EnumParam("msgSources", EnumUseNameOrValue.Name)]
        public MessageTypes Type { get; set; }
    }
}
