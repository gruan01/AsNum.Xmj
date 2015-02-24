﻿using AsNum.Xmj.API.Attributes;

namespace AsNum.Xmj.API.Methods {
    public class OrderNewMsg : MethodBase<string> {
        protected override string APIName {
            get {
                return "api.addOrderMessage";
            }
        }

        [Param("orderId")]
        public string OrderID {
            get;
            set;
        }

        [Param("content")]
        public string Content {
            get;
            set;
        }
    }
}