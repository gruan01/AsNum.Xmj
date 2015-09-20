using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Xmj.API.Methods {
    /// <summary>
    /// 根据订单号查询支持的线上发货服务
    /// </summary>
    public class LogisticsServiceByOrderID : MethodBase<List<SupportOnlineLogisticsService>> {

        protected override string APIName {
            get {
                return "api.getOnlineLogisticsServiceListByOrderId";
            }
        }

        [Param("orderId", Required = true)]
        public string OrderID { get; set; }

        [Param("goodsWeight")]
        public double Weight { get; set; }

        [Param("goodsWeight")]
        public int Width { get; set; }

        [Param("goodsHeight")]
        public int Height { get; set; }

        [Param("goodsLength")]
        public int Length { get; set; }


        public override List<SupportOnlineLogisticsService> Execute(Auth auth) {
            var str = this.GetResult(auth);
            var o = new {
                result = new List<SupportOnlineLogisticsService>(),
                success = false
            };
            o = JsonConvert.DeserializeAnonymousType(str, o);
            return o.result.OrderBy(i => i.ServiceName).ToList();
        }
    }
}
