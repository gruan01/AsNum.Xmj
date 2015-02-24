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
        [Description("维护与查询"), Order(1)]
        DataMQ,

        [Description("数据同步")]
        Sync,

        [Description("报表"), Order(2)]
        Report
    }
}
