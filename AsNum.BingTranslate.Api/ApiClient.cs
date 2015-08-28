using AsNum.BingTranslate.Api.Entities;
using AsNum.BingTranslate.Api.Methods;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AsNum.Common.Extends;

namespace AsNum.BingTranslate.Api {
    public class ApiClient {

        private static ApiClient _client = null;

        private AccessToken _token = null;
        internal AccessToken Token {
            get {
                if (this._token == null)
                    this._token = new AccessToken();
                return this._token;
            }
            private set {
                this._token = value;
            }
        }

        internal readonly CookieContainer Cookies = new CookieContainer();


        private string ClientID, Secret;

        static ApiClient() {
            //设置策略注入
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            var injector = new PolicyInjector(configurationSource);
            try {
                PolicyInjection.SetPolicyInjector(injector);
            } catch { }
        }

        public static void Init(string clientID, string secret) {
            _client = new ApiClient(clientID, secret);
        }

        public static ApiClient GetInstance() {
            return _client;
        }


        private ApiClient(string clientID, string secret) {
            this.ClientID = clientID;
            this.Secret = secret;
        }


        /// <summary>
        /// 对ApiClient 进行认证
        /// </summary>
        internal void DoAuth() {
            var method = new Auth() {
                ClientID = this.ClientID,
                ClientSecret = this.Secret
            };
            this.Token = method.Execute(this).Result;
        }

        /// <summary>
        /// 获取API的URL
        /// </summary>
        /// <returns></returns>
        internal string GetApiUrl(MethodBase method) {

            if (!string.IsNullOrWhiteSpace(method.Url)) {
                return method.Url;
            }

            return string.Format("http://api.microsofttranslator.com/v2/Http.svc/{0}", method.MethodName);
        }

        /// <summary>
        /// 执行API方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<T> Execute<T>(MethodBase<T> method) {
            //将方法在策略中包装
            var m = (MethodBase<T>)PolicyInjection.Wrap(method.GetType(), method);
            return await m.Execute(this);
        }

        public static async Task<T> ExecuteWrap<T>(MethodBase<T> method) {
            return await ApiClient.GetInstance().Execute(method);
        }
    }
}
