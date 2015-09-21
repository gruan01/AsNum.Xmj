using AsNum.Xmj.API.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    [Serializable]
    public class Product {

        [Param("categoryId"), JsonProperty("categoryId")]
        public int CategoryID { get; set; }

        /// <summary>
        /// 产品属性(是否有有包装/材质/品牌/功能等)
        /// </summary>
        [JsonParam("aeopAeProductPropertys"), JsonProperty("aeopAeProductPropertys")]
        public List<ProductProperty> Properties { get; set; }

        [Param("subject", Required = true), JsonProperty("subject")]
        public string ProductName { get; set; }

        [Param("imageURLs"), JsonProperty("imageURLs")]
        public string ImageUrls { get; set; }

        [Param("isImageDynamic"), JsonProperty("isImageDynamic")]
        public bool IsImageDynamic { get; set; }

        [Param("isImageWatermark"), JsonProperty("isImageWatermark")]
        public bool IsImageWatermark { get; set; }

        [Param("keyword"), JsonProperty("keyword")]
        public string Keyword { get; set; }

        [Param("productMoreKeywords1"), JsonProperty("productMoreKeywords1")]
        public string Keyword1 { get; set; }

        [Param("productMoreKeywords2"), JsonProperty("productMoreKeywords2")]
        public string Keyword2 { get; set; }

        [Param("summary"), JsonProperty("summary")]
        public string Sumamary { get; set; }

        /// <summary>
        /// 产品一口价
        /// </summary>
        [Param("productPrice"), JsonProperty("productPrice")]
        public decimal Price { get; set; }

        [EnumParam("productUnit", EnumUseNameOrValue.Name), JsonProperty("productUnit")]
        public ProductUnit Unit { get; set; }

        /// <summary>
        /// 是否打包销售
        /// </summary>
        [Param("packageType"), JsonProperty("packageType")]
        public bool IsPackageSell { get; set; }

        /// <summary>
        /// 如果打包销售,每包几件
        /// </summary>
        [Param("lotNum"), JsonProperty("lotNum")]
        public int LotNum { get; set; }

        //[JsonParam]
        [JsonParam("aeopAeProductSKUs"), JsonProperty("aeopAeProductSKUs")]
        public List<ProductSKU> SKUs { get; set; }


        private int? bulkOrder = null;
        /// <summary>
        /// 批发最小数量
        /// </summary>
        [Param("bulkOrder"), JsonProperty("bulkOrder")]
        public int? BulkOrder {
            get {
                return this.bulkOrder;
            }
            set {
                if (value.HasValue && value <= 1)
                    this.bulkOrder = null;
                else if (value.HasValue && value > 100000)
                    this.bulkOrder = 100000;
                else
                    this.bulkOrder = value;
            }
        }

        private int? bulkDiscount = null;
        /// <summary>
        /// 批发折扣
        /// </summary>
        [Param("bulkDiscount"), JsonProperty("bulkDiscount")]
        public int? BulkDiscount {
            get {
                return this.bulkDiscount;
            }
            set {
                if (value.HasValue && (value > 99 || value < 1)) {
                    throw new ArgumentOutOfRangeException("BulkDiscount");
                }
                this.bulkDiscount = value;
            }
        }

        /// <summary>
        /// 备货期,天
        /// </summary>
        [Param("deliveryTime"), JsonProperty("deliveryTime")]
        public int StockUpPeriod { get; set; }

        [Param("detail"), JsonProperty("detail")]
        public string Detail { get; set; }

        [Param("promiseTemplateId"), JsonProperty("promiseTemplateId")]
        public long PromiseTemplateId { get; set; }

        /// <summary>
        /// 产品分组
        /// </summary>
        [Param("groupId"), JsonProperty("groupId")]
        public int ProductGroup { get; set; }

        /// <summary>
        /// 运费模板 ID
        /// </summary>
        [Param("freightTemplateId"), JsonProperty("freightTemplateId")]
        public int FreightTemplateID { get; set; }

        [Param("packageLength"), JsonProperty("packageLength")]
        public int PackageLength { get; set; }

        [Param("packageWidth"), JsonProperty("packageWidth")]
        public int PackageWidth { get; set; }

        [Param("packageHeight"), JsonProperty("packageHeight")]
        public int PackageHeight { get; set; }

        /// <summary>
        /// 包裹重量 KG
        /// </summary>
        [Param("grossWeight"), JsonProperty("grossWeight")]
        public double PackageWeight { get; set; }

        /// <summary>
        /// 有效天数
        /// </summary>
        [Param("wsValidNum"), JsonProperty("wsValidNum")]
        public int PeriodOfValidity { get; set; }


        private string src = "isv";
        [Param("src"), JsonProperty("src")]
        public string Src {
            get {
                return this.src;
            }
            set {
                this.src = value;
            }
        }
    }
}
