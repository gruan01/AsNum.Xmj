using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Report.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Report.Menus {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.DataAndReport)]
    public class AccountQuickCodeMenu : MenuItemBase {
        public override string Header {
            get {
                return "账户识别码维护";
            }
        }

        public override void Execute(object obj) {
            var vm = new AccountQuickCodeViewModel();
            this.Sheel.Show(vm, true);
        }
    }
}
