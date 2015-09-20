using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.ProductManager.ViewModels;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.ProductManager.Menus {

    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.OrderAndProduct)]
    public class FindProductMenu : MenuItemBase {
        public override string Header {
            get {
                return "产品查询";
            }
        }

        public override void Execute(object obj) {
            var vm = new ProductQueryViewModel();
            this.Sheel.Show(vm);
        }
    }
}
