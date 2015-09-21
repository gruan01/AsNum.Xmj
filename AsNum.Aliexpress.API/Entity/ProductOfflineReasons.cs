using AsNum.Xmj.API.Attributes;
using System.ComponentModel;

namespace AsNum.Xmj.API.Entity {
    public enum ProductOfflineReasons {

        [Description("过期下架")]
        [SpecifyNameValue( Name = "expire_offline")]
        Expire,

        [Description("手动下架")]
        [SpecifyNameValue( Name = "user_offline")]
        Manual,

        [Description("违规下架")]
        [SpecifyNameValue( Name = "punish_offline")]
        Punish,

        [Description("降级下架")]
        [SpecifyNameValue( Name = "degrade_offline")]
        Degrade
    }
}
