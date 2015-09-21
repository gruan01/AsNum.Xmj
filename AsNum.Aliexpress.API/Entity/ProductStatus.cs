using AsNum.Xmj.API.Attributes;
using System.ComponentModel;

namespace AsNum.Xmj.API.Entity {
    public enum ProductStatus {
        [Description("销售中")]
        [SpecifyNameValue(Name = "onSelling")]
        OnSelling,

        [Description("下架")]
        [SpecifyNameValue(Name = "offline")]
        Offline,

        [Description("审核中")]
        [SpecifyNameValue(Name = "auditing")]
        Auditing,

        [Description("审核不通过")]
        [SpecifyNameValue(Name = "editingRequired")]
        EditingRequired
    }
}
