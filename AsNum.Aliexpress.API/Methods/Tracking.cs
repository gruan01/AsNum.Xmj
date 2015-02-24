using AsNum.Xmj.API.Attributes;

namespace AsNum.Xmj.API.Methods {
    public class Tracking : MethodBase<object> {
        protected override string APIName {
            get {
                return "api.queryTrackingResult";
            }
        }

        [Param("logisticsNo", Required = true)]
        public string TrackingNO {
            get;
            set;
        }

        [Param("toArea", Required = true)]
        public string CountryCode {
            get;
            set;
        }

        [Param("outRef", Required = true)]
        public string OrderNO {
            get;
            set;
        }

        private string origin = "ESCROW";
        [Param("origin")]
        public string Origin {
            get {
                return this.origin;
            }
        }

        [Param("serviceName", Required = true)]
        public string ServiceName {
            get;
            set;
        }
    }
}
