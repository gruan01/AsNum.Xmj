using AsNum.Xmj.API.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {
    /// <summary>
    /// 买家活跃等级, A0 ~ A5, 数字越高,越活跃
    /// </summary>
    public class BuyerLevel : MethodBase<string> {
        protected override string APIName {
            get { return "api.queryAccountLevel"; }
        }

        /// <summary>
        /// 买家ID
        /// </summary>
        [Param("loginId", Required = true)]
        public string BuyerID {
            get;
            set;
        }

        public override string Execute(Auth auth) {
            var str = this.GetResult(auth);
            var o = new { level = "", success = false };
            o = JsonConvert.DeserializeAnonymousType(str, o);
            return o.success ? o.level : "";
        }
    }
}
