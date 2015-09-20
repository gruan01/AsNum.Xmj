using System.Collections.Generic;

namespace AsNum.Xmj.Common.Interfaces {
    public interface IMenuItem {

        bool IsSeparator { get; }

        string Header { get; }

        string Group { get; }

        ICollection<IMenuItem> SubItems { get; }

        void Execute(object obj);
    }
}
