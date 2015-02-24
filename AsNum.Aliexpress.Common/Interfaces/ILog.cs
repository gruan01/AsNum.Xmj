using System;
using System.Data;

namespace AsNum.Xmj.Common.Interfaces {
    public interface ILog {
        void Log(Exception ex);
        void Log(string msg, bool canClear = true);
        void Log(IDbCommand cmd);
    }
}
