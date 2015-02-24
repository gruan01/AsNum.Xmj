using AsNum.Xmj.API.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    [Serializable]
    public class ProductSKU {

        //TODO 这里如果将 Price 改为 Decimal 或 double 的话,会报错.不知道为什么
        /// <summary>
        /// 该SKU对应的价格
        /// </summary>
        [Param("skuPrice"), JsonProperty("skuPrice")]
        public string Price { get; set; }

        /// <summary>
        /// 是否有库存
        /// </summary>
        [Param("skuStock"), JsonProperty("skuStock")]
        public bool InStock { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [Param("skuCode"), JsonProperty("skuCode")]
        public string SkuCode { get; set; }


        [Param("aeopSKUProperty"), JsonProperty("aeopSKUProperty")]
        public List<ProductSKUProperty> Property { get; set; }
    }
}
