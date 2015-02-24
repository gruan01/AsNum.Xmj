using AsNum.Common.Net;
using AsNum.Xmj.API.Attributes;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API {

    public abstract class MethodBase : MarshalByRefObject {

        public Auth Auth;

        protected abstract string APIName {
            get;
        }

        public string ResultString {
            get;
            protected set;
        }

        public static T GetMethod<T>(Auth auth) where T : MethodBase {
            var method = PolicyInjection.Create<T>();
            method.Auth = auth;
            return method;
        }

        [NeedAuth]
        public virtual string GetResult(Auth auth) {
            var url = auth.GetApiUrl(this.APIName);

            var dic = ParamHelper.GetParams(this);

            var rh = new RequestHelper(auth.CookieContainer);
            this.ResultString = rh.Post(url, dic);
            return this.ResultString;
        }
    }

    public abstract class MethodBase<T> : MethodBase /*where T : class*/ {

        [NeedAuth]
        public virtual T Execute(Auth auth) {
            this.ResultString = this.GetResult(auth);
            return JsonConvert.DeserializeObject<T>(this.ResultString);
        }

    }
}
