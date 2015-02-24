using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    public class OrderDetailAttribute {

        //[ForeignKey("DetailOf")]
        //[Key, Column(Order = 0)]
        //public int DetailID {
        //    get;
        //    set;
        //}

        [ForeignKey("DetailOf")]
        [Key, StringLength(20, MinimumLength = 10), Required, Column(Order = 0)]
        public string OrderNO {
            get;
            set;
        }

        //子单号
        [ForeignKey("DetailOf")]
        [Key, StringLength(20), Required, Column(Order = 1)]
        public string SubOrderNO {
            get;
            set;
        }

        [Key, Column(Order = 2)]
        public int Order {
            get;
            set;
        }

        [StringLength(100)]
        public string AttrStr {
            get;
            set;
        }

        /// <summary>
        /// 兼容老订单
        /// </summary>
        [StringLength(200)]
        public string CompatibleStr {
            get;
            set;
        }

        public virtual OrderDetail DetailOf {
            get;
            set;
        }
    }
}
