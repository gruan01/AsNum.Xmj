using AsNum.Xmj.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Common {
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public class LogObserverable : ILogObserverable<ILog> {

        private List<ILog> Observers = new List<ILog>();

        public LogObserverable() { 
        
        }

        public void Attach(ILog logger) {
            this.Observers.Add(logger);
        }

        public void Detach(ILog logger) {
            this.Observers.Remove(logger);
        }

        public void Notify(string msg, bool canClear) {
            this.Observers.ForEach(o => {
                o.Log(msg, canClear);
            });
        }

        public void Notify(Exception ex) {
            this.Observers.ForEach(o => {
                o.Log(ex);
            });
        }

        public void Notify(System.Data.IDbCommand cmd) {
            this.Observers.ForEach(o => {
                o.Log(cmd);
            });
        }
    }
}
