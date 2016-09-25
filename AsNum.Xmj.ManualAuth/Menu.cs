using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AsNum.Xmj.ManualAuth {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.System)]
    public class Menu : MenuItemBase {
        public override string Header
        {
            get
            {
                return "手动认证";
            }
        }

        public override void Execute(object obj) {
            base.Execute(obj);

            //var setting = new Dictionary<string, object>() {
            //        {"WindowStartupLocation",WindowStartupLocation.CenterScreen},
            //        {"ResizeModel",ResizeMode.NoResize},
            //        {"Width",800},
            //        {"Height",500}
            //    };


            WindowManager.ShowDialog(new AuthViewModel());
        }
    }
}
