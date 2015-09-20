using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System;

namespace AsNum.Xmj.Common {
    public abstract class MenuItemBase : IMenuItem {

        [Import]
        protected ISheel Sheel { get; set; }

        [Import]
        protected IWindowManager WindowManager { get; set; }

        public abstract string Header {
            get;
        }



        public virtual void Execute(object obj) {

        }


        public virtual ICollection<IMenuItem> SubItems {
            get {
                return null;
            }
        }

        public bool IsSeparator {
            get {
                return false;
            }
        }


        public virtual string Group {
            get { return ""; }
        }
    }
}
