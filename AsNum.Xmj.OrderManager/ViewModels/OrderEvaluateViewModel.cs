using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using AsNum.Xmj.OrderManager.Views;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class OrderEvaluateViewModel : VMScreenBase {
        public override string Title {
            get {
                return "交易评介";
            }
        }

        public BindableCollection<Order> Orders {
            get;
            private set;
        }

        private Order currOrder;
        public Order CurrOrder {
            get {
                return this.currOrder;
            }
            set {
                this.currOrder = value;
                this.NotifyOfPropertyChange(() => this.SubOrders);
            }
        }

        public BindableCollection<OrderDetail> SubOrders {
            get {
                if (this.CurrOrder != null) {
                    return new BindableCollection<OrderDetail>(this.CurrOrder.Details);
                } else
                    return null;
            }
        }

        public List<int> Stars {
            get {
                return Enumerable.Range(1, 5).Reverse().ToList();
            }
        }
        public int Star {
            get;
            set;
        }

        public string Ctx {
            get;
            set;
        }

        public IOrder OrderBiz { get; set; }

        public OrderEvaluateViewModel(List<string> orders) {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();

            var cond = new OrderSearchCondition() {
                SpecifyOrders = orders
            };
            this.Orders = new BindableCollection<Order>(this.OrderBiz.Search(cond));
            this.CurrOrder = this.Orders.FirstOrDefault();
        }

        public void ShowDetailVM(Order order, DataGridRowDetailsEventArgs eventArgs) {
            var ctrl = (ContentControl)eventArgs.DetailsElement.FindName("detailView");
            var view = new OrderDetailView();
            var model = new OrderDetailViewModel(order);

            ViewModelBinder.Bind(model, view, null);
            ctrl.Content = view;
        }

        public void Send() {
            var acs = new AccountSetting();
            var ac = acs.Value.FirstOrDefault(a => a.User.Equals(this.CurrOrder.Account, StringComparison.OrdinalIgnoreCase));
            if (ac != null) {
                var api = new APIClient(ac.User, ac.Pwd);
                var method = new OrderEvaluate() {
                    OrderNo = this.CurrOrder.OrderNO,
                    Score = this.Star,
                    Content = this.Ctx
                };
                var o = api.Execute(method);
            }
        }
    }
}
