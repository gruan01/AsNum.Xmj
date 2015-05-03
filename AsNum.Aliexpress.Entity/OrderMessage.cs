using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    public class OrderMessage {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID {
            get;
            set;
        }

        [StringLength(20), Required]
        public string OrderNO {
            get;
            set;
        }

        [ForeignKey("OrderNO")]
        public virtual Order OrderOf {
            get;
            set;
        }

        [StringLength(2000)]
        public string Content {
            get;
            set;
        }

        public DateTime CreateOn {
            get;
            set;
        }

        [StringLength(30)]
        public string Sender {
            get;
            set;
        }

    }
}
