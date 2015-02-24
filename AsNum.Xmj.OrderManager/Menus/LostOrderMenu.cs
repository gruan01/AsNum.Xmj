using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.OrderManager.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.OrderManager.Menus {

    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.DataMQ)]
    public class LostOrderMenu : MenuItemBase {
        public override string Header {
            get {
                return "缺失订单查询";
            }
        }

        public override void Execute(object obj) {
            var vm = new LostOrderFinderViewModel();
            this.Sheel.Show(vm);
        }
    }
}
