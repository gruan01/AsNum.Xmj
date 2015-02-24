using AsNum.Xmj.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.Common {
    public class MenuItem : IMenuItem {
        public string Header {
            get;
            private set;
        }

        public ICollection<IMenuItem> SubItems {
            get;
            set;
        }

        private Action executeAct = null;

        public MenuItem(string header, Action act) {
            this.Header = header;
            this.executeAct = act;
        }

        public void Execute(object obj) {
            if (this.executeAct != null)
                this.executeAct();
        }
    }
}
