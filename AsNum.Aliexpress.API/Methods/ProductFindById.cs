using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;

namespace AsNum.Xmj.API.Methods {
    public class ProductFindById : MethodBase<Product> {
        protected override string APIName {
            get { return "api.findAeProductById"; }
        }

        [Param("productId", Required = true)]
        public string ProductID { get; set; }
    }
}
