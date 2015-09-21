using AsNum.Xmj.API.Attributes;
using System.ComponentModel;

namespace AsNum.Xmj.API.Entity {
    public enum ShipmentSendTypes {
        [Description("全部发货")]
        [SpecifyNameValue(Name = "all")]
        Full,

        [Description("部分发货")]
        [SpecifyNameValue(Name = "part")]
        Part
    }
}
