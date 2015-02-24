using AsNum.Xmj.API.Handlers;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace AsNum.Xmj.API.Attributes {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NeedAuthAttribute : HandlerAttribute {

        public override ICallHandler CreateHandler(IUnityContainer container) {
            return new NeedAuthHandler();
        }


    }
}
