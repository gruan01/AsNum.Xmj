using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel.Composition;

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
    }
}
