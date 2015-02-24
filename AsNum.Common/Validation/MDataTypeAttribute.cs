using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CtripSZ.Frameworks.Mvc.Validation {
    /// <summary>
    /// 可指定显示格式的 DataTypeAttribute
    /// 目前只用来在验证日期时，使用自定义格式
    /// </summary>
    public class MDataTypeAttribute : DataTypeAttribute {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        public MDataTypeAttribute(DataType type , string format = "")
            : base(type) {
            base.DisplayFormat = new DisplayFormatAttribute() {
                DataFormatString = format
            };
        }
    }
}
