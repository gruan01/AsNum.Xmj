using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Common.SettingEditors;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.AliSync.Settings {
    [Export(typeof(ISettingGroup))]
    public class SyncSettingGroup : ISettingGroup {
        public SettingCategories Category {
            get {
                return SettingCategories.Sync;
            }
        }

        public string GroupName {
            get {
                return "同步设置";
            }
        }

        public string Desc {
            get {
                return "";
            }
        }

        private ICollection<ISettingEditor> items = new List<ISettingEditor>() {
            new OnOffEditorViewModel(new SmartSync()),
            new StringEditorViewModel(new SmartSyncDays())
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
