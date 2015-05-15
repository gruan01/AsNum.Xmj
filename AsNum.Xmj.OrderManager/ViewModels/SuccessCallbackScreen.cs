using AsNum.Xmj.Common;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public abstract class SuccessCallbackScreen : VMScreenBase {
        public Action<string> OnSuccess { get; set; }
    }
}
