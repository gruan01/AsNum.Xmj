using System.ComponentModel.DataAnnotations;

namespace AsNum.Xmj.Entity {
    public class Buyer {

        [StringLength(20), Required, Key]
        public string BuyerID { get; set; }

        [StringLength(50), Required]
        public string Name { get; set; }

        /// <summary>
        /// <remarks>
        /// 有些客人并没有留邮箱
        /// </remarks>
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 国家代码, 这里获取的有非标准的代码，长度也不是标准的2,所以不能做为外键
        /// </summary>
        [StringLength(5), Required]
        public string CountryCode{ get; set; }

        //[ForeignKey("CountryCode")]
        //public Country Country { get; set; }
    }
}
