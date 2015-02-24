using AsNum.Common.Extends;
using AsNum.Xmj.API.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Xmj.API {
    internal static class ParamHelper {

        public static IEnumerable<Dictionary<string, string>> GetParams(IEnumerable<object> objs) {
            if (objs == null)
                return null;

            return objs.Select(o => GetParams(o));
        }

        public static Dictionary<string, string> GetParams(object obj) {
            if (obj == null)
                return null;

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

        public static string GetParamsAsJson(object obj) {
            if (obj == null)
                return null;

            var dic = GetParams(obj);
            return JsonConvert.SerializeObject(dic);
        }

        public static string GetParamsAsJson(IEnumerable<object> objs) {
            if (objs == null)
                return null;

            return JsonConvert.SerializeObject(objs.Select(obj => GetParams(obj)));
        }

    }
}
