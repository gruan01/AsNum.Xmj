using AsNum.Test.ViewModels;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsNum.Test {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.None)]
    public class Menu : MenuItemBase {
        public override string Header {
            get {
                return "测试";
            }
        }

        public override ICollection<IMenuItem> SubItems {
            get {
                return new List<IMenuItem>() {
                    new MenuItem("测试仪表盘", ()=>{
                        this.Execute(this);
                    })
                };
            }
        }

        public override void Execute(object obj) {
            var vm = new DashBoardTestViewModel();
            this.Sheel.Show(vm);
        }
    }
}
