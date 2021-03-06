﻿using AsNum.Common.Net;
using AsNum.Xmj.API.Attributes;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

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
        public async virtual Task<string> GetResult(Auth auth) {
            var dic = ParamHelper.GetParams(this);

            var url = auth.GetApiUrl(this.APIName, dic);

            var rh = new RequestHelper(auth.CookieContainer);
            this.ResultString = rh.Post(url, dic);
            return await Task.FromResult(this.ResultString);
        }
    }

    public abstract class MethodBase<T> : MethodBase /*where T : class*/ {

        [NeedAuth]
        public async virtual Task<T> Execute(Auth auth) {
            this.ResultString = await this.GetResult(auth);
            return JsonConvert.DeserializeObject<T>(this.ResultString);
        }

    }
}
