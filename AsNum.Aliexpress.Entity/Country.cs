using System.ComponentModel.DataAnnotations;

namespace AsNum.Xmj.Entity {
    public class Country {

        [StringLength(5), Key, Required]
        public string CountryCode { get; set; }

        [StringLength(50), Required]
        public string EnName { get; set; }

        [StringLength(30), Required]
        public string ZhName { get; set; }

        [StringLength(5)]
        public string PhoneCode { get; set; }
    }
}
