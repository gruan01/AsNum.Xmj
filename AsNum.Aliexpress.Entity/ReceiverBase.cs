using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    public abstract class ReceiverBase {

        [Key, StringLength(20), Required]
        public string OrderNO {
            get;
            set;
        }

        [ForeignKey("OrderNO")]
        public virtual Order OrderFor {
            get;
            set;
        }

        [StringLength(50), Required]
        public string Name {
            get;
            set;
        }

        [StringLength(50), Required]
        public string ZipCode {
            get;
            set;
        }

        [StringLength(200), Required]
        public string Address {
            get;
            set;
        }


        [StringLength(5), Required]
        public string CountryCode {
            get;
            set;
        }

        public virtual Country Country {
            get;
            set;
        }

        [StringLength(100), Required]
        public string City {
            get;
            set;
        }

        [StringLength(100), Required]
        public string Province {
            get;
            set;
        }

        [StringLength(100)]
        public string Phone {
            get;
            set;
        }

        [StringLength(50)]
        public string PhoneArea {
            get;
            set;
        }

        [StringLength(50)]
        public string Mobi {
            get;
            set;
        }

        public string FullAddress {
            get {
                return string.Format("{0} , {1} , {2} , {3}", this.Address, this.City, this.Province, this.Country != null ? this.Country.EnName : this.CountryCode);
            }
        }

    }
}
