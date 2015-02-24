using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Settings {
    [Export(typeof(ISettingGroup))]
    public class NetworkGroupSetting : ISettingGroup {
        public SettingCategories Category {
            get {
                return SettingCategories.Network;
            }
        }

        public string GroupName {
            get {
                return "网络设置";
            }
        }

        public string Desc {
            get {
                return "";
            }
        }

        public ICollection<ISettingEditor> Items {
            get;
            set;
        }
    }
}
