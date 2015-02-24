using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;

namespace AsNum.Xmj.API.Methods {
    public class ProductEditField : MethodBase<NormalResult> {
        protected override string APIName {
            get {
                return "api.editSimpleProductFiled";
            }
        }

        [Param("productId")]
        public string ProductID {
            get;
            set;
        }

        [EnumNameParam("fiedName")]
        public ProductBatchEditFields Field {
            get;
            set;
        }

        [Param("fiedvalue")]
        public string Value {
            get;
            set;
        }

        public override NormalResult Execute(Auth auth) {
            var str = this.GetResult(auth);
            var o = new {
                success = true
            };
            o = JsonConvert.DeserializeAnonymousType(str, o);
            //return new NormalResult() {
            //    Success = o.success
            //};
            return new NormalResult() {
                Success = o.success
            };
        }
    }
}
