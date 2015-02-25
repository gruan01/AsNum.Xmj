using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.OrderManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.Menus {

    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.DataMQ)]
    public class RemindersMenu : MenuItemBase {

        public override string Header {
            get { return "未付款订单催单"; }
        }

        public override void Execute(object obj) {
            var vm = new RemindersViewModel();
            this.Sheel.Show(vm);
        }
    }
}
