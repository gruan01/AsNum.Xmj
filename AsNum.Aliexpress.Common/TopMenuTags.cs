using AsNum.Common.Annoations;
using System.ComponentModel;

namespace AsNum.Xmj.Common {
    public enum TopMenuTags {
        None = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("系统"), Order(0)]
        System,
        /// <summary>
        /// 
        /// </summary>
        [Description("订单&产品"), Order(1)]
        OrderAndProduct,

        [Description("数据同步")]
        Sync,

        [Description("数据&报表"), Order(2)]
        DataAndReport,

        [Description("工具"), Order(3)]
        Tools
    }
}
