using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System.Collections.Generic;

namespace AsNum.Xmj {
    public class TopMenuItem : MenuItemBase {

        private string header = "";

        public override string Header {
            get { return this.header; }
        }

        public int Order { get; set; }

        private ICollection<IMenuItem> subItems;
        public override ICollection<IMenuItem> SubItems {
            get {
                return this.subItems;
            }
        }

        public void SetSubItems(ICollection<IMenuItem> subItems) {
            this.subItems = subItems;
        }

        public TopMenuItem(string header, ICollection<IMenuItem> subs) {
            this.header = header;
            this.subItems = subs;
        }
    }
}
