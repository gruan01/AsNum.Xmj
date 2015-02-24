using System;

namespace AsNum.Xmj.Logger {
    public class InfoItem {

        public DateTime Time {
            get;
            set;
        }

        public string Msg {
            get;
            set;
        }

        public bool canClear = true;

        public bool CanClear {
            get {
                return this.canClear;
            }
            set {
                this.canClear = value;
            }
        }
    }
}
