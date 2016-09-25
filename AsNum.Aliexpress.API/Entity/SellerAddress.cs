using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Entity {
    public class SellerAddress {
        public string StreetAddress { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }
        public string MemberType { get; set; }
        public string Postcode { get; set; }
        public int AddressId { get; set; }
        public string TrademanageId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; }
        public string Email { get; set; }
        public string County { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
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
                    this.StreetAddress,
                    this.Postcode
                    );
            }
        }
    }
}
