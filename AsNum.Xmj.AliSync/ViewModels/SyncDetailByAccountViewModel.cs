using AsNum.Xmj.API.Entity;
using AsNum.Xmj.Common;
using Caliburn.Micro;
using System.Linq;

namespace AsNum.Xmj.AliSync.ViewModels {
    public class SyncDetailByAccountViewModel : VMScreenBase {

        public string Account {
            get;
            private set;
        }
        public override string Title {
            get {
                return this.Account;
            }
        }

        public BindableCollection<SyncStatusDetailViewModel> VMS {
            get;
            set;
        }

        public SyncDetailByAccountViewModel(string account) {
            this.Account = account;
            this.VMS = new BindableCollection<SyncStatusDetailViewModel>();
        }

        public void SetStatus(OrderStatus status, int total) {
            if (!this.VMS.Any(v => v.Status == status))
                this.VMS.Add(new SyncStatusDetailViewModel(status, total));
        }

        public void SetDealedOrder(OrderStatus status, bool success) {
            var vm = this.VMS.FirstOrDefault(v => v.Status == status);
            if (vm != null) {
                vm.SetDealed(success);
            }
        }
    }
}
