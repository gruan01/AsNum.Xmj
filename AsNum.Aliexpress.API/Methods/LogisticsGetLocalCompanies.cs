using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {
    /// <summary>
    /// 获取中邮小包支持的国内快递公司信息
    /// </summary>
    public class LogisticsGetLocalCompanies : MethodBase<List<LocalLogisticsCompany>> {
        protected override string APIName {
            get { return "api.qureyWlbDomesticLogisticsCompany"; }
        }

        public async override Task<List<LocalLogisticsCompany>> Execute(Auth auth) {
            var str = await base.GetResult(auth);
            var o = new {
                result = new List<LocalLogisticsCompany>(),
                success = false
            };
            o = JsonConvert.DeserializeAnonymousType(str, o);
            return o.result;
        }
    }
}
