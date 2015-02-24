using AsNum.Xmj.AliSync.ViewModels;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.AliSync.Settings {
    [Export(typeof(ISettingGroup))]
    public class AccountSettingGroup : ISettingGroup {
        public SettingCategories Category {
            get { return SettingCategories.None; }
        }

        public string GroupName {
            get { return "账户设置"; }
        }

        public string Desc {
            get { return ""; }
        }

        private ICollection<ISettingEditor> items = new List<ISettingEditor>() {
            new AccountEditorViewModel(new AccountSetting())
        };

        public ICollection<ISettingEditor> Items {
            get {
                return this.items;
            }
            set {
                this.items = value;
            }
        }
    }
}
