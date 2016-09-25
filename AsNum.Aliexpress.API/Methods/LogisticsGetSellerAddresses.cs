using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {
    public class LogisticsGetSellerAddresses : MethodBase<SellerAddressResponse> {

        protected override string APIName {
            get {
                return "alibaba.ae.api.getLogisticsSellerAddresses";
            }
        }


        [Param("request", Required = true)]
        private string _Types {
            get {
                var arr = this.Types.Select(t => t.ToString());
                return JsonConvert.SerializeObject(arr);
            }
        }


        public List<AddTypes> Types {
            get;
        }

        public LogisticsGetSellerAddresses() {
            this.Types = new List<AddTypes>();
        }

        public enum AddTypes {
            sender,
            pickup,
            refund
        }
    }
}
