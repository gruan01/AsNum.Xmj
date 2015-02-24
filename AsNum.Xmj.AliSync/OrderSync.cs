using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.AliSync {
    [Export(typeof(IOrderSync))]
    public class OrderSync : IOrderSync {

        public IOrder OrderBiz { get; set; }

        public OrderSync() {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
        }

        public void Sync(string orderID) {
            var order = this.OrderBiz.GetOrder(orderID, false);
            if (order != null) {
                var s = new AccountSetting();
                var acc = AccountHelper.GetAccount(order.Account);
                //var acc = s.Value.FirstOrDefault(a => a.User.Equals(order.Account, StringComparison.OrdinalIgnoreCase));
                if (acc != null) {
                    var sync = new InternalOrderSync(acc.User, acc.Pwd);
                    sync.Sync(order);
                }
            }
        }

        public void Sync(Xmj.Entity.OrderStatus status = OrderStatus.UNKNOW, bool smart = true) {
            throw new NotImplementedException();
        }
    }
}
