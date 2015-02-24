using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using AsNum.Common.Extend;
using System.Globalization;

namespace AsNum.Common.Net {
    public class CookiePool {

        private static readonly Regex rx = new Regex(@"(?<k>path|domain|expires)=(?<v>[^;]*)");

        private static CookieContainer container = null;
        public static CookieContainer CookieContainer {
            get {
                if(container == null)
                    container = new CookieContainer();
                return container;
            }
            private set {
                container = value;
            }
        }

        public static void Push(string[] orgCookieHeaders , string domain) {
            if(string.IsNullOrEmpty(domain))
                throw new ArgumentException("domain");
            if(orgCookieHeaders == null) {
                throw new ArgumentException("orgCookieHeaders");
            }
            var uri = new Uri(domain);

            foreach(var och in orgCookieHeaders) {
                var ctx = och.Split(new string[] { "; " } , StringSplitOptions.RemoveEmptyEntries)[0];
                var tmp = ctx.Split('=');
                if(rx.IsMatch(och)) {
                    var mas = rx.Matches(och);
                    mas.OfType<Match>().ToList().ForEach(m => {
                        var cookie = new Cookie();
                        cookie.Name = tmp[0];
                        cookie.Value = tmp[1];
                        var v = m.Groups["v"].Value;
                        switch(m.Groups["k"].Value.ToLower()) {
                            case "domain":
                                cookie.Domain = string.IsNullOrEmpty(v) ? domain.ToLower() : v.ToLower();
                                break;
                            case "path":
                                cookie.Path = v;
                                break;
                            case "expires":
                                cookie.Expires = v.Replace('-',' ').ToDateTime("R", DateTime.Now);
                                break;
                        }
                        if(string.IsNullOrEmpty(cookie.Domain)) {
                            cookie.Domain = string.Format(".{0}", uri.Host);
                        }
                        CookieContainer.Add(cookie);
                    });
                }
            }
        }
    }
}
