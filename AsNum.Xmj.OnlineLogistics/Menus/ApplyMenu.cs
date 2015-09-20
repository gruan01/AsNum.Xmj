using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.OnlineLogistics.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.OnlineLogistics.Menus {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.OrderAndProduct)]
    public class ApplyMenu : MenuItemBase {

        public override string Group {
            get {
                return "Logistic";
            }
        }

        public override string Header {
            get { return "申请线上发货"; }
        }

        public override void Execute(object obj) {
            var vm = new ApplyViewModel();
            this.Sheel.Show(vm);
        }
    }
}
