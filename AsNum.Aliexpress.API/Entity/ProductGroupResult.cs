using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    public class ProductGroupResult {

        [JsonProperty("aeProductGroupList")]
        public List<ProductGroup> List{get;set;}

    }
}
