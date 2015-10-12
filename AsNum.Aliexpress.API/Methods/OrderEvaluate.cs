using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {
    public class OrderEvaluate : MethodBase<Result> {
        protected override string APIName {
            get {
                return "api.evaluation.saveSellerFeedback";
            }
        }

        [Param("orderId", Required = true)]
        public string OrderNo {
            get;
            set;
        }

        [Param("score")]
        public int Score {
            get;
            set;
        }

        [Param("feedbackContent")]
        public string Content {
            get;
            set;
        }

        public async override Task<Result> Execute(Auth auth) {
            var str = await base.GetResult(auth);
            dynamic obj = JObject.Parse(str);

            if (obj.success == false) {
                return new Result() {
                    Success = false,
                    Message = obj.errorMessage,
                    ErrorCode = obj.errorCode
                };
            } else
                return new Result() {
                    Success = obj.success
                };
        }
    }
}
