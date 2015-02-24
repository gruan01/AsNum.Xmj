using AsNum.Xmj.API.Attributes;
using System.ComponentModel;

namespace AsNum.Xmj.API.Entity {
    public enum ProductOfflineReasons {

        [Description("过期下架")]
        [SpecifyValue("expire_offline")]
        Expire,

        [Description("手动下架")]
        [SpecifyValue("user_offline")]
        Manual,

        [Description("违规下架")]
        [SpecifyValue("punish_offline")]
        Punish,

        [Description("降级下架")]
        [SpecifyValue("degrade_offline")]
        Degrade
    }
}
