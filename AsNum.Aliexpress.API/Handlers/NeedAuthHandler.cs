using Microsoft.Practices.Unity.InterceptionExtension;

namespace AsNum.Xmj.API.Handlers {
    public class NeedAuthHandler : ICallHandler {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {
            //var auth = (input.Target as MethodBase).Auth;
            var auth = (Auth)input.Inputs["auth"];

            //if (auth.AuthToken == null || auth.AuthToken.IsInvalid)
            //    AuthHelper.DoAuth(auth);
            ////auth.DoAuth( auth.User , auth.Pwd);

            if (auth.AuthToken.HasExpiressed)
                auth.RefreshAccessToken();

            return getNext()(input, getNext);
        }

        public int Order {
            get;
            set;
        }
    }
}
