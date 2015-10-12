using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {
    public class LogisticsServiceList : MethodBase<List<SupportLogisticsServices>> {
        protected override string APIName {
            get {
                return "api.listLogisticsService";
            }
        }

        public async override Task<List<SupportLogisticsServices>> Execute(Auth auth) {
            var str = await this.GetResult(auth);
            var o = new {
                result = new List<SupportLogisticsServices>(),
                success = false
            };
            o = JsonConvert.DeserializeAnonymousType(str, o);
            return o.result.OrderBy(i => i.Code).ToList();
        }
    }
}
