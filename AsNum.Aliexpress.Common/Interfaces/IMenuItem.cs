using System.Collections.Generic;

namespace AsNum.Xmj.Common.Interfaces {
    public interface IMenuItem {

        string Header { get; }

        ICollection<IMenuItem> SubItems { get; }

        void Execute(object obj);
    }
}
