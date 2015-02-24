using System.ComponentModel.DataAnnotations;

namespace AsNum.Xmj.Entity {
    public class EUBShipper {

        [StringLength(20), Key]
        public string Flag {
            get;
            set;
        }

        [StringLength(30), Required]
        public string Name {
            get;
            set;
        }

        [StringLength(30), Required]
        public string Phone {
            get;
            set;
        }

        [StringLength(50), Required]
        public string Email {
            get;
            set;
        }

        [StringLength(6), Required]
        public string ZipCode {
            get;
            set;
        }

        [StringLength(30), Required]
        public string Province {
            get;
            set;
        }

        [StringLength(50), Required]
        public string Street {
            get;
            set;
        }

        [StringLength(50), Required]
        public string City {
            get;
            set;
        }

        [StringLength(50), Required]
        public string County {
            get;
            set;
        }

        public bool IsDefault {
            get;
            set;
        }
    }
}
