using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    /// <summary>
    /// 用于采购，管理员留 
    /// </summary>
    public class OrderNote {

        [ForeignKey("OrderOf")]
        [Key, StringLength(20)]
        public string OrderNO {
            get;
            set;
        }

        [StringLength(1000)]
        public string Note {
            get;
            set;
        }

        public virtual Order OrderOf {
            get;
            set;
        }
    }
}
