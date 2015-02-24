using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace AsNum.Xmj.API.Handlers {
    [Serializable]
    public class SaveAuthDataHandler : ICallHandler {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext) {
            var opts = (Auth)input.Target;
            opts.PropertyChanged += opts_PropertyChanged;

            return getNext()(input, getNext);
        }

        void opts_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var opts = (Auth)sender;
            AuthDataPersistence.Save(opts);
        }

        public int Order {
            get;
            set;
        }
    }
}
