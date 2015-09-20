using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.OrderManager.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.OrderManager.Menus {

    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.OrderAndProduct)]
    public class ShipmentMenu : MenuItemBase {
        public override string Header {
            get {
                return "批量发货";
            }
        }

        public override string Group {
            get {
                return "logistic";
            }
        }

        public override void Execute(object obj) {
            var vm = new BatchShipmentViewModel();
            this.Sheel.Show(vm, true);
        }
    }
}
