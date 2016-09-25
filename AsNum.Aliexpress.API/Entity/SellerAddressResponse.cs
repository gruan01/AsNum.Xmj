using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Entity {
    public class SellerAddressResponse {


        [JsonProperty("pickupSellerAddressesList")]
        public List<OnlineLogisticsContacts> PickupAdds {
            get; set;
        }

        [JsonProperty("senderSellerAddressesList")]
        public List<OnlineLogisticsContacts> SenderAdds {
            get; set;
        }

        [JsonProperty("refundSellerAddressesList")]
        public List<OnlineLogisticsContacts> Refunds {
            get; set;
        }
    }
}
