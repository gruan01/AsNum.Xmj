using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.OrderManager.ViewModels {
    [Export(typeof(IOrderDealSubView)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class PurchaseDetailViewModel : VMScreenBase, IOrderDealSubView {

        public override string Title {
            get {
                return "采购信息";
            }
        }

        public string OrderNO {
            get;
            set;
        }

        public PurchaseDetail Detail {
            get;
            set;
        }

        public IOrder OrderBiz { get; set; }

        public PurchaseDetailViewModel() {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
        }

        public void Build(Order order) {
            this.OrderNO = order.OrderNO;
            this.Detail = order.PurchasseDetail;

            if (this.Detail == null)
                this.Detail = new PurchaseDetail() {
                    OrderNO = this.OrderNO
                };

            this.NotifyOfPropertyChange(() => this.Detail);
        }

        public void Save() {
            this.OrderBiz.SavePurchaseDetail(this.Detail);
        }
    }
}
