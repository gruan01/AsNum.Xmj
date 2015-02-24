using System;

namespace AsNum.Common.Attributes {
    /// <summary>
    /// 用于 BaseQuery
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MapToAttribute : Attribute {


        /// <summary>
        /// 映射到的属性
        /// </summary>
        public string Field {
            get;
            set;
        }

        /// <summary>
        /// 比较类型
        /// </summary>
        public MapToOpts Opt {
            get;
            set;
        }

        /// <summary>
        /// 是否忽略大小写
        /// </summary>
        public bool IgnoreCase {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        public MapToAttribute(string field) {
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException("field");

            this.Field = field;
        }

    }

    /// <summary>
    /// 比较类型
    /// </summary>
    public enum MapToOpts {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,

        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,

        /// <summary>
        /// 大于
        /// </summary>
        Gt,

        /// <summary>
        /// 大于等于
        /// </summary>
        GtOrEqual,

        /// <summary>
        /// 小于
        /// </summary>
        Lt,

        /// <summary>
        /// 小于等
        /// </summary>
        LtOrEqual,

        /// <summary>
        /// 以 XXX 开始(字符串)
        /// </summary>
        StartWith,

        /// <summary>
        /// 以XXX结尾(字符串)
        /// </summary>
        EndWith,

        /// <summary>
        /// 包含XXX(字符串)
        /// </summary>
        Include
    }
}
