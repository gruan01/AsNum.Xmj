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
    /// 获取当前用户下与当前用户建立消息关系的列表
    /// </summary>
    public class MessageRelationList : MethodBase<IEnumerable<MessageRelation>> {
        protected override string APIName {
            get {
                return "api.queryMsgRelationList";
            }
        }

        private int page = 1;
        [Param("currentPage", Required = true)]
        public int Page {
            get {
                return this.page;
            }
            set {
                this.page = value < 1 ? 1 : value;
            }
        }

        private int pageSize = 100;
        [Param("pageSize", Required = true)]
        public int PageSize {
            get {
                return this.pageSize;
            }
            set {
                this.pageSize = value > 5000 ? 5000 : (value < 1 ? 1 : value);
            }
        }

        [EnumParam("msgSources", EnumUseNameOrValue.Name, Required = true)]
        public MessageTypes Type { get; set; }

        [Param("filter")]
        public string Filter { get; set; }

        public bool UnReaded {
            set {
                this.Filter = value ? "dealStat/readStat/rank0/rank1/rank2/rank3/rank4/rank5" : null;
            }
        }

        public async override Task<IEnumerable<MessageRelation>> Execute(Auth auth) {
            var result = await this.GetResult(auth);
            var o = new {
                result = new List<MessageRelation>()
            };

            o = JsonConvert.DeserializeAnonymousType(result, o);
            if (o.result != null)
                o.result.ForEach(d => d.Account = auth.User);
            return o.result ?? Enumerable.Empty<MessageRelation>();
        }
    }
}
