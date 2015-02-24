using AsNum.Xmj.Common;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Logger.ViewModels {
    [Export(typeof(AsNum.Xmj.Common.Interfaces.ILog))]
    public class ExceptionShowerViewModel : VMScreenBase, AsNum.Xmj.Common.Interfaces.ILog {

        public override string Title {
            get {
                return "异常";
            }
        }

        private List<Exception> exceptions = new List<Exception>();
        public BindableCollection<Exception> Exceptions {
            get;
            set;
        }

        [ImportingConstructor]
        public ExceptionShowerViewModel(LogObserverable observer) {
            observer.Attach(this);
            this.Exceptions = new BindableCollection<Exception>(exceptions);
        }

        public void Log(Exception ex) {
            while(ex.InnerException != null){
                ex = ex.InnerException;
            }
            this.Exceptions.Add(ex);
        }

        public void Log(string msg, bool canClear) {
            //throw new NotImplementedException();
        }


        public void Log(System.Data.IDbCommand cmd) {
            
        }
    }
}
