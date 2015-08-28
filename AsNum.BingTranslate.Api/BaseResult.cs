using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.BingTranslate.Api {
    public class BaseResult {

        [JsonProperty("errcode")]
        public int ErrorCode {
            get;
            set;
        }

        [JsonProperty("errmsg")]
        public string ErrorInfo {
            get;
            set;
        }

        public bool HasError {
            get {
                return this.ErrorCode != 0;
            }
        }
    }
}
