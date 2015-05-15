using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {
    /// <summary>
    /// 延长订单收货时间
    /// </summary>
    public class OrderExtendReceiveTime : MethodBase<NormalResult> {
        protected override string APIName {
            get { return "api.extendsBuyerAcceptGoodsTime"; }
        }

        [Param("orderId", Required = true)]
        public string OrderNO { get; set; }

        [Param("day")]
        public int Days { get; set; }
    }
}
