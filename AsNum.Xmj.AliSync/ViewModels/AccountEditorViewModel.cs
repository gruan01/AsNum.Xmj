using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;

namespace AsNum.Xmj.AliSync.ViewModels {
    public class AccountEditorViewModel : VMScreenBase, ISettingEditor {

        public ISettingItem SettingItem {
            get;
            set;
        }

        public void Save() {
            this.SettingItem.Save();
        }

        public override string Title {
            get { return ""; }
        }

        public AccountEditorViewModel(ISettingItem item) {
            this.SettingItem = item;
        }

        public void Delete(Account acc) {
            var b = ((AccountSetting)this.SettingItem).Value.Remove(acc);
        }
    }
}
