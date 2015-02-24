
namespace AsNum.Xmj.Common.Interfaces {
    public interface ISettingEditor {

        ISettingItem SettingItem { get; set; }

        void Save();
    }
}
