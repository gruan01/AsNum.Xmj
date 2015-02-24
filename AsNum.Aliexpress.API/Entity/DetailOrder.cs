using AsNum.Xmj.API.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    public class DetailOrder {

        [JsonProperty("id")]
        public string OrderNO { get; set; }

        [JsonProperty("orderStatus")]
        public OrderStatus Status {
            get;
            set;
        }

        /// <summary>
        /// 返回的是未调整之前的价钱, 如果订单有调整价格,这个就不对了.
        /// </summary>
        [JsonProperty("orderAmount")]
        public Amount OrderAmount { get; set; }

        [JsonProperty("receiptAddress")]
        public Receiver Receiver { get; set; }

        [JsonProperty("buyerInfo")]
        public Customer Customer { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        [JsonProperty("logisticsAmount")]
        public Amount LogisticsAmount { get; set; }

        //[JsonProperty("issueStatus")]
        //public string IssueStatus { get; set; }

        [JsonProperty("issueStatus"), JsonConverter(typeof(IssueConverter))]
        public bool InIssue {
            get;
            set;
        }

        [JsonProperty("frozenStatus")]
        public string FrozenStatus { get; set; }

        /// <summary>
        /// 物流信息
        /// </summary>
        [JsonProperty("logisticInfoList")]
        public List<LogisticInfo> LogisticInfos { get; set; }

        [JsonProperty("logisticsStatus")]
        public string logisticsStatus { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        [JsonProperty("gmtCreate"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime CreateOn { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        [JsonProperty("gmtPaySuccess"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime? PaymentOn { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [JsonProperty("gmtTradeEnd"), JsonConverter(typeof(AliDateTimeConverter))]
        public DateTime? EndOn { get; set; }

        /// <summary>
        /// 子订单，就是订单下面买了哪些产品
        /// </summary>
        [JsonProperty("ChildOrderList")]
        public List<ChildOrderDetail> ChildOrders { get; set; }
    }
}
