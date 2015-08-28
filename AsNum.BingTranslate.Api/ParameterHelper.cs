using AsNum.BingTranslate.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsNum.Common.Extends;

namespace AsNum.BingTranslate.Api {
    internal class ParameterHelper {

        public static Dictionary<string, string> GetParams(object obj) {
            var dic = new Dictionary<string, string>();
            var props = obj.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(ParamAttribute), true).Length > 0);
            foreach (var p in props) {
                var pa = (ParamAttribute)p.GetCustomAttributes(typeof(ParamAttribute), true).First();
                var pms = pa.GetParams(obj, p);
                if (pms != null) {
                    foreach (var pm in pms) {
                        dic.Set(pm.Key, pm.Value);
                    }
                }
            }

            return dic;
        }

    }
}
