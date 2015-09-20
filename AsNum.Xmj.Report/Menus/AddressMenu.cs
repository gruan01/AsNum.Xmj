using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Report.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Report.Menus {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.OrderAndProduct)]
    public class AddressMenu : MenuItemBase {
        public override string Header {
            get {
                return "发货地址";
            }
        }

        public override string Group {
            get {
                return "logistic";
            }
        }

        public override void Execute(object obj) {
            this.Sheel.Show(new AddressExportViewModel(), true);
        }
    }
}
