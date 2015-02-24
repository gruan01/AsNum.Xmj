using AsNum.Xmj.API.Attributes;
using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API.Entity {
    /// <summary>
    /// 颜色/颜色图片 等属性
    /// </summary>
    [Serializable]
    public class ProductSKUProperty {
        [Param("SkuPropertyId"), JsonProperty("SkuPropertyId")]
        public int skuPropertyId { get; set; }

        [Param("propertyValueId"), JsonProperty("propertyValueId")]
        public int PropertyValueId { get; set; }

        [Param("skuImage"), JsonProperty("skuImage")]
        public string SkuImage { get; set; }

        [Param("propertyValueDefinitionName"), JsonProperty("propertyValueDefinitionName")]
        public string DefinitionName { get; set; }
    }
}
