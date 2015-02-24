using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;

namespace AsNum.Xmj.API.Methods {
    public class ProductGetProductGroup : MethodBase<ProductGroupResult> {
        protected override string APIName {
            get { return "api.getWsProductGroup"; }
        }

        [Param("page")]
        public int? Page { get; set; }
    }
}
