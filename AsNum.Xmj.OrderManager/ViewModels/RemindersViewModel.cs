using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class RemindersViewModel : VMScreenBase {
        public override string Title {
            get { return "未附款订单催单"; }
        }

        public OrderSearchCondition Cond { get; set; }

        public BindableCollection<Order> Orders { get; set; }

        private Order order = null;
        public Order CurrOrder {
            get {
                return this.order;
            }
            set {
                this.order = value;
                this.SubOrderVM.Build(value);
                this.MsgVM.Build(value);
                if (value != null) {
                    this.Msg = MsgTpl.Replace("{Name}", value.Buyer.Name);
                    this.NotifyOfPropertyChange(() => this.Msg);
                }
            }
        }

        public SubOrderViewModel SubOrderVM { get; set; }

        public OrderMessagesViewModel MsgVM { get; set; }

        public IOrder OrderBiz { get; set; }

        public string Msg { get; set; }

        private static string MsgTpl = "";

        static RemindersViewModel() {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReminderTpl.txt");
            if (File.Exists(file))
                MsgTpl = File.ReadAllText(file);
        }

        public RemindersViewModel() {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
            this.SubOrderVM = new SubOrderViewModel();
            this.MsgVM = new OrderMessagesViewModel();
            this.Cond = new OrderSearchCondition() {
                Status = OrderStatus.PLACE_ORDER_SUCCESS,
                TimesType = AsNum.Xmj.BizEntity.Conditions.OrderSearchCondition.TimesTypes.ByCreateOn,
                BeginAt = DateTime.Now.AddDays(-2),
                EndAt = DateTime.Now.AddHours(-1)
            };
        }

        public void Search() {
            var datas = this.OrderBiz.Search(this.Cond);
            this.Orders = new BindableCollection<Order>(datas);
            this.NotifyOfPropertyChange(() => this.Orders);
        }
    }
}
