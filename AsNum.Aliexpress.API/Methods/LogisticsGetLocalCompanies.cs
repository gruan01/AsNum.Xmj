using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Methods {
    /// <summary>
    /// 获取中邮小包支持的国内快递公司信息
    /// </summary>
    public class LogisticsGetLocalCompanies : MethodBase<List<LocalLogisticsCompany>> {
        protected override string APIName {
            get { return "api.qureyWlbDomesticLogisticsCompany"; }
        }

        public override List<LocalLogisticsCompany> Execute(Auth auth) {
            var str = base.GetResult(auth);
            var o = new {
                result = new List<LocalLogisticsCompany>(),
                success = false
            };
            o = JsonConvert.DeserializeAnonymousType(str, o);
            return o.result;
        }
    }
}
