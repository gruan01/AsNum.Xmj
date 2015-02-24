using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API.Entity {
    [Serializable]
    public class SKU {

        [JsonProperty("order")]
        public int Order {
            get;
            set;
        }

        [JsonProperty("pName")]
        public string Name {
            get;
            set;
        }

        [JsonProperty("pValue")]
        public string Value {
            get;
            set;
        }

        /// <summary>
        /// 自定义值，如自定义颜色的名名称
        /// </summary>
        [JsonProperty("selfDefineValue")]
        public string CustomValue {
            get;
            set;
        }

        /// <summary>
        /// 属性对应的图片
        /// </summary>
        [JsonProperty("skuImg")]
        public string SKUImage {
            get;
            set;
        }

        /// <summary>
        /// 兼容字符串，很老的订单里的SKU不是上面这种格式
        /// </summary>
        public string CompatibleStr {
            get;
            set;
        }
    }
}
