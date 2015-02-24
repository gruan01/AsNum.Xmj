using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class OrderProduct {

        [JsonProperty("orderId")]
        public string OrderID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [JsonProperty("productId")]
        public string ProductID { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [JsonProperty("skuCode")]
        public string SKU { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        [JsonProperty("productImgUrl")]
        public string Image { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [JsonProperty("productName")]
        public string Name { get; set; }

        /// <summary>
        /// 产品信息镜像链接
        /// </summary>
        [JsonProperty("productSnapUrl")]
        public string SnapURL { get; set; }

        [JsonProperty("productCount")]
        public int Qty { get; set; }

        [JsonProperty("productUnit")]
        public string Unit { get; set; }

        /// <summary>
        /// 备货期 天
        /// </summary>
        [JsonProperty("goodsPrepareTime")]
        public int PrepareDays { get; set; }

        [JsonProperty("productUnitPrice")]
        public Amount UnitPrice { get; set; }

        [JsonProperty("totalProductAmount")]
        public Amount TotalPrice { get; set; }

        [JsonProperty("canSubmitIssue")]
        public bool CanSubmitIssue { get; set; }

        [JsonProperty("childId")]
        public string ChildID { get; set; }

        /// <summary>
        /// 运抵时间
        /// </summary>
        [JsonProperty("deliveryTime")]
        public string DeliveryTime { get; set; }

        [JsonProperty("freightCommitDay")]
        public string FreightCommitDay { get; set; }

        [JsonProperty("issueStatus")]
        public string IssueStatus { get; set; }

        /// <summary>
        /// 发货方式
        /// </summary>
        [JsonProperty("logisticsServiceName")]
        public string ShippingType { get; set; }

        [JsonProperty("logisticsType")]
        public string LogisticsType { get; set; }


        [JsonProperty("logisticsAmount")]
        public Amount LogisticsAmount {
            get;
            set;
        }

        /// <summary>
        /// 产品留言
        /// </summary>
        [JsonProperty("memo")]
        public string OrderMessage { get; set; }

        [JsonProperty("moneyBack3x")]
        public bool MoneyBack3x { get; set; }
    }
}
