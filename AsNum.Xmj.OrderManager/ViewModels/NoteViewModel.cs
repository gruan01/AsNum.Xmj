using AsNum.Common.Extends;
using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AsNum.Xmj.OrderManager.ViewModels {
    [Export(typeof(IOrderDealSubView)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class NoteViewModel : VMScreenBase, IOrderDealSubView {

        public override string Title {
            get {
                return "留言及采购备注";
            }
        }

        public string OrderNO {
            get;
            set;
        }
        private string Account = "";

        //public List<OrderMessageEx> Msgs {
        //    get;
        //    set;
        //}

        public OrderNote OrderNote {
            get;
            set;
        }

        public bool IsBusy {
            get;
            set;
        }

        public string BusyText {
            get;
            set;
        }

        public OrderMessagesViewModel MsgListVM { get; set; }

        private Order Order = null;

        public IOrder OrderBiz { get; set; }


        public NoteViewModel() {
            this.MsgListVM = new OrderMessagesViewModel();
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
        }

        public void Build(Order order) {

            this.Order = order;

            this.OrderNO = order.OrderNO;
            this.Account = order.Account;
            //this.Msgs = this.DealMessage(order.Messages)
            //    .OrderByDescending(m => m.CreateOn)
            //    .ToList();

            this.OrderNote = order.Note;
            if (this.OrderNote == null) {
                this.OrderNote = new OrderNote() {
                    OrderNO = order.OrderNO
                };
            }

            this.NotifyOfPropertyChange(() => this.OrderNote);
            //this.NotifyOfPropertyChange(() => this.Msgs);
            this.MsgListVM.Build(order);
        }

        //private List<OrderMessageEx> DealMessage(IEnumerable<OrderMessage> msgs) {
        //    if (msgs == null || msgs.Count() == 0)
        //        return new List<OrderMessageEx>();

        //    var mes = msgs.Select(m => {
        //        var me = new OrderMessageEx();
        //        //DynamicCopy.CopyProperties(m, me);
        //        DynamicCopy.CopyTo(m, me);
        //        return me;
        //    }).ToList();

        //    var f = mes.First().Sender;
        //    mes.ForEach(m => {
        //        m.Left = m.Sender.Equals(f);
        //    });

        //    return mes;
        //}

        public void Sync() {

            this.IsBusy = true;
            this.BusyText = "正在同步订单留言...";
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyText);

            DispatcherHelper.DoEvents();

            var msgs = MessageSync.SyncByOrderNO(this.OrderNO, this.Account);
            var msgs1 = msgs.Select(m => new OrderMessage {
                ID = m.ID,
                Content = m.Content,
                OrderNO = this.OrderNO,
                Sender = m.Sender,
                CreateOn = m.CreateOn
            }).ToList();
            //this.Msgs = this.DealMessage(msgs1);

            this.NotifyOfPropertyChange("Msgs");

            //if (this.Msgs != null && this.Msgs.Count > 0)
            if (msgs1 != null && msgs1.Count > 0) {
                this.Order.Messages = msgs1;
                this.Build(this.Order);
                this.OrderBiz.SaveOrderMessage(msgs1);
            }
            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyText);
        }

        public void SavePurchaseNote() {
            this.OrderBiz.SaveOrderNote(this.OrderNote);
        }


        public WriteMsgViewModel MsgVM {
            get;
            set;
        }

        public bool IsShowWriteDialog {
            get;
            set;
        }

        public void WriteMsg() {
            this.IsShowWriteDialog = true;
            this.MsgVM = new WriteMsgViewModel();
            this.MsgVM.OnSuccess = (orderNo, account) => {
                this.IsShowWriteDialog = false;
                this.NotifyOfPropertyChange(() => this.IsShowWriteDialog);
                DispatcherHelper.DoEvents();
                this.Sync();
            };
            this.MsgVM.Build(this.Order);
            this.NotifyOfPropertyChange(() => this.MsgVM);
            this.NotifyOfPropertyChange(() => this.IsShowWriteDialog);
        }

    }
}
