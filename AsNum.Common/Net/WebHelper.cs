using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using AsNum.Common.Extend;

namespace AsNum.Common.Net {

    /// <summary>
    /// 
    /// </summary>
    public enum RequestMethod {
        /// <summary>
        /// 
        /// </summary>
        GET,
        /// <summary>
        /// 
        /// </summary>
        POST
    }

    /// <summary>
    /// 
    /// </summary>
    public class WebHelper {
        private static Regex urlCheckReg = new Regex("^(http[s]?://)", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) {   //   Always   accept   
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode = null) {
            WebHeaderCollection repheaders;
            CookieCollection cookies;
            return GetCtx(url, method, encode, null, null, "", out repheaders, out cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="timeout">millisecond</param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode, int timeout) {
            WebHeaderCollection repheaders;
            CookieCollection cookies;
            return GetCtx(url, method, encode, timeout, null, null, "", out repheaders, out cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode, Dictionary<string, string> headers) {
            WebHeaderCollection repHeaders;
            CookieCollection cookies;
            return GetCtx(url, method, encode, headers, null, "", out repHeaders, out cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="headers"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode, Dictionary<string, string> headers, Dictionary<string, string> datas) {
            WebHeaderCollection repHeaders;
            CookieCollection cookies;
            return GetCtx(url, method, encode, headers, datas, "", out repHeaders, out cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="headers"></param>
        /// <param name="datas"></param>
        /// <param name="responseHeader"></param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode, Dictionary<string, string> headers, Dictionary<string, string> datas, out WebHeaderCollection responseHeader) {
            CookieCollection cookies;
            return GetCtx(url, method, encode, headers, datas, "", out responseHeader, out cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="headers"></param>
        /// <param name="datas"></param>
        /// <param name="responseHeader"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode, Dictionary<string, string> headers, Dictionary<string, string> datas, out WebHeaderCollection responseHeader, out CookieCollection cookies) {
            return GetCtx(url, method, encode, headers, datas, "", out responseHeader, out cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="headers"></param>
        /// <param name="datas"></param>
        /// <param name="origDatas"></param>
        /// <param name="responseHeader"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode, Dictionary<string, string> headers, Dictionary<string, string> datas, string origDatas, out WebHeaderCollection responseHeader, out CookieCollection cookies) {
            return GetCtx(url, method, encode, null, headers, datas, "", out responseHeader, out cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="headers"></param>
        /// <param name="origDatas"></param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode, Dictionary<string, string> headers, string origDatas) {
            WebHeaderCollection responseHeader;
            CookieCollection cookies;
            return GetCtx(url, method, encode, null, headers, null, origDatas, out responseHeader, out cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="encode"></param>
        /// <param name="timeout"></param>
        /// <param name="headers"></param>
        /// <param name="datas"></param>
        /// <param name="origDatas"></param>
        /// <param name="responseHeader"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string GetCtx(string url, RequestMethod method, Encoding encode, int? timeout, Dictionary<string, string> headers, Dictionary<string, string> datas, string origDatas, out WebHeaderCollection responseHeader, out CookieCollection cookies) {
            cookies = null;
            responseHeader = null;

            if(string.IsNullOrWhiteSpace(url))
                return "";

            if(!urlCheckReg.Match(url).Success)
                url = "http://" + url;

            //method = method.ToUpper();
            if(method == RequestMethod.GET) {
                url = GetRequestUrl(url, datas, encode);
            }

            if(url.StartsWith("https", true, null)) {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            if(encode == null) {
                encode = Encoding.UTF8;
            }

            try {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                if(timeout != null && timeout > 0)
                    req.Timeout = (int)timeout;

                //WebProxy proxy = new WebProxy("172.18.20.1", 8080);
                //proxy.Credentials = new NetworkCredential("gruan", "xxx", "CN3");
                //req.Proxy = proxy;


                req.Method = method.ToString();
                req.CookieContainer = new CookieContainer();
                SetRequestHeaders(req, headers);
                if(method == RequestMethod.POST)
                    SetPostData(req, datas, origDatas, encode);


                HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
                cookies = rep.Cookies;
                responseHeader = rep.Headers;
                StreamReader sr = new StreamReader(rep.GetResponseStream(), encode);
                string ctx = sr.ReadToEnd();
                sr.Close();
                rep.Close();
                return ctx;
            } catch(Exception ex) {
                return ex.Message;
            }
        }

        private static string GetRequestUrl(string baseUrl, Dictionary<string, string> datas, Encoding encode) {
            string url = baseUrl;
            if(datas != null) {
                foreach(string key in datas.Keys) {
                    url = url.SetUrlKeyValue(key, datas[key], encode);
                }
            }
            return url;
        }

        private static void SetRequestHeaders(HttpWebRequest req, Dictionary<string, string> headers) {
            if(headers == null)
                return;
            foreach(string key in headers.Keys) {
                switch(key) {
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
                        if(headers[key].ToLower() == "keep-alive")
                            req.KeepAlive = true;
                        break;
                    case "Content-Type":
                        switch(headers[key].ToLower()) {
                            case "application/x-www-form-urlencoded":
                            case "text/plain":
                            case "text/xml":
                            case "jsonp":
                                req.ContentType = headers[key].ToLower();
                                break;
                            //case "multipart/form-data":
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

        private static void SetPostData(HttpWebRequest req, Dictionary<string, string> datas, string origDatas, Encoding encode) {
            List<string> kv = new List<string>();
            if(datas != null) {
                foreach(string key in datas.Keys) {
                    kv.Add(string.Format("{0}={1}", key, HttpUtility.UrlEncode(datas[key], encode)));
                }
            }

            StreamWriter sw = new StreamWriter(req.GetRequestStream());
            sw.Write(string.Join("&", kv.ToArray()));
            sw.Write(origDatas);
            sw.Close();
        }
    }
}
