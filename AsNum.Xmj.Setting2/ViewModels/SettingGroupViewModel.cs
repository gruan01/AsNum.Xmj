using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System.Collections.Generic;

namespace AsNum.Xmj.Setting2.ViewModels {
    public class SettingGroupViewModel : VMScreenBase {

        public string GroupName { get; set; }

        public ICollection<ISettingEditor> Items { get; set; }

        public SettingGroupViewModel(string groupName, ICollection<ISettingEditor> editors) {
            this.GroupName = groupName;
            this.Items = editors;
        }


        public override string Title {
            get { return ""; }
        }
    }
}
