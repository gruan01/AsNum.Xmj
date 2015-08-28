using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.BingTranslate.Api.Handlers {
    /// <summary>
    /// 需要进行认证(策略注入)
    /// </summary>
    public class NeedAuthHandler : ICallHandler {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {
            var client = (ApiClient)input.Inputs["client"];
            if (client == null)
                throw new ArgumentException("client");

            if (client.Token == null || client.Token.IsInvalid)
                client.DoAuth();

            return getNext()(input, getNext);
        }

        public int Order {
            get;
            set;
        }
    }
}
