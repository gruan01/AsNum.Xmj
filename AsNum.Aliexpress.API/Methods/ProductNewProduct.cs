using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;

namespace AsNum.Xmj.API.Methods {
    public class ProductNewProduct : MethodBase<ProductNewProductResult> {

        protected override string APIName {
            get { return "api.postAeProduct"; }
        }

        private Product detail = new Product();
        [FlattenParam]
        public Product ProductDetail {
            get {
                return detail;
            }
            set {
                this.detail = value;
            }
        }
    }
}
