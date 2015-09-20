using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    public class OrdeLogistic {

        [Key, Column(Order = 0), StringLength(30)]
        public string TrackNO {
            get;
            set;
        }

        //public int LogisticsType {
        //    get;
        //    set;
        //}

        [Key, Column(Order = 1)]
        [StringLength(20)]
        public string LogisticCode {
            get; set;
        }

        [Key, Column(Order = 2), StringLength(20)]
        public string OrderNO {
            get;
            set;
        }

        [ForeignKey("OrderNO")]
        public virtual Order OrderOf {
            get;
            set;
        }

        /// <summary>
        /// 为null 时，说明是错单
        /// </summary>
        public DateTime? SendOn {
            get;
            set;
        }


        /// <summary>
        /// 包裹重量
        /// </summary>
        [NotMapped]
        public decimal? Weight { get; set; }

        /// <summary>
        /// 费用
        /// </summary>
        [NotMapped]
        public decimal? Fee { get; set; }
    }
}
