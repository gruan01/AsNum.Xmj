using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync;
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
using AsNum.Common.Extends;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class RemindersViewModel : VMScreenBase {
        public override string Title {
            get { return "未附款订单催单"; }
        }

        public OrderSearchCondition Cond { get; set; }

        public BindableCollection<ReminderOrder> Orders { get; set; }

        private ReminderOrder order = null;
        public ReminderOrder CurrOrder {
            get {
                return this.order;
            }
            set {
                this.order = value;
                this.SubOrderVM.Build(value);
                this.MsgVM.Build(value);
                this.NotifyOfPropertyChange(() => this.CurrOrder);
                //if (value != null) {
                //    //this.Msg = MsgTpl.Replace("{Name}", value.Buyer.Name);
                //    this.Msg = value.Msg;
                //    this.NotifyOfPropertyChange(() => this.Msg);
                //}
            }
        }

        public SubOrderViewModel SubOrderVM { get; set; }

        public OrderMessagesViewModel MsgVM { get; set; }

        public IOrder OrderBiz { get; set; }

        public string Msg { get; set; }

        private static string MsgTpl = "";

        public bool IsBusy { get; set; }

        public string BusyText { get; set; }

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
            this.Cond.Pager.AllowPage = false;
        }

        public void Search() {
            var datas = this.OrderBiz.Search(this.Cond)
                .Select(o => {
                    var ro = new ReminderOrder();
                    o.CopyToExcept(ro);
                    ro.Msg = MsgTpl.Replace("{Name}", o.Buyer.Name);
                    return ro;
                });
            this.Orders = new BindableCollection<ReminderOrder>(datas);
            this.NotifyOfPropertyChange(() => this.Orders);
            this.SyncMsg(false);
        }

        public void SyncMsg(bool all = true) {
            Task.Factory.StartNew(() => {

                this.IsBusy = true;
                this.BusyText = "正在同步订单留言...";
                this.NotifyOfPropertyChange(() => this.IsBusy);
                this.NotifyOfPropertyChange(() => this.BusyText);

                DispatcherHelper.DoEvents();
                var willSyncOrders = this.Orders as IEnumerable<ReminderOrder>;
                if (!all) {
                    willSyncOrders = this.Orders.Where(o => !o.IsRemindered);
                }
                foreach (var o in willSyncOrders) {
                    Task.Factory.StartNew(() => {
                        this.Sync(o);
                    }, TaskCreationOptions.AttachedToParent)
                    .ContinueWith(t => {
                        t.Dispose();
                    }, TaskContinuationOptions.AttachedToParent);
                }
            }).ContinueWith(t => {
                this.IsBusy = false;
                this.NotifyOfPropertyChange(() => this.IsBusy);
                this.NotifyOfPropertyChange(() => this.BusyText);
                this.Orders.Refresh();
                t.Dispose();
            });
        }

        private void Sync(ReminderOrder order) {
            var msgs = MessageSync.SyncByOrderNO(order.OrderNO, order.Account);
            var msgs1 = msgs.Select(m => new OrderMessage {
                ID = m.ID,
                Content = m.Content,
                OrderNO = order.OrderNO,
                Sender = m.Sender,
                CreateOn = m.CreateOn
            }).ToList();

            this.NotifyOfPropertyChange("Msgs");
            if (msgs1 != null && msgs1.Count > 0) {
                order.Messages = msgs1;
                this.OrderBiz.SaveOrderMessage(msgs1);
            }
        }

        public void Send() {
            Task.Factory.StartNew(() => {

                this.IsBusy = true;
                this.BusyText = "正在发送催款留言...";
                this.NotifyOfPropertyChange(() => this.IsBusy);
                this.NotifyOfPropertyChange(() => this.BusyText);

                foreach (var o in this.Orders.Where(oo => !oo.IsRemindered)) {
                    Task.Factory.StartNew(() => {
                        this.Send(o);
                    }, TaskCreationOptions.AttachedToParent)
                    .ContinueWith(t => {
                        t.Dispose();
                    }, TaskContinuationOptions.AttachedToParent);
                }

                DispatcherHelper.DoEvents();
            }).ContinueWith(t => {
                this.IsBusy = false;
                this.NotifyOfPropertyChange(() => this.IsBusy);
                this.NotifyOfPropertyChange(() => this.BusyText);
                t.Dispose();
            })
            .ContinueWith(t => {
                this.SyncMsg(false);
            });
        }

        public void Send(ReminderOrder order) {
            MessageSync.WriteOrderMessage(order.Account, order.OrderNO, order.Msg);
        }
    }
}
