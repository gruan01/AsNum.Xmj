using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Setting2.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Setting2.Menus {
    [Export(typeof(IMenuItem))]
    [ExportMetadata("TopMenuTag", TopMenuTags.System)]
    public class SettingMenu : MenuItemBase {

        //[Import]
        //public SettingViewModel VM;

        public override string Header {
            get { return "设置中心"; }
        }

        public override void Execute(object obj) {
            //this.WindowManager.ShowDialog(this.VM);
            var vm = new SettingViewModel();
            GlobalData.MefContainer.ComposeParts(vm);
            this.Sheel.Show(vm);
        }
    }
}
