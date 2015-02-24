using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Report.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Report.Menus {

    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.Report)]
    public class TurnoverMenu : MenuItemBase {

        public override string Header {
            get { return "销售额报表"; }
        }

        public override void Execute(object obj) {
            var vm = new TurnoverViewModel();
            base.Sheel.Show(vm);
        }

    }
}
