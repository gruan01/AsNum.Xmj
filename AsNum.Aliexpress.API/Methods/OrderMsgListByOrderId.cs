using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Methods {

    [Obsolete("Use OrderMsgList instade", true)]
    public class OrderMsgListByOrderId : MethodBase<List<Message>> {

        protected override string APIName {
            get { return "api.queryOrderMsgListByOrderId"; }
        }

        [Param("orderId")]
        public string OrderID { get; set; }
    }
}
