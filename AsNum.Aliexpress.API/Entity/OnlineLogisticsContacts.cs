using AsNum.Xmj.API.Attributes;

namespace AsNum.Xmj.API.Entity {

    /// <summary>
    /// 线上发货联系人信息
    /// </summary>
    public class OnlineLogisticsContacts {

        [Param("city", Required = true)]
        public string City { get; set; }


        /// <summary>
        /// 县区
        /// </summary>
        [Param("county")]
        public string County { get; set; }

        [Param("country", Required = true)]
        public string CountryCode { get; set; }

        /// <summary>
        /// 只用来显示
        /// </summary>
        public string CountryName { get; set; }

        [Param("fax")]
        public string Fax { get; set; }

        [Param("mobile")]
        public string Mobile { get; set; }

        [Param("phone")]
        public string Phone { get; set; }

        [Param("name", Required = true)]
        public string Name { get; set; }

        [Param("postcode")]
        public string PostCode { get; set; }

        [Param("province", Required = true)]
        public string Province { get; set; }

        [Param("streetAddress", Required = true)]
        public string Address {
            get;
            set;
        }
    }
}
