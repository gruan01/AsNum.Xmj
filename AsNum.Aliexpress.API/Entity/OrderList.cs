using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Entity {
    public class OrderList {

        [JsonProperty("totalItem")]
        public int Count { get; set; }

        private List<SuccinctOrder> orders = null;

        [JsonProperty("orderList")]
        public List<SuccinctOrder> Orders {
            get {
                if(this.orders == null)
                    this.orders = new List<SuccinctOrder>();
                return this.orders;
            }
            set {
                this.orders = value;
            }
        }
    }
}
