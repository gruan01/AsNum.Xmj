using AsNum.Common.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;

namespace AsNum.Xmj.API.Methods {
    /// <summary>
    /// 批量获取线上发货标签
    /// </summary>
    public class LogisticsOnlineLogisticsPrintInfo : MethodBase<byte[]> {
        protected override string APIName {
            get { return "api.getPrintInfos"; }
        }

        /// <summary>
        /// 国际运单号
        /// </summary>
        //[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public List<string> LogisticsNOs { get; set; }

        public LogisticsOnlineLogisticsPrintInfo() {
            this.LogisticsNOs = new List<string>();
        }

        public override string GetResult(Auth auth) {
            var dic = ParamHelper.GetParams(this);
            var url = auth.GetApiUrl(this.APIName, dic);

            var tmp = this.LogisticsNOs.Select(s => new {
                internationalLogisticsId = s
            });
            dic.Add("warehouseOrderQueryDTOs", JsonConvert.SerializeObject(tmp));

            var rh = new RequestHelper(auth.CookieContainer);
            this.ResultString = rh.Post(url, dic);
            return this.ResultString;
        }

        public override byte[] Execute(Auth auth) {
            var o = new { StatusCode = "0", body = "" };
            var str = this.GetResult(auth);
            o = JsonConvert.DeserializeAnonymousType(str, o);
            if (o.StatusCode == "200") {
                return Convert.FromBase64String(o.body);
            } else
                return null;
        }
    }
}
