using AsNum.Xmj.API.Attributes;
using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API.Entity {
    [Serializable]
    public class ProductProperty {

        [Param("attrNameId"), JsonProperty("attrNameId")]
        public string NameID { get; set; }

        [Param("attrValueId"), JsonProperty("attrValueId")]
        public string ValueID { get; set; }

        [Param("attrName"),JsonProperty("attrName")]
        public string AttrName { get; set; }

        [Param("attrValue"),JsonProperty("attrValue")]
        public string AttrValue { get; set; }
    }
}
