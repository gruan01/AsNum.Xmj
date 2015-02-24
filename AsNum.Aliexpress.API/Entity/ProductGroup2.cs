using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    public class ProductGroup2 {

        [JsonProperty("groupId")]
        public int ID {
            get;
            set;
        }

        [JsonProperty("groupName")]
        public string Name {
            get;
            set;
        }

        
        public string NamePath { get; set; }

        [JsonProperty("childGroup")]
        public List<ProductGroup2> Children {
            get;
            set;
        }

        public bool CanChoice {
            get {
                return this.Children == null || this.Children.Count == 0;
            }
        }

        public string Account {
            get;
            set;
        }
    }
}
