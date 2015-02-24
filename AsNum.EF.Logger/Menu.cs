using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.EFLogger.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.EFLogger {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.System)]
    public class Menu : MenuItemBase {
        public override string Header {
            get {
                return "SQL捕获";
            }
        }

        public override void Execute(object obj) {
            var vm = new SQLLoggerViewModel();
            this.Sheel.Show(vm);
        }
    }
}
