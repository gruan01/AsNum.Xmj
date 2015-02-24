using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    public class SuccinctOrder {

        /// <summary>
        /// <remarks>
        /// API只返回一个sellerSignerFullname，并没有返回这个订单所对应的账户。这个属性是用于辅助。
        /// </remarks>
        /// </summary>
        public string Account {
            get;
            set;
        }

        [JsonProperty("orderId")]
        public string OrderID {
            get;
            set;
        }

        [JsonProperty("orderStatus")]
        public OrderStatus OrderStatus {
            get;
            set;
        }

        /// <summary>
        /// 订单类型
        /// </summary>
        [JsonProperty("BizType")]
        public string BizType {
            get;
            set;
        }

        [JsonProperty("buyerLoginId")]
        public string BuyerID {
            get;
            set;
        }

        [JsonProperty("buyerSignerFullname")]
        public string Buyer {
            get;
            set;
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
        [JsonProperty("frozenStatus")]
        public string FrozenStatus {
            get;
            set;
        }

        /// <summary>
        /// 资金状态
        /// </summary>
        [JsonProperty("fundStatus")]
        public string FundStatus {
            get;
            set;
        }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        [JsonProperty("gmtCreate"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime CreateOn {
            get;
            set;
        }

        /// <summary>
        /// 付款时间
        /// </summary>
        [JsonProperty("gmtPayTime"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime? PayTime {
            get;
            set;
        }

        /// <summary>
        /// 是否请求放款
        /// </summary>
        [JsonProperty("hasRequestLoan")]
        public bool HasRequestLoan {
            get;
            set;
        }

        /// <summary>
        /// 纠纷状态
        /// </summary>
        [JsonProperty("issueStatus"), JsonConverter(typeof(IssueConverter))]
        public bool InIssue {
            get;
            set;
        }

        [JsonProperty("payAmount")]
        public Amount Amount {
            get;
            set;
        }

        /// <summary>
        /// 付款类型
        /// </summary>
        [JsonProperty("paymentType")]
        public string PaymentType {
            get;
            set;
        }

        [JsonProperty("productList")]
        public List<OrderProduct> Products {
            get;
            set;
        }

        [JsonProperty("sellerSignerFullname")]
        public string Seller {
            get;
            set;
        }

        /// <summary>
        /// 毫秒
        /// </summary>
        [JsonProperty("timeoutLeftTime")]
        public long TimeoutLeftTime {
            get;
            set;
        }

        /// <summary>
        /// 剩余时间(未发货时为发货剩余时间，待收货时，为剩余收货时间)
        /// </summary>
        public TimeSpan OutLeftTime {
            get {
                return TimeSpan.FromMilliseconds(this.TimeoutLeftTime);
            }
        }
    }
}
