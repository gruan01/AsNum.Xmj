using AsNum.Common.Extends;
using AsNum.Xmj.Common;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AsNum.Xmj.Setting2.ViewModels {
    [Export]
    public class SettingViewModel : VMScreenBase {

        public override string Title {
            get { return "设置中心2"; }
        }

        [ImportMany]
        public IEnumerable<Lazy<ISettingGroup>> SettingGroups;


        public IObservableCollection<SettingCategoryViewModel> Categories {
            get {
                var cs = this.SettingGroups
                    .Where(s => s.Value.Category != SettingCategories.None)
                    .GroupBy(g => g.Value.Category)
                    .Select(g => new SettingCategoryViewModel(EnumHelper.GetDescription(g.Key), g.Select(gg => gg.Value).ToList()));
                var otherCs = this.SettingGroups
                    .Where(s => s.Value.Category == SettingCategories.None)
                    .Select(s => new SettingCategoryViewModel(s.Value.GroupName, s.Value));

                var bc = new BindableCollection<SettingCategoryViewModel>(cs);
                bc.AddRange(otherCs);
                return bc;
            }
        }

        public void Save() {
            foreach (var g in this.SettingGroups) {
                if (g.Value.Items == null)
                    continue;

                foreach (var i in g.Value.Items) {
                    i.Save();
                }
            }
        }

        //public Setting2ViewModel() {
        //    //http://caliburnmicro.codeplex.com/wikipage?title=Handling%20Custom%20Conventions&referringTitle=Documentation
        //    //ViewLocator.AddSubNamespaceMapping("", ".Views");
        //}

        public override object GetView(object context = null) {
            return base.GetView(context);
        }
    }
}
