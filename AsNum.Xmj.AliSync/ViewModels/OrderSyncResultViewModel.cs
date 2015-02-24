using AsNum.Xmj.API.Entity;
using AsNum.Xmj.Common;
using Caliburn.Micro;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AsNum.Xmj.AliSync.ViewModels {

    public class OrderSyncResultViewModel : VMScreenBase {

        public override string Title {
            get {
                return "订单同步结果";
            }
        }

        public BindableCollection<SyncDetailByAccountViewModel> VMS {
            get;
            set;
        }

        public OrderSyncResultViewModel() {
            this.VMS = new BindableCollection<SyncDetailByAccountViewModel>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStatus(string account, OrderStatus status, int total) {
            account = account.ToLower();
            var vm = this.VMS.FirstOrDefault(v => string.Equals(v.Account, account));
            if (vm == null) {
                vm = new SyncDetailByAccountViewModel(account);
                this.VMS.Add(vm);
            }
            vm.SetStatus(status, total);

            this.NotifyOfPropertyChange("VMS");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDealedOrder(string account, OrderStatus status, string orderNO, bool success) {
            var vm = this.VMS.FirstOrDefault(v => string.Equals(v.Account, account));
            if (vm != null)
                vm.SetDealedOrder(status, success);
        }
    }
}
