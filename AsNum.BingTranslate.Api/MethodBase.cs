using AsNum.BingTranslate.Api.Attributes;
using AsNum.Common.Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace AsNum.BingTranslate.Api {

    public enum ReturnTypes {
        Json,
        Xml
    }

    public abstract class MethodBase : MarshalByRefObject {

        public abstract string MethodName {
            get;
        }

        public virtual string Url {
            get {
                return null;
            }
        }


        /// <summary>
        /// 请求方式
        /// </summary>
        public virtual HttpMethods RequestType {
            get {
                return HttpMethods.Get;
            }
        }

        /// <summary>
        /// 返回的结果类型
        /// </summary>
        public virtual ReturnTypes ReturnType {
            get {
                return ReturnTypes.Xml;
            }
        }

        public string ResultString {
            get;
            protected set;
        }


        /// <summary>
        /// API调用所需要的URL参数
        /// </summary>
        /// <returns></returns>
        internal virtual Dictionary<string, string> GetParams() {
            return ParameterHelper.GetParams(this);
        }

        /// <summary>
        /// 获取API调用的结果字符串
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        internal virtual async Task<string> GetResult(ApiClient client) {
            var url = client.GetApiUrl(this);
            var dic = this.GetParams();
            var rh = new RequestHelper(client.Cookies);

            if (!client.Token.IsInvalid) {
                rh.RequestHeader = new Dictionary<string, string>();
                rh.RequestHeader.Add("Authorization", string.Format("Bearer {0}", client.Token.Token));
            }

            var ctx = "";
            if (this.RequestType == HttpMethods.Get) {
                ctx = rh.Get(url, dic);
            } else {
                ctx = rh.Post(url, dic);
            }
            return ctx;
        }
    }

    public abstract class MethodBase<TResult> : MethodBase {

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [NeedAuth]
        public virtual async Task<TResult> Execute(ApiClient client) {
            this.ResultString = await this.GetResult(client);

            switch (this.ReturnType) {
                case ReturnTypes.Json:
                    return JsonConvert.DeserializeObject<TResult>(this.ResultString);
                case ReturnTypes.Xml:
                    var bytes = Encoding.UTF8.GetBytes(this.ResultString);
                    using (var stm = new MemoryStream(bytes)) {
                        var ser = new DataContractSerializer(typeof(TResult));
                        return (TResult)ser.ReadObject(stm);
                    }
                default:
                    return default(TResult);
            }
        }
    }
}
