using AsNum.Xmj.API.Attributes;
using Newtonsoft.Json;

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

        [JsonProperty("StreetAddress")]
        [Param("streetAddress", Required = true)]
        public string Address {
            get;
            set;
        }

        [JsonProperty("addressId")]
        [Param("addressId", Required = true)]
        public long ID {
            get; set;
        }

        public string Street { get; set; }

        public string Postcode { get; set; }

        public bool IsDefault { get; set; }

        public string Email { get; set; }

        public bool IsNeedToUpdate { get; set; }

        public string Summary {
            get {
                return string.Format("{0}{1}; {2}{3}{4}{5}{6} {7}",
                    this.Name,
                    this.Phone,
                    this.Province,
                    this.City,
                    this.County,
                    this.Street,
                    this.Address,
                    this.Postcode
                    );
            }
        }
    }
}
