using AsNum.Xmj.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.Common {
    public class MenuSeparator : IMenuItem {
        public string Group {
            get {
                return "";
            }
        }

        public string Header {
            get {
                return "";
            }
        }

        public bool IsSeparator {
            get {
                return true;
            }
        }

        public ICollection<IMenuItem> SubItems {
            get {
                return null;
            }
        }

        public void Execute(object obj) {
            
        }
    }
}
