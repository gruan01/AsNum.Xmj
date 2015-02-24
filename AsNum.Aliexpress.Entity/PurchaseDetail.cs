using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {

    /// <summary>
    /// 采购信息
    /// </summary>
    public class PurchaseDetail {


        [ForeignKey("OrderOf")]
        [Key, StringLength(20), Required, Column(Order = 1)]
        public string OrderNO {
            get;
            set;
        }


        public virtual Order OrderOf {
            get;
            set;
        }

        /// <summary>
        /// 是否以采购完
        /// </summary>
        public bool Completed {
            get;
            set;
        }

        public string Note {
            get;
            set;
        }
    }
}
