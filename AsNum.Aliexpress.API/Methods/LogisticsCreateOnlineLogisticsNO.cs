using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Methods {
    /// <summary>
    /// 创建线上发货物流订单
    /// </summary>
    public class LogisticsCreateOnlineLogisticsNO : MethodBase<NormalResult> {

        protected override string APIName {
            get { return "api.createWarehouseOrder"; }
        }

        [Param("tradeOrderId", Required = true)]
        public string OrderID { get; set; }

        /// <summary>
        /// 交易订单来源,AE订单为ESCROW ；国际站订单为“SOURCING”
        /// </summary>
        [Param("tradeOrderFrom", Required = true)]
        public string OrderFrom { get; set; }

        /// <summary>
        /// 国内快递ID
        /// </summary>
        [Param("domesticLogisticsCompanyId", Required = true)]
        public string LocalLogisticCompanyID { get; set; }

        /// <summary>
        /// 国内快递公司名称
        /// </summary>
        [Param("domesticLogisticsCompany")]
        public string LocalLogisticCompanyName { get; set; }

        /// <summary>
        /// 国内快递运单号,长度1-32
        /// </summary>
        [Param("domesticTrackingNo", Required = true)]
        public string LocalTrackingNO { get; set; }

        [Param("remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 根据订单号获取线上发货物流方案“API获取用户选择的实际发货物流服务
        /// </summary>
        [Param("warehouseCarrierService")]
        public string Service { get; set; }

        public OnlineLogisticsContacts Receiver { get; set; }

        public OnlineLogisticsContacts Sender { get; set; }

        public OnlineLogisticsContacts Pickup { get; set; }

        [JsonParam("addressDTOs", Required = true)]
        public object Addresses {
            get {
                return new {
                    receiver = ParamHelper.GetParams(this.Receiver),
                    sender = ParamHelper.GetParams(this.Sender),
                    pickup = ParamHelper.GetParams(this.Pickup)
                };
            }
        }


        public List<OnlineLogisticsDeclareInfo> Declares { get; set; }

        [JsonParam("declareProductDTOs", Required = true)]
        public object _Declares {
            get {
                return ParamHelper.GetParams(this.Declares);
            }
        }

        /// <summary>
        /// 国际单号并不一定同步返回, 需要在执行一次查询
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public override NormalResult Execute(Auth auth) {
            var str = this.GetResult(auth);

            //{"result":{"warehouseOrderId":38339973,"tradeOrderId":65150272717171,"errorCode":1,"tradeOrderFrom":"ESCROW","success":true,"outOrderId":30573162247},"success":true}

            var o = new {
                success = false
            };

            o = JsonConvert.DeserializeAnonymousType(str, o);

            return new NormalResult() {
                Success = o.success,
                ErrorInfo = str
            };
        }
    }
}
