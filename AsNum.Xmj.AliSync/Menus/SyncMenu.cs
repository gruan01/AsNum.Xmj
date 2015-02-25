using AsNum.Common.Extends;
using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.AliSync.ViewModels;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace AsNum.Xmj.AliSync.Menus {

    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.Sync)]
    public class SyncMenu : MenuItemBase {

        public override string Header {
            get {
                return "订单同步";
            }
        }

        public override ICollection<IMenuItem> SubItems {
            get {
                var sds = EnumHelper.GetDescriptions<OrderStatus>()
                .Select(s => new MenuItem(s.Value, () => StatusClick(s.Key))).ToList<IMenuItem>();
                //sds.Add(new MenuItem("", null));
                return sds;
            }
        }

        private static bool Running = false;
        public OrderSyncResultViewModel ResultVM = null;
        private void StatusClick(OrderStatus status) {
            if (Running) {
                MessageBox.Show("亲,同步正在进行中.");
                return;
            }
            Running = true;

            var acsetting = new AccountSetting();
            this.ResultVM = new OrderSyncResultViewModel();
            this.ResultVM.CloseAble = false;
            Task.Factory.StartNew(() => {
                foreach (var acc in acsetting.Value) {
                    var os = new InternalOrderSync(acc.User, acc.Pwd);
                    os.OrderListReturned += os_OrderListReturned;
                    os.OrderDealed += os_OrderDealed;
                    os.Sync(status, new SmartSync().Value);
                }
            }, TaskCreationOptions.LongRunning)
            .ContinueWith(t => {
                this.ResultVM.CloseAble = true;
                Running = false;
                MessageBox.Show("全部同步完成");
                t.Dispose();
            });
            this.Sheel.Show(this.ResultVM, true);
        }

        void os_OrderDealed(object sender, OrderDealedEventArgs e) {
            this.ResultVM.AddDealedOrder(e.Account, e.Status, e.OrderNO, e.IsSuccess);
        }

        void os_OrderListReturned(object sender, OrderListEventArgs e) {
            this.ResultVM.AddStatus(e.Account, e.Status, e.Total);
        }
    }
}
