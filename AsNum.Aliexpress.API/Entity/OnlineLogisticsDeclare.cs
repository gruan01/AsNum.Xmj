using AsNum.Xmj.API.Attributes;

namespace AsNum.Xmj.API.Entity {
    /// <summary>
    /// 线上发货 物品声明信息
    /// </summary>
    public class OnlineLogisticsDeclareInfo {

        /// <summary>
        /// 产品ID, 必填,如为礼品,则设置为0
        /// </summary>
        [Param("productId")]
        public string ProductID { get; set; }

        /// <summary>
        /// 申报中文名称(必填,长度1-20)
        /// </summary>
        [Param("categoryCnDesc")]
        public string DescCn { get; set; }

        /// <summary>
        /// 为申报英文名称(必填,长度1-60)
        /// </summary>
        [Param("categoryEnDesc")]
        public string DescEn { get; set; }

        /// <summary>
        /// 产品件数(必填1-999);
        /// </summary>
        [Param("productNum")]
        public int Count { get; set; }

        /// <summary>
        /// 申报金额(必填,0.01-10000.00)
        /// </summary>
        [Param("productDeclareAmount")]
        public decimal Amount { get; set; }


        /// <summary>
        /// productWeight为产品申报重量(必填0.001-2.000);
        /// </summary>
        [Param("productWeight")]
        public double Weight { get; set; }

        /// <summary>
        /// 是否包含锂电池(必填0/1);
        /// </summary>
        public bool WithBattery { get; set; }

        [Param("isContainsBattery")]
        public int IsWithBattery {
            get {
                return this.WithBattery ? 1 : 0;
            }
        }
    }
}
