using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Common.SettingEditors;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.AliSync.Settings {
    [Export(typeof(ISettingGroup))]
    public class APISettingGroup : ISettingGroup {

        public SettingCategories Category {
            get { return SettingCategories.None; }
        }

        public string GroupName {
            get { return "Ali API 设置"; }
        }

        public string Desc {
            get { return ""; }
        }

        private ICollection<ISettingEditor> items = new List<ISettingEditor>() {
            new StringEditorViewModel(new APPKey()),
            new StringEditorViewModel(new APPSecretCode())
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
