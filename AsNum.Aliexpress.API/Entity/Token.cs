using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API.Entity {
    [Serializable]
    public class Token {

        [JsonProperty("aliId")]
        public string AliID {
            get;
            set;
        }

        [JsonProperty("resource_owner")]
        public string Owner {
            get;
            set;
        }

        /// <summary>
        /// AccessToken 的过期时间，秒
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn {
            get;
            set;
        }

        [JsonProperty("refresh_token")]
        public string RefreshToken {
            get;
            set;
        }

        [JsonProperty("access_token")]
        public string AccessToken {
            get;
            set;
        }

        /// <summary>
        /// 令牌产生时间
        /// </summary>
        public DateTime CreateOn {
            get;
            private set;
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool HasExpiressed {
            get {
                return this.CreateOn.AddSeconds(this.ExpiresIn) <= DateTime.Now;
            }
        }

        public Token() {
            this.CreateOn = DateTime.Now;
        }

        public bool IsInvalid {
            get {
                return string.IsNullOrEmpty(this.RefreshToken) || string.IsNullOrEmpty(this.AccessToken);
            }
        }
    }
}
