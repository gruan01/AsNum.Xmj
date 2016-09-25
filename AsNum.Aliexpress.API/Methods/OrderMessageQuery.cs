using AsNum.Xmj.API.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {

    /// <summary>
    /// 订单留言
    /// </summary>
    public class OrderMessageQuery : MethodBase<object> {
        protected override string APIName {
            get { return "api.queryOrderMsgList"; }
        }


        [DateTimeParam("startTime", "MM/dd/yyyy HH:mm:ss")]
        public DateTime? BeginAt { get; set; }

        [DateTimeParam("endTime", "MM/dd/yyyy HH:mm:ss")]
        public DateTime? EndAt { get; set; }

        [Param("orderId")]
        public string OrderNO { get; set; }

        [Param("buyerId")]
        public string BuyerID { get; set; }

        [Param("currentPage")]
        public int? PageIdx { get; set; }

        [Param("pageSize")]
        public int? PageSize { get; set; }
    }
}
