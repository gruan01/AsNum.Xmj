using AsNum.Xmj.API.Attributes;

namespace AsNum.Xmj.API.Methods {
    public class MessageSend : MethodBase<object> {
        protected override string APIName {
            get {
                return "api.addMessage";
            }
        }

        [Param("buyerId", Required=true)]
        public string BuyerID {
            get;
            set;
        }

        [Param("content", Required=true)]
        public string Ctx {
            get;
            set;
        }
    }
}
