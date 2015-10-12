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
    /// 站内信/订单留言查询详情列表
    /// </summary>
    public class MessageDetailList : MethodBase<IEnumerable<MessageDetail>> {
        protected override string APIName {
            get {
                return "api.queryMsgDetailList";
            }
        }

        [Param("currentPage", Required = true)]
        public int Page { get; set; }

        [Param("pageSize", Required = true)]
        public int PageSize { get; set; }

        [Param("channelId", Required = true)]
        public string ChannelID { get; set; }

        [EnumParam("msgSources", EnumUseNameOrValue.Name, Required = true)]
        public MessageTypes Type { get; set; }

        public MessageDetailList() {
            this.PageSize = 50;
        }

        public async override Task<IEnumerable<MessageDetail>> Execute(Auth auth) {
            var result = await this.GetResult(auth);

            var o = new {
                result = new List<MessageDetail>()
            };

            o = JsonConvert.DeserializeAnonymousType(result, o);
            return o.result;
        }
    }
}
