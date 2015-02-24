using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;

namespace AsNum.Xmj.API.Methods {
    public class OrderFindByID : MethodBase<DetailOrder> {

        protected override string APIName {
            get { return "api.findOrderById"; }
        }

        [Param("orderId", Required = true)]
        public string OrderID { get; set; }
    }
}
