using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AsNum.Xmj.API.Methods {
    public class OrderWaitingEvaluateList : MethodBase<List<string>> {
        protected override string APIName {
            get {
                return "api.evaluation.querySellerEvaluationOrderList";
            }
        }

        public override List<string> Execute(Auth auth) {
            var str = this.GetResult(auth);
            Regex rx = new Regex(@"{""orderId"":(?<oid>\d+)}");
            return rx.Matches(str).Cast<Match>().Select(m => m.Groups["oid"].Value).ToList();
        }
    }
}
