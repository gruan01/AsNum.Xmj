using AsNum.WPF.Controls;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Linq;

namespace AsNum.Xmj.OrderManager.ViewModels {
    [Export(typeof(IOrderDealSubView)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class LogisticViewModel : VMScreenBase, IOrderDealSubView {
        public override string Title {
            get {
                return "物流信息";
            }
        }

        public BindableCollection<OrdeLogistic> Logistics {
            get;
            set;
        }

        public bool IsShamShipping {
            get;
            set;
        }

        public bool CanIsShamShipping {
            get;
            private set;
        }

        public bool CanFillTrackNO {
            get;
            private set;
        }

        public bool CanUpdate {
            get;
            private set;
        }

        public bool IsBusy {
            get;
            set;
        }

        public SuccessCallbackScreen FVM {
            get;
            set;
        }



        public BindableCollection<IQuickTrackButton> TrackBtns {
            get;
            set;
        }

        private string OrderNO;


        public bool CanTrack {
            get;
            set;
        }

        public IOrder OrderBiz { get; set; }

        public LogisticViewModel() {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
        }

        public void Build(Order order) {

            this.OrderNO = order.OrderNO;

            this.Logistics = new BindableCollection<OrdeLogistic>(order.Logistics);
            this.IsShamShipping = order.IsShamShipping;
            this.CanIsShamShipping = order.Status == OrderStatus.WAIT_BUYER_ACCEPT_GOODS;
            this.CanUpdate = this.CanIsShamShipping;
            this.CanFillTrackNO = order.Status == OrderStatus.WAIT_SELLER_SEND_GOODS || order.Status == OrderStatus.SELLER_PART_SEND_GOODS;

            this.CanTrack = this.Logistics != null && this.Logistics.Count > 0;

            this.NotifyOfPropertyChange(() => this.Logistics);
            this.NotifyOfPropertyChange(() => this.IsShamShipping);
            this.NotifyOfPropertyChange(() => this.CanIsShamShipping);
            this.NotifyOfPropertyChange(() => this.CanUpdate);
            this.NotifyOfPropertyChange(() => this.CanFillTrackNO);
            this.NotifyOfPropertyChange(() => this.CanTrack);

            this.TrackBtns = new BindableCollection<IQuickTrackButton>();
            var exports = GlobalData.MefContainer.GetExports<IQuickTrackButton, IQuickTrackButtonMetadata>();
            foreach (var e in exports) {
                if (this.Logistics.Any(l => (l.LogisticsType & e.Metadata.Support) == l.LogisticsType)) {
                    this.TrackBtns.Add(e.Value);
                }
            }
            this.NotifyOfPropertyChange(() => this.TrackBtns);
        }

        public void Update() {
            this.OrderBiz.UpdateShamShippingStatus(this.OrderNO, this.IsShamShipping);
        }

        public void FillTrackNO() {
            this.FVM = new FillTrackNOViewModel(this.OrderNO);
            this.FVM.OnSuccess = o => {
                this.IsBusy = false;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            };
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.FVM);
            DispatcherHelper.DoEvents();
        }

        public void Track(IQuickTrackButton tracker) {
            tracker.Track(this.Logistics.ToList());
        }

        public void ExtendReceiveDays() {
            this.FVM = new ExtendReceiveDaysViewModel(this.OrderNO);
            this.FVM.OnSuccess = o => {
                this.IsBusy = false;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            };
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.FVM);
            DispatcherHelper.DoEvents();
        }
    }
}
