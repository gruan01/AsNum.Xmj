using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.BingTranslate.Api.Entities {
    public class AccessToken : BaseResult {


        [JsonProperty("access_token")]
        public string Token {
            get;
            set;
        }

        [JsonProperty("expires_in")]
        public int ExpiresIn {
            get;
            set;
        }

        [JsonProperty("token_type")]
        public string Type {
            get;
            set;
        }

        [JsonProperty("scope")]
        public string Scope {
            get;
            set;
        }

        public DateTime CreateOn {
            get;
            private set;
        }

        public AccessToken() {
            this.CreateOn = DateTime.Now;
        }

        /// <summary>
        /// 是否已过期
        /// </summary>
        public bool HasExpiressed {
            get {
                return this.CreateOn.AddSeconds(this.ExpiresIn) <= DateTime.Now;
            }
        }

        /// <summary>
        /// 是否无效
        /// </summary>
        public bool IsInvalid {
            get {
                return string.IsNullOrEmpty(this.Token) || this.HasExpiressed;
            }
        }

    }
}
