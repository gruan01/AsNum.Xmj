using AsNum.Xmj.Common.Interfaces;
using System.Collections.Generic;

namespace AsNum.Xmj.Common {
    public interface ISettingGroup {

        /// <summary>
        /// 选项所在的分类
        /// </summary>
        SettingCategories Category { get; }

        /// <summary>
        /// 分组名称
        /// </summary>
        string GroupName { get; }

        string Desc { get; }

        // 必须实现 set 方法,要不然在 Bind 的时候,因为 Items 是只读的,就不能双向绑定
        ICollection<ISettingEditor> Items { get; set; }
    }
}
