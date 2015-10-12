using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.Common;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class ExtendReceiveDaysViewModel : SuccessCallbackScreen {
        public override string Title {
            get { return "延长收货时间"; }
        }

        public string OrderNO { get; set; }

        public int Days { get; set; }

        private Order Order;

        public IOrder OrderBiz { get; set; }

        public string BusyString { get; set; }

        public bool IsBusy { get; set; }

        public ExtendReceiveDaysViewModel(string orderNo) {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
            this.OrderNO = orderNo;

            this.Order = this.OrderBiz.GetOrder(this.OrderNO);
        }

        public async Task Extend() {

            this.IsBusy = true;
            this.BusyString = "请稍候...";
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyString);
            DispatcherHelper.DoEvents();

            var success = false;
            var s = new AccountSetting();
            var account = s.Value.FirstOrDefault(a => a.User.Equals(this.Order.Account, StringComparison.OrdinalIgnoreCase));
            if (account != null) {
                var method = new OrderExtendReceiveTime() {
                    OrderNO = this.OrderNO,
                    Days = this.Days < 0 ? 0 : this.Days
                };
                var api = new APIClient(account.User, account.Pwd);
                var o = await api.Execute(method);
                success = o.Success;
                this.BusyString = success ? "成功" : "失败";
            } else {
                this.BusyString = "未取到账户信息";
            }

            this.NotifyOfPropertyChange(() => this.BusyString);
            DispatcherHelper.DoEvents();
            Thread.Sleep(2000);

            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.IsBusy);

            if (success && this.OnSuccess != null) {
                this.OnSuccess(this.OrderNO);
            }
        }

        
    }
}
