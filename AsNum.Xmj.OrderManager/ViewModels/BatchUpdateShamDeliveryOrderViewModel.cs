using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using Caliburn.Micro;
using System.Linq;
using System.Text.RegularExpressions;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class BatchUpdateShamDeliveryOrderViewModel : VMScreenBase {
        public override string Title {
            get {
                return "已填未发订单处理";
            }
        }

        public string OrderNOs {
            get;
            set;
        }

        public BindableCollection<Order> Orders {
            get;
            set;
        }

        public IOrder OrderBiz { get; set; }

        public BatchUpdateShamDeliveryOrderViewModel() {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
        }

        public void Search() {
            var ons = Regex.Split(this.OrderNOs, "\r\n").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            var cond = new OrderSearchCondition() { 
                IsShamShipping = true,
                SpecifyOrders = ons
            };
            this.Orders = new BindableCollection<Order>(this.OrderBiz.Search(cond));
            this.NotifyOfPropertyChange(() => this.Orders);
        }
    }
}
