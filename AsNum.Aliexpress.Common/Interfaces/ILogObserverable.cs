using System;
using System.Data;

namespace AsNum.Xmj.Common.Interfaces {
    public interface ILogObserverable<T> where T : ILog {
        void Attach(T logger);
        void Detach(T logger);
        void Notify(string msg, bool canClear = true);
        void Notify(Exception ex);
        void Notify(IDbCommand cmd);
    }
}
