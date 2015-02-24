using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.OrderManager.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.OrderManager.Menus {

    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.DataMQ)]
    public class ShamDeliveryUpdateMenu : MenuItemBase {
        public override string Header {
            get {
                return "已填未发处理";
            }
        }

        public override void Execute(object obj) {
            var vm = new BatchUpdateShamDeliveryOrderViewModel();
            this.Sheel.Show(vm);
        }
    }
}
