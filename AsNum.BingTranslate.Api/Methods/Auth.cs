using AsNum.BingTranslate.Api.Attributes;
using AsNum.BingTranslate.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.BingTranslate.Api.Methods {
    /// <summary>
    /// 认证
    /// </summary>
    internal class Auth : MethodBase<AccessToken> {

        public override string MethodName {
            get {
                return "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
            }
        }

        public override string Url {
            get {
                return this.MethodName;
            }
        }

        public override HttpMethods RequestType {
            get {
                return HttpMethods.Post;
            }
        }

        public override ReturnTypes ReturnType {
            get {
                return ReturnTypes.Json;
            }
        }


        [Param("client_id")]
        public string ClientID {
            get;
            set;
        }

        [Param("client_secret")]
        public string ClientSecret {
            get;
            set;
        }

        [Param("scope")]
        public string Scope {
            get {
                return "http://api.microsofttranslator.com";
            }
        }

        [Param("grant_type")]
        public string GrantType {
            get {
                return "client_credentials";
            }
        }
    }
}
