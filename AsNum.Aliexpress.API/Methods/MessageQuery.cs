using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Methods {
    /// <summary>
    /// 查询站内信
    /// </summary>
    public class MessageQuery : MethodBase<Paged<Message2>> {
        protected override string APIName {
            get {
                return "api.queryMessageList";
            }
        }

        [DateTimeParam("startTime", "MM/dd/yyyy HH:mm:ss")]
        public DateTime? StartTime {
            get;
            set;
        }

        [DateTimeParam("startTime", "MM/dd/yyyy HH:mm:ss")]
        public DateTime? EndTime {
            get;
            set;
        }

        [Param("buyerId")]
        public string BuyerID {
            get;
            set;
        }

        [Param("currentPage")]
        public int? CurrPage {
            get;
            set;
        }

        [Param("pageSize")]
        public int? PageSize {
            get;
            set;
        }

        [NeedAuth]
        public override Paged<Message2> Execute(Auth auth) {
            var str = this.GetResult(auth);
            var o = new {
                total = 0,
                msgList = new List<Message2>()
            };
            o = JsonConvert.DeserializeAnonymousType(this.ResultString, o);

            o.msgList.ForEach(m => {
                m.Account = auth.User;
            });

            return new Paged<Message2>() {
                CurrPage = this.CurrPage != null ? this.CurrPage.Value : 1,
                Results = o.msgList,
                Total = o.total
            };
        }
    }
}
