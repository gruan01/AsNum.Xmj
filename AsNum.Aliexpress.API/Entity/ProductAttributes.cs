using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    public class ProductAttributes {

        [JsonProperty("sku")]
        public List<SKU> SKU {
            get;
            set;
        }
    }
}
