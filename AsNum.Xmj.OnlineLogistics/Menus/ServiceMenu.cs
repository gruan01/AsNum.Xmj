using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.OnlineLogistics.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OnlineLogistics.Menus {

    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.DataAndReport)]
    public class ServiceMenu : MenuItemBase {
        public override string Header {
            get {
                return "物流服务商";
            }
        }

        public override string Group {
            get {
                return "data";
            }
        }

        public override void Execute(object obj) {
            var vm = new LogisticsServiceViewModel();
            this.Sheel.Show(vm);
        }
    }
}
