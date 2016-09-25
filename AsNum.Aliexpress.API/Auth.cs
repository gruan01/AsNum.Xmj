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
using System.Threading;
using System.Windows.Controls;

namespace AsNum.Xmj.API {

    //[Synchronization]
    [Serializable, SaveAuthData]
    public partial class Auth : MarshalByRefObject, INotifyPropertyChanged {

        public static readonly string AppKey = "";
        public static readonly string SECKey = "";

        private object LockObj = null;

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
            internal set {
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
            this.LockObj = new object();
        }

        public Auth(string user, string pwd) {
            this.User = user;
            this.Pwd = pwd;
            this.LockObj = new object();
        }

        public string GetApiUrl(string opt, Dictionary<string, string> dic) {
            var url = string.Format("https://{0}/openapi/{1}/{2}/{3}/{4}/{5}",
                            Domain,
                            DataProtocol,
                            Version,
                            APINameSpace,
                            opt,
                            AppKey)
                            .SetUrlKeyValue("access_token", this.AuthToken.AccessToken);


            if (dic != null) {
                //foreach (var kv in dic) {
                //    url = url.SetUrlKeyValue(kv.Key, kv.Value);
                //}
                //计算签名时,不能转码,
                var ps = string.Join("&", dic.Select(kv => string.Format("{0}={1}", kv.Key, kv.Value)));
                url = string.Format("{0}&{1}", url, ps);
            }

            url = url.SetUrlKeyValue("_aop_signature", SIG(url, true));

            //var sig = SIG("https://gw.api.alibaba.com/openapi/param2/1/aliexpress.open/api.findOrderListSimpleQuery/1530643?page=1&pageSize=50&access_token=a82ec1ad-4c88-4a8c-b8c1-49d75f9bf43c&_aop_signature=D6B46D52175A7DF3821BBCCF56E082EF9CED111B", true);

            return url;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            if (this.PropertyChanged != null) {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public void DoAuth(string user, string pwd) {
            //this.Code = this.GetAuthCode(user, pwd);
            //this.Code = this.GetAuthCode2(user, pwd);

            if (this.AuthToken == null || this.AuthToken.IsInvalid)
                this.GetAccessToken();
        }


        //private string GetAuthCode2(string user, string pwd) {

        //    if (this.LockObj == null)
        //        this.LockObj = new object();

        //    var url = "http://gw.api.alibaba.com/auth/authorize.htm?client_id=&site=aliexpress&redirect_uri=urn:ietf:wg:oauth:2.0:oob";
        //    url = url.SetUrlKeyValue("client_id", AppKey);
        //    //SIG必须要把前面所有参数一起计算
        //    url = url.SetUrlKeyValue("_aop_signature", SIG(url));

        //    var code = "";
        //    Monitor.Enter(this.LockObj);
        //    var form = new AuthForm(url) {
        //        Text = string.Format("请完成账户 {0} 的授权", user)
        //    };

        //    Action act = () => {
        //        if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
        //            code = form.Code;
        //        }
        //    };

        //    form.Invoke(act);
        //    Monitor.Exit(this.LockObj);

        //    return code;
        //}


        public static string AuthUrl {
            get {
                var url = "http://gw.api.alibaba.com/auth/authorize.htm?client_id=&site=aliexpress&redirect_uri=urn:ietf:wg:oauth:2.0:oob";
                url = url.SetUrlKeyValue("client_id", AppKey);
                //SIG必须要把前面所有参数一起计算
                url = url.SetUrlKeyValue("_aop_signature", SIG(url));
                return url;
            }
        }


        /// <summary>
        /// 获取授权临时码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private string GetAuthCode(string user, string pwd) {
            this.User = user;

            //var url = "http://gw.api.alibaba.com/auth/authorize.htm?client_id=&site=aliexpress&redirect_uri=urn:ietf:wg:oauth:2.0:oob";
            //url = url.SetUrlKeyValue("client_id", AppKey);
            ////SIG必须要把前面所有参数一起计算
            //url = url.SetUrlKeyValue("_aop_signature", SIG(url));

            var url = AuthUrl;

            var rh = new RequestHelper(this.CookieContainer);
            //rh.RequestHeader = RequestHelper.CommonHeder;
            //rh.RequestHeader.Add("Content-Type", "application/x-www-form-urlencoded");
            var ctx = rh.Get(url);
            url = rh.ResponseUrl;

            //if (url.StartsWith("http://alisec.alibaba.com/checkcodev3.php")) {

            //    return GetAuthCode(user, pwd);
            //}

            var dic = ctx.CollectFormItems("login-form");
            dic.Set("account", user);
            dic.Set("password", pwd);

            rh.RequestHeader = new Dictionary<string, string>() { 
                {"Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"},
                //{"Accept-Encoding","gzip, deflate"},
                {"Accept-Language","zh-CN,zh;q=0.8,en;q=0.6,zh-TW;q=0.4"},
                {"Cache-Control","max-age=0"},
                {"Connection","keep-alive"},
                {"Content-Type","application/x-www-form-urlencoded"},
                {"Referer","http://authhz.alibaba.com/auth/authorize.htm?client_id=1530643&site=aliexpress&redirect_uri=urn:ietf:wg:oauth:2.0:oob&_aop_signature=A6185B8F6332618CC157706C5FBDC275FC92258A"},
                {"User-Agent","Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.93 Safari/537.36"}            
            };

            ctx = rh.Post(url, dic);

            var uri = new Uri(rh.ResponseUrl, UriKind.Absolute);
            if (!uri.Segments.Last().Equals("authCode.htm", StringComparison.OrdinalIgnoreCase)) {
                //if (!rh.ResponseUrl.StartsWith("http://authhz.alibaba.com/auth/authCode.htm", StringComparison.OrdinalIgnoreCase)) {
                //if (!rh.ResponseUrl.StartsWith("http://gw.api.alibaba.com/auth/authCode.htm", StringComparison.OrdinalIgnoreCase)) {
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

        private static string SIG(string url, bool useUrlPath = false) {
            string url2 = "";
            if (useUrlPath) {
                url2 = url.Replace("https://gw.api.alibaba.com/openapi/", "");
                url2 = url2.Replace("https://gw.api.alibaba.com:443/openapi/", "");
                url2 = url2.Split('?')[0];
            }

            //取出url 中的参数的键值对，并排除 _aop_signature
            var dic = url.ParseString(false);
            var a = dic.Keys.Where(k => !string.Equals("_aop_signature", k))
                .OrderBy(k => k)
                .Select(k => string.Format("{0}{1}", k, dic[k]));

            var b = string.Join("", a);
            if (useUrlPath) {
                b = string.Format("{0}{1}", url2, b);
            }

            using (var sec = new HMACSHA1(Encoding.UTF8.GetBytes(SECKey))) {
                sec.ComputeHash(Encoding.UTF8.GetBytes(b));
                return sec.Hash.Bin2Hex();
            }
        }
    }
}
