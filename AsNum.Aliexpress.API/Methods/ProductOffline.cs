using AsNum.Xmj.API.Attributes;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Methods {
    public class ProductOffline : MethodBase<object> {
        protected override string APIName {
            get {
                return "api.offlineAeProduct";
            }
        }

        public List<string> IDs {
            set {
                this.ProductIDs = string.Join(";", value);
            }
        }

        [Param("productIds")]
        public string ProductIDs {
            get;
            set;
        }
    }
}
