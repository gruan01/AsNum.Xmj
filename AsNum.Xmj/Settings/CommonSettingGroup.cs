using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Settings {
    [Export(typeof(ISettingGroup))]
    public class CommonSettingGroup : ISettingGroup {
        public SettingCategories Category {
            get { return SettingCategories.Normal; }
        }

        public string GroupName {
            get { return "Test 设置"; }
        }

        public string Desc {
            get { return ""; }
        }


        public ICollection<ISettingEditor> Items {
            get;
            set;
        }
    }
}
