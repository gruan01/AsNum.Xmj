using AsNum.Xmj.API.Attributes;
using System.ComponentModel;

namespace AsNum.Xmj.API.Entity {
    public enum ProductStatus {
        [Description("销售中")]
        [SpecifyValue("onSelling")]
        OnSelling,

        [Description("下架")]
        [SpecifyValue("offline")]
        Offline,

        [Description("审核中")]
        [SpecifyValue("auditing")]
        Auditing,

        [Description("审核不通过")]
        [SpecifyValue("editingRequired")]
        EditingRequired
    }
}
