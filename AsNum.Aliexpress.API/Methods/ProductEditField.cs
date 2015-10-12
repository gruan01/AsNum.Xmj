using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System.Threading.Tasks;

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

        [EnumParam("fiedName", EnumUseNameOrValue.Name)]
        public ProductBatchEditFields Field {
            get;
            set;
        }

        [Param("fiedvalue")]
        public string Value {
            get;
            set;
        }

        public async override Task<NormalResult> Execute(Auth auth) {
            var str = await this.GetResult(auth);
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
