using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace AsNum.Common.Net {
    public class HttpRequestHeaderBuilder {

        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        private Regex rx = new Regex(@"\p{Lu}");

        public Dictionary<string, string> Headers {
            get {
                return _headers;
            }
        }



        public void AddHeader(HttpRequestHeader header, string value) {
            string key = rx.Replace(header.ToString(), new MatchEvaluator(Me));
            AddHeader(key, value);
        }

        private string Me(Match ma) {
            if (ma.Index == 0)
                return ma.ToString();
            return "-" + ma.ToString();
        }

        public void AddHeader(string header, string value) {
            if (!_headers.ContainsKey(header))
                _headers.Add(header, value);
            else
                _headers[header] = value;
        }
    }
}
