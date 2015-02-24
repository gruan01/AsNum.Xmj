using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    public class Owner {
        [StringLength(20), Key, Column(Order = 0)]
        public string Account {
            get;
            set;
        }

        [StringLength(3)]
        public string QuickCode {
            get;
            set;
        }
        public AccountTypes AccountType {
            get;
            set;
        }

        [StringLength(30)]
        public string SenderCity { get; set; }

        [StringLength(30)]
        public string SenderFax { get; set; }

        [StringLength(30)]
        public string SenderMobi { get; set; }

        [StringLength(30)]
        public string SenderPhone { get; set; }

        [StringLength(30)]
        public string SenderName { get; set; }

        [StringLength(30)]
        public string SenderProvince { get; set; }

        [StringLength(90)]
        public string SenderAddress { get; set; }

        [StringLength(10)]
        public string SenderPostCode { get; set; }






        [StringLength(30)]
        public string PickupCity { get; set; }

        [StringLength(30)]
        public string PickupFax { get; set; }

        [StringLength(30)]
        public string PickupMobi { get; set; }

        [StringLength(30)]
        public string PickupPhone { get; set; }

        [StringLength(30)]
        public string PickupName { get; set; }

        [StringLength(30)]
        public string PickupProvince { get; set; }

        /// <summary>
        /// 县/区
        /// </summary>
        public string PickupCounty { get; set; }

        [StringLength(90)]
        public string PickupAddress { get; set; }

        [StringLength(10)]
        public string PickupPostCode { get; set; }
    }
}
