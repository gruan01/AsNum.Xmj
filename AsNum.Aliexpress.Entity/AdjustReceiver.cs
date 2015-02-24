using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Aliexpress.Entity {
    public class AdjustReceiver {

        [Key, StringLength(20, MinimumLength = 10), Required]
        public string OrderNO {
            get;
            set;
        }

        [ForeignKey("OrderNO")]
        public virtual Order Order {
            get;
            set;
        }

        /// <summary>
        /// 收件人
        /// </summary>
        [StringLength(20)]
        public string Name {
            get;
            set;
        }

        [StringLength(20)]
        public string ZipCode {
            get;
            set;
        }

        [StringLength(200)]
        public string Address {
            get;
            set;
        }

        [StringLength(5)]
        public string CountryCode {
            get;
            set;
        }

        [ForeignKey("CountryCode")]
        public Country Country {
            get;
            set;
        }

        [StringLength(20)]
        public string City {
            get;
            set;
        }

        [StringLength(20)]
        public string Province {
            get;
            set;
        }

        [StringLength(20)]
        public string Phone {
            get;
            set;
        }

        [StringLength(10)]
        public string PhoneArea {
            get;
            set;
        }

        [StringLength(20)]
        public string Mobi {
            get;
            set;
        }

    }
}
