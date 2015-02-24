using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.OrderManager.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.OrderManager.Menus {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.DataMQ)]
    public class OrderQueryMenu : MenuItemBase {

        public override string Header {
            get { return "订单查询"; }
        }

        public override void Execute(object obj) {
            this.Sheel.Show(new OrderQueryViewModel());
        }
    }
}
