using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Menus {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.System)]
    public class StartUpMenu : MenuItemBase {
        public override string Header {
            get {
                return "Welcome";
            }
        }
        public override void Execute(object obj) {
            this.Sheel.Show(new StartUpViewModel(), true);
        }
    }
}
