using AsNum.Xmj.API.Handlers;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace AsNum.Xmj.API.Attributes {

    [AttributeUsage(AttributeTargets.Class)]
    internal class SaveAuthDataAttribute : HandlerAttribute {

        public override ICallHandler CreateHandler(IUnityContainer container) {
            return new SaveAuthDataHandler();
        }

    }
}
