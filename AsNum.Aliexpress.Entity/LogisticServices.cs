using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.Entity {
    public class LogisticServices {

        /// <summary>
        /// 物流服务代码
        /// </summary>
        [Key, StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        [StringLength(50)]
        public string NameEn { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        [StringLength(50)]
        public string NameCn { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        [StringLength(30)]
        public string Company { get; set; }

        /// <summary>
        /// 最小处理时间
        /// </summary>
        public int MiniProcessDays { get; set; }

        /// <summary>
        /// 最大处理时间
        /// </summary>
        public int MaxProcessDays { get; set; }

        /// <summary>
        ///  物流追踪号码校验规则
        /// </summary>
        [StringLength(200)]
        public string CheckRegex { get; set; }

        /// <summary>
        /// 越大越靠前
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否常用
        /// </summary>
        public bool IsUsual { get; set; }

        [NotMapped]
        public string Display {
            get {
                return !string.IsNullOrWhiteSpace(this.NameCn) ? this.NameCn : this.NameEn;
            }
        }
    }
}
