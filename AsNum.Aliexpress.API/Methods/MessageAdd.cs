using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {

    /// <summary>
    /// 新增站内信/订单留言
    /// </summary>
    public class MessageAdd : MethodBase<NormalResult> {
        protected override string APIName {
            get {
                return "api.addMsg";
            }
        }

        /// <summary>
        /// 如果为订单留言， 则为订单号
        /// </summary>
        [Param("channelId")]
        public string ChannelID { get; set; }

        [Param("buyerId", Required = true)]
        public string BuyerID { get; set; }

        [Param("content", Required = true)]
        public string Ctx { get; set; }

        [EnumParam("msgSources", EnumUseNameOrValue.Name)]
        public MessageTypes Type { get; set; }

        public override async Task<NormalResult> Execute(Auth auth) {
            var result = await this.GetResult(auth);

            var o = new { result = new { isSuccess = false, errorCode = 0, errorMsg = "" } };
            o = JsonConvert.DeserializeAnonymousType(result, o);
            return new NormalResult() {
                ErrorCode = o.result.errorCode.ToString(),
                ErrorInfo = o.result.errorMsg,
                Success = o.result.isSuccess
            };
        }
    }
}
