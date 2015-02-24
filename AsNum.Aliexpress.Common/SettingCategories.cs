using System.ComponentModel;

namespace AsNum.Xmj.Common {
    public enum SettingCategories {

        [Description("常规")]
        Normal,

        [Description("网络")]
        Network,
        
        [Description("同步")]
        Sync,

        None
    }
}
