using AsNum.Common.Extends;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AsNum.Common.Net {
    /// <summary>
    /// Web 请求扩展
    /// </summary>
    public class RequestHelper {

        /// <summary>
        /// 预设 Request Header
        /// </summary>
        public static Dictionary<string, string> CommonHeder {
            get {
                var builder = new HttpRequestHeaderBuilder();
                builder.AddHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)");
                builder.AddHeader("Accept", "text/html, application/xhtml+xml, */*");
                builder.AddHeader("Accept-Language", "zh-CN");
                //builder.AddHeader("Connection", "Keep-Alive");
                return builder.Headers;
            }
        }

        private static Regex urlCheckReg = new Regex("^(http[s]?://)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 代理设置
        /// </summary>
        public WebProxy Proxy {
            get;
            set;
        }

        private string url = "";
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url {
            get {
                return this.url;
            }
            private set {
                this.url = value;
                if (!urlCheckReg.Match(this.url).Success)
                    this.url = "http://" + url;
                if (this.url.StartsWith("https", true, null)) {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                }
            }
        }

        /// <summary>
        /// 最终响应的URL，如果发生 301、302 时，这个值会和请求地址不同
        /// </summary>
        public string ResponseUrl {
            get;
            private set;
        }

        private Encoding encode = Encoding.UTF8;
        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encode {
            get {
                return this.encode;
            }
            set {
                this.encode = value;
            }
        }

        private int timeOut = 0;
        /// <summary>
        /// 超时时长，为0时，不限制超时,单位：毫秒
        /// </summary>
        public int TimeOut {
            get {
                return this.timeOut;
            }
            set {
                this.timeOut = value < 0 ? 0 : value;
            }
        }

        private Dictionary<string, string> requestHeader = new Dictionary<string, string>();
        /// <summary>
        /// 请求头
        /// </summary>
        public Dictionary<string, string> RequestHeader {
            get {
                if (this.requestHeader == null)
                    this.requestHeader = new Dictionary<string, string>();
                return this.requestHeader;
            }
            set {
                this.requestHeader = value;
            }
        }

        /// <summary>
        /// 同一组操作,请使用同一个 CookieContainer
        /// <remarks>
        /// 比如两个不同的用户登陆到某系统,请使用两个 CookieContainer, 
        /// 不能每次请求都使用一个新的 CookieContainer,这样会导至先前读到的 Cookie 无法应用到新的请求
        /// </remarks>
        /// </summary>
        public CookieContainer CookieContainer {
            get;
            set;
        }

        /// <summary>
        /// 返回的头
        /// </summary>
        public WebHeaderCollection ResponseHeaders {
            get;
            private set;
        }

        /// <summary>
        /// HTTP 状态, 如果发生了跳转, 最终返回的是跳转页的 HttpStatus , 而不会出现 301/302
        /// </summary>
        public HttpStatusCode? HttpStatus {
            get;
            private set;
        }

        /// <summary>
        /// 异常的状态，通常是网络不通等
        /// </summary>
        public WebExceptionStatus? ExceptionStatus {
            get;
            set;
        }


        private Lazy<string> MutiPartFormBoundry = new Lazy<string>(() => {
            return string.Format("---------------------------{0}", DateTime.Now.Ticks.ToString("x"));
        });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public RequestHelper(CookieContainer container) {
            this.CookieContainer = container;
        }


        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public string Get(string url, Dictionary<string, string> datas = null) {
            var req = this.SetRequest(url, datas);
            return this.GetContext(req);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public Stream GetStream(string url, Dictionary<string, string> datas = null) {
            var req = this.SetRequest(url, datas);
            return req.GetResponse().GetResponseStream();
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="datas"></param>
        /// <param name="files"></param>
        /// <param name="origDatas"></param>
        /// <returns></returns>
        public string Post(string url, Dictionary<string, string> datas = null, List<PostFileItem> files = null, byte[] origDatas = null, string contentType = "application/x-www-form-urlencoded") {
            this.Url = url;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(this.Url);
            if (this.TimeOut > 0)
                req.Timeout = this.TimeOut;

            if (this.Proxy != null)
                req.Proxy = this.Proxy;

            req.Method = "POST";
            req.CookieContainer = this.CookieContainer;

            SetRequestHeaders(req, this.RequestHeader);

            if (files == null || files.Count == 0) {
                req.ContentType = contentType;
                SetPostData(req, datas, origDatas, this.Encode);
            } else {
                req.ContentType = string.Format("multipart/form-data; boundary={0}", this.MutiPartFormBoundry.Value);
                this.SetMutipartPostData(req, datas, files, this.Encode);
            }

            return this.GetContext(req);
        }

        private HttpWebRequest SetRequest(string url, Dictionary<string, string> datas = null) {
            if (datas != null) {
                foreach (var d in datas) {
                    url = url.SetUrlKeyValue(d.Key, d.Value, this.Encode);
                }
            }

            this.Url = url;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(this.Url);
            if (this.TimeOut > 0)
                req.Timeout = this.TimeOut;

            if (this.Proxy != null)
                req.Proxy = this.Proxy;

            req.Method = "GET";
            req.CookieContainer = this.CookieContainer;

            SetRequestHeaders(req, this.RequestHeader);
            return req;
        }

        private string GetContext(WebRequest req) {
            try {
                using (var rep = (HttpWebResponse)req.GetResponse()) {
                    this.ResponseHeaders = rep.Headers;
                    this.ResponseUrl = rep.ResponseUri.ToString();

                    this.HttpStatus = rep.StatusCode;
                    this.ExceptionStatus = null;


                    if (rep.ContentEncoding.ToLower().Contains("gzip")) {
                        using (var repStm = rep.GetResponseStream())
                        using (var stm = new GZipStream(repStm, CompressionMode.Decompress))
                        using (var sr = new StreamReader(stm, this.Encode)) {
                            return sr.ReadToEnd();
                        }
                    } else {
                        using (var stm = rep.GetResponseStream())
                        using (var sr = new StreamReader(stm, this.Encode)) {
                            return sr.ReadToEnd();
                        }
                    }
                }
            } catch (WebException ex) {
                if ((ex.Response is HttpWebResponse)) {
                    this.HttpStatus = ((HttpWebResponse)ex.Response).StatusCode;
                    this.ExceptionStatus = null;
                    using (var sr = new StreamReader(ex.Response.GetResponseStream(), this.Encode)) {
                        return sr.ReadToEnd();
                    }
                } else {
                    this.ExceptionStatus = ex.Status;
                    this.HttpStatus = null;
                    return "";
                }
            }
            //catch(Exception exx) {
            //    throw exx;
            //}
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) {   //   Always   accept   
            return true;
        }

        private void SetRequestHeaders(HttpWebRequest req, Dictionary<string, string> headers) {
            if (headers == null)
                return;
            foreach (string key in headers.Keys) {
                switch (key) {
                    case "Accept":
                        req.Accept = headers[key];
                        break;
                    case "Accept-Charset":
                        req.Headers.Add(key, headers[key]);
                        break;
                    case "Accept-Encoding":
                        req.Headers.Add(HttpRequestHeader.AcceptEncoding, headers[key]);
                        break;
                    case "Accept-Language":
                        req.Headers.Add(key, headers[key]);
                        break;
                    case "User-Agent":
                        req.UserAgent = headers[key];
                        break;
                    case "Referer":
                        req.Referer = headers[key];
                        break;
                    case "Cookie":
                        req.CookieContainer.SetCookies(req.RequestUri, headers[key]);
                        break;
                    case "Connection":
                        if (headers[key].ToLower() == "keep-alive")
                            req.KeepAlive = true;
                        break;
                    case "Content-Type":
                        switch (headers[key].ToLower()) {
                            case "application/x-www-form-urlencoded":
                            case "text/plain":
                            case "text/xml":
                            case "jsonp":
                                req.ContentType = headers[key].ToLower();
                                break;
                            //case "multipart/form-data":
                            //    req.ContentType = string.Format("multipart/form-data; boundary={0}", this.MutiPartFormBoundry.Value);
                            //    break;
                            default:
                                req.ContentType = "application/x-www-form-urlencoded";
                                break;
                        }
                        break;
                    default:
                        try {
                            req.Headers.Add(key, headers[key]);
                        } catch {

                        }
                        break;
                }
            }
        }

        private static void SetPostData(HttpWebRequest req, Dictionary<string, string> datas, byte[] origDatas, Encoding encode, List<PostFileItem> files = null) {
            List<string> kv = new List<string>();
            if (datas != null) {
                foreach (string key in datas.Keys) {
                    kv.Add(string.Format("{0}={1}", key, HttpUtility.UrlEncode(datas[key], encode)));
                }
            }

            using (var rs = req.GetRequestStream()) {
                if (kv.Count > 0)
                    using (var sw = new StreamWriter(rs)) {
                        sw.Write(string.Join("&", kv.ToArray()));
                    }

                if (origDatas != null) {
                    //req.ContentType = "text/plain";
                    rs.Write(origDatas, 0, origDatas.Length);
                }
            }
        }

        private void SetMutipartPostData(HttpWebRequest req, Dictionary<string, string> datas, List<PostFileItem> files, Encoding encode) {
            //用 StreamWriter 居然不行
            using (var msm = new MemoryStream()) {
                var sb = new StringBuilder();
                if (datas != null)
                    foreach (var kv in datas) {
                        sb.AppendFormat("--{0}\r\n", this.MutiPartFormBoundry.Value);
                        sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n", kv.Key);
                        sb.Append(kv.Value);
                        sb.Append("\r\n");
                    }

                var bytes = this.Encode.GetBytes(sb.ToString());
                msm.Write(bytes, 0, bytes.Length);

                if (files != null) {
                    foreach (var file in files) {
                        sb = new StringBuilder();
                        sb.AppendFormat("--{0}\r\n", this.MutiPartFormBoundry.Value);
                        sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n", file.Name, file.FileName);
                        sb.AppendFormat("Content-Type: text/plain\r\n\r\n");

                        bytes = this.Encode.GetBytes(sb.ToString());
                        msm.Write(bytes, 0, bytes.Length);

                        bytes = file.FileBytes;
                        msm.Write(bytes, 0, bytes.Length);

                        bytes = this.Encode.GetBytes("\r\n");
                        msm.Write(bytes, 0, bytes.Length);
                    }
                }

                var str = string.Format("--{0}--\r\n", this.MutiPartFormBoundry.Value);
                bytes = this.Encode.GetBytes(str);
                msm.Write(bytes, 0, bytes.Length);

                msm.Position = 0;
                var tempBuffer = new byte[msm.Length];
                msm.Read(tempBuffer, 0, tempBuffer.Length);

                //req.ContentLength = tempBuffer.Length;
                req.ServicePoint.Expect100Continue = false;
                req.GetRequestStream().Write(tempBuffer, 0, tempBuffer.Length);

                //msm.Close();
            }
        }
    }

}
