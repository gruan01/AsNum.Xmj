using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Methods {
    public class OrderMsgList : MethodBase<Paged<Message>> {

        protected override string APIName {
            get {
                return "api.queryOrderMsgList";
            }
        }

        [DateTimeParam("startTime", "MM/dd/yyyy HH:mm:ss")]
        public DateTime? StartTime {
            get;
            set;
        }

        [DateTimeParam("endTime", "MM/dd/yyyy HH:mm:ss")]
        public DateTime? EndTime {
            get;
            set;
        }

        [Param("orderId")]
        public string OrderID {
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
        public override Paged<Message> Execute(Auth auth) {
            var str = this.GetResult(auth);
            var o = new {
                total = 0,
                msgList = new List<Message>()
            };
            o = JsonConvert.DeserializeAnonymousType(this.ResultString, o);
            return new Paged<Message>() {
                CurrPage = this.CurrPage != null ? this.CurrPage.Value : 1,
                Results = o.msgList,
                Total = o.total
            };
        }
    }
}
