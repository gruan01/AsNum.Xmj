using AsNum.Xmj.IBiz;
using System.Collections.Generic;

namespace AsNum.Xmj.Biz {
    public class BaseBiz : IBaseBiz {
        public Dictionary<string, string> Errors {
            get;
            set;
        }

        public bool HasError {
            get {
                return this.Errors != null && this.Errors.Count > 0;
            }
        }
    }
}
