using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;

namespace AsNum.Xmj.API.Methods {
    public class OrderShipment : MethodBase<NormalResult> {
        protected override string APIName {
            get {
                return "api.sellerShipment";
            }
        }

        [Param("outRef", Required = true)]
        public string OrderNO {
            get;
            set;
        }

        //[EnumNameParam("serviceName", Required = true)]
        //public LogisticsTypes LogisticsType {
        //    get;
        //    set;
        //}

        [Param("serviceName", Required = true)]
        public string LogisticsType {
            get; set;
        }

        [Param("logisticsNo", Required = true)]
        public string TrackingNO {
            get;
            set;
        }

        [EnumNameParam("sendType")]
        public ShipmentSendTypes SendType {
            get;
            set;
        }

        [Param("description")]
        public string Description {
            get;
            set;
        }

        [Param("trackingWebsite")]
        public string TrackingWebSite {
            get;
            set;
        }

        [NeedAuth]
        public override NormalResult Execute(Auth auth) {
            var str = this.GetResult(auth);

            var o = new {
                success = true,
                error_message = "",
                error_code = ""
            };

            o = JsonConvert.DeserializeAnonymousType(str, o);

            //var o = JsonConvert.DeserializeObject(str);
            //var success = (o as JObject).Value<bool>("success");

            var result = new NormalResult() {
                Success = o.success,
                ErrorCode = o.error_code,
                ErrorInfo = o.error_message
            };

            return result;
        }
    }
}
