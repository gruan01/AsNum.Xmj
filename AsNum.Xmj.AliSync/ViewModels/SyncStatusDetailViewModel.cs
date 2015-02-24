using AsNum.Common.Extends;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.Common;

namespace AsNum.Xmj.AliSync.ViewModels {
    public class SyncStatusDetailViewModel : VMScreenBase {

        public string title = "";
        public override string Title {
            get {
                return this.title;
            }
        }

        public int Total {
            get;
            set;
        }

        public int Dealed {
            get;
            set;
        }

        public OrderStatus Status {
            get;
            private set;
        }

        public SyncStatusDetailViewModel(OrderStatus status, int total) {
            this.Status = status;
            this.Total = total;
            this.title = EnumHelper.GetDescription(status);
        }

        public void SetDealed(bool success) {
            this.Dealed++;
            this.NotifyOfPropertyChange("Dealed");
        }
    }
}
