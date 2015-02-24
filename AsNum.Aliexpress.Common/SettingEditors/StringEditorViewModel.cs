using AsNum.Xmj.Common.Interfaces;

namespace AsNum.Xmj.Common.SettingEditors {
    public class StringEditorViewModel : VMScreenBase , ISettingEditor {

        public ISettingItem SettingItem {
            get;
            set;
        }

        public StringEditorViewModel(ISettingItem item) {
            this.SettingItem = item;
        }


        public void Save() {
            this.SettingItem.Save();
        }

        public override string Title {
            get { return ""; }
        }
    }
}
