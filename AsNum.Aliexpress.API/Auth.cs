using AsNum.Common.Extends;
using AsNum.Common.Net;
using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace AsNum.Xmj.API {

    //[Synchronization]
    [Serializable, SaveAuthData]
    public partial class Auth : MarshalByRefObject, INotifyPropertyChanged {

        public static readonly string AppKey = "";
        public static readonly string SECKey = "";

        /// <summary>
        /// API 版本
        /// </summary>
        public static readonly int Version = 1;

        /// <summary>
        /// 数据协议
        /// </summary>
        public static readonly string DataProtocol = "param2";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string Domain = "gw.api.alibaba.com";

        public static readonly string APINameSpace = "aliexpress.open";

        private string user = "";



        static Auth() {
            AppKey = ConfigurationManager.AppSettings.Get(ConstValue.AppKeyAppSettingKey, "");
            SECKey = ConfigurationManager.AppSettings.Get(ConstValue.SECKeyAppSettingKey, "");

            if (string.IsNullOrEmpty(AppKey) || string.IsNullOrEmpty(SECKey)) {
                throw new Exception(string.Format("未设置 AppSetting {0} {1}", ConstValue.AppKeyAppSettingKey, ConstValue.SECKeyAppSettingKey));
            }
        }

        //[NotifyParentProperty(true)]
        public string User {
            get {
                return this.user;
            }
            set {
                var flag = string.Equals(this.user, value);
                this.user = value;
                if (!flag)
                    this.OnPropertyChanged("User");
            }
        }

        internal string Pwd {
            get;
            set;
        }

        private string code = "";
        /// <summary>
        /// 授权临时码,该码用于获取授权码
        /// <remarks>用户同意授权后，返回的是 Code</remarks>
        /// </summary>
        public string Code {
            get {
                return this.code;
            }
            private set {
                var flag = this.code == value;
                this.code = value;
                if (!flag)
                    OnPropertyChanged("Code");
            }
        }

        private Token authToken = null;
        /// <summary>
        /// <remarks>跟据Code 返回令牌. 该令牌下有刷新令牌的Token。刷新令牌时，该刷新Token 不会返回，说明刷新Token 只会产生一次</remarks>
        /// </summary>
        public Token AuthToken {
            get {
                return this.authToken;
            }
            private set {
                var flag = this.authToken != null && this.authToken.Equals(value);
                this.authToken = value;
                if (!flag)
                    this.OnPropertyChanged("AuthToken");
            }
        }


        public CookieContainer CookieContainer = new CookieContainer();

        public Auth() {
        }

        public Auth(string user, string pwd) {
            this.User = user;
            this.Pwd = pwd;
        }

        public string GetApiUrl(string opt) {
            return string.Format("https://{0}/openapi/{1}/{2}/{3}/{4}/{5}",
                            Domain,
                            DataProtocol,
                            Version,
                            APINameSpace,
                            opt,
                            AppKey)
                            .SetUrlKeyValue("access_token", this.AuthToken.AccessToken);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public void DoAuth(string user, string pwd) {
            this.Code = this.GetAuthCode(user, pwd);

            if (this.AuthToken == null || this.AuthToken.IsInvalid)
                this.GetAccessToken();
        }

        /// <summary>
        /// 获取授权临时码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string GetAuthCode(string user, string pwd) {
            this.User = user;

            var url = "http://gw.api.alibaba.com/auth/authorize.htm?client_id=&site=aliexpress&redirect_uri=urn:ietf:wg:oauth:2.0:oob";
            url = url.SetUrlKeyValue("client_id", AppKey);
            //SIG必须要把前面所有参数一起计算
            url = url.SetUrlKeyValue("_aop_signature", SIG(url));

            var rh = new RequestHelper(this.CookieContainer);
            //rh.RequestHeader = RequestHelper.CommonHeder;
            var ctx = rh.Get(url);

            var dic = ctx.CollectFormItems("login-form");
            dic.Set("account", user);
            dic.Set("password", pwd);

            ctx = rh.Post(url, dic);
            if (!rh.ResponseUrl.StartsWith("http://gw.api.alibaba.com/auth/authCode.htm", StringComparison.OrdinalIgnoreCase)) {
                //this.HaveAuthed = false;
                throw new Exception(string.Format("授权失败，用户:{0},　请检查用户名密码是否正确.", user));
            } else {
                //this.HaveAuthed = true;
                return dic.Get("recordId");
            }
        }

        /// <summary>
        /// 获取授权码
        /// </summary>
        public void GetAccessToken() {
            var url = string.Format("https://gw.api.alibaba.com/openapi/http/1/system.oauth2/getToken/{0}?grant_type=authorization_code&need_refresh_token=true&client_id={1}&client_secret={2}&redirect_uri=urn:ietf:wg:oauth:2.0:oob&code={3}",
                AppKey, AppKey, SECKey, this.Code);

            var rh = new RequestHelper(this.CookieContainer);
            var ctx = rh.Post(url);

            var token = JsonConvert.DeserializeObject<Token>(ctx);
            if (token != null) {
                this.AuthToken = token;
                //this.RefreshToken = token.RefreshToken;
            } else {
                throw new Exception(string.Format("获取令牌失败,用户:{0}", this.User));
            }
        }

        public void RefreshAccessToken() {
            var url = string.Format("https://gw.api.alibaba.com/openapi/param2/1/system.oauth2/getToken/{0}", AppKey);
            //grant_type=refresh_token&client_id={1}&client_secret={2}&refresh_token={3}
            var dic = new Dictionary<string, string>() { 
                {"grant_type","refresh_token"},
                {"client_id",AppKey},
                {"client_secret", SECKey},
                {"refresh_token", this.AuthToken.RefreshToken},
                {"need_refresh_token","true"}
            };
            var rh = new RequestHelper(this.CookieContainer);
            var ctx = rh.Post(url, dic);
            var token = JsonConvert.DeserializeObject<Token>(ctx);
            if (token != null) {
                token.RefreshToken = this.AuthToken.RefreshToken;
                this.AuthToken = token;
            } else {
                throw new Exception(string.Format("刷新令牌失败,用户:{0}", this.User));
            }
        }

        private static string SIG(string url) {
            //取出url 中的参数的键值对，并排除 _aop_signature
            var dic = url.ParseString(false);
            var a = dic.Keys.Where(k => !string.Equals("_aop_signature", k))
                .OrderBy(k => k)
                .Select(k => string.Format("{0}{1}", k, dic[k]));

            var b = string.Join("", a);

            using (var sec = new HMACSHA1(Encoding.UTF8.GetBytes(SECKey))) {
                sec.ComputeHash(Encoding.UTF8.GetBytes(b));
                return sec.Hash.Bin2Hex();
            }
        }
    }
}
