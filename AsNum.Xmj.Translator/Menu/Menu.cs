using AsNum.BingTranslate.Api;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Translator.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsNum.Common.Extends;

namespace AsNum.Xmj.Translator.Menu {
    [Export(typeof(IMenuItem))]
    [ExportMetadata("TopMenuTag", TopMenuTags.Tools)]
    public class SettingMenu : MenuItemBase {
        public override string Header {
            get { return "翻译"; }
        }

        static SettingMenu() {
            var clientID = ConfigurationManager.AppSettings.Get(ConstValue.ClientIDAppSettingKey, "");
            var secretCode = ConfigurationManager.AppSettings.Get(ConstValue.SECKeyAppSettingKey, "");
            ApiClient.Init(clientID, secretCode);
        }

        public override void Execute(object obj) {
            //base.Execute(obj);
            var vm = new TranslatorViewModel();
            this.Sheel.Show(vm);
        }
    }
}
