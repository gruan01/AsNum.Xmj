using AsNum.Xmj.Common;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Xmj.Setting2.ViewModels {
    public class SettingCategoryViewModel : VMScreenBase {

        private string title = "";
        public override string Title {
            get { return this.title; }
        }

        public List<SettingGroupViewModel> Groups { get; set; }

        public SettingCategoryViewModel(string groupName, List<ISettingGroup> groups) {
            this.title = groupName;
            this.Groups = groups.Select(g => new SettingGroupViewModel(g.GroupName, g.Items)).ToList();
        }

        public SettingCategoryViewModel(string groupName, ISettingGroup group) {
            this.title = groupName;
            this.Groups = new List<SettingGroupViewModel>() { 
                new SettingGroupViewModel(group.GroupName, group.Items)
            };
        }

    }
}
