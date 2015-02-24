using Newtonsoft.Json;

namespace AsNum.Xmj.API.Entity {
    public class ChildOrderDetail {

        /// <summary>
        /// 子订单号
        /// </summary>
        [JsonProperty("id")]
        public string ID {
            get;
            set;
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        [JsonProperty("productId")]
        public string ProductID {
            get;
            set;
        }

        /// <summary>
        /// 几个一包(如果打包卖，就是几个一包），如果单个卖，就是1
        /// </summary>
        [JsonProperty("lotNum")]
        public int LotNum {
            get;
            set;
        }

        /// <summary>
        /// 售卖单单
        /// </summary>
        [JsonProperty("productUnit")]
        public string Unit {
            get;
            set;
        }

        /// <summary>
        /// 卖了多少
        /// </summary>
        [JsonProperty("productCount")]
        public int Count {
            get;
            set;
        }

        [JsonProperty("productName")]
        public string ProductName {
            get;
            set;
        }

        [JsonProperty("skuCode")]
        public string SKUCode {
            get;
            set;
        }

        [JsonProperty("productAttributes")]
        private string AttributesStr {
            get;
            set;
        }
        /// <summary>
        /// 一个产品对应多个属性？
        /// </summary>
        public ProductAttributes Attributes {
            get {
                ProductAttributes attrs = null;
                if (!string.IsNullOrWhiteSpace(this.AttributesStr))
                    attrs = JsonConvert.DeserializeObject<ProductAttributes>(this.AttributesStr);

                if (attrs == null || attrs.SKU == null) {
                    attrs = new ProductAttributes() {
                        SKU = new System.Collections.Generic.List<SKU>() {
                             new  SKU(){
                                 Order = 1,
                                  Name = "",
                                   CustomValue = "",
                                    SKUImage = "",
                                 CompatibleStr = this.AttributesStr
                             }
                        }
                    };
                }
                return attrs;
            }
        }

        [JsonProperty("productPrice")]
        public Amount ProductPrice {
            get;
            set;
        }
    }
}
