using AsNum.Aliexpress.Common;
using AsNum.Aliexpress.Common.Interfaces;
using AsNum.Xmj.OrderManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.Menus {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.System)]
    public class PropertyCatelogsMgrMenu : MenuItemBase {
        public override string Header {
            get {
                return "属性挂载分类";
            }
        }

        public override void Execute(object obj) {
            var vm = new PProductCatelogViewModel();
            this.Sheel.Show(vm, true);
        }
    }
}
