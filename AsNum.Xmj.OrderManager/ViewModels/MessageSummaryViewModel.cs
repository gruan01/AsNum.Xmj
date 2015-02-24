using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AE = AsNum.Xmj.API.Entity;

namespace AsNum.Xmj.OrderManager.ViewModels {
    [Export(typeof(IStartUpModel)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class MessageSummaryViewModel : VMScreenBase, IStartUpModel {

        public override string Title {
            get {
                return "72小时消息汇总";
            }
        }

        public BindableCollection<AE.Message> OrderMsgs {
            get;
            set;
        }

        public BindableCollection<AE.Message2> Msgs {
            get;
            set;
        }

        public bool IsWaitingLoadOrderMsgs {
            get;
            set;
        }

        public bool IsWaitingLoadMsgs {
            get;
            set;
        }

        public void Load() {
            this.LoadOrderMsgs();
            this.LoadMsgs();
        }

        private void LoadOrderMsgs() {
            this.IsWaitingLoadOrderMsgs = true;
            this.NotifyOfPropertyChange(() => this.IsWaitingLoadOrderMsgs);
            DispatcherHelper.DoEvents();

            Task.Factory.StartNew(() => {
                var startTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-3), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
                List<AE.Message> msgs = MessageSync.QueryOrderMsg(startTime, null, null);
                this.OrderMsgs = new BindableCollection<AE.Message>(
                    msgs.Where(m => !m.SenderID.StartsWith("cn", StringComparison.OrdinalIgnoreCase))
                    .ToLookup(o => o.OrderNO)
                    .Select(l => l.OrderByDescending(ll => ll.CreateOn).FirstOrDefault())
                    .OrderByDescending(m => m.CreateOn)
                    );
                this.NotifyOfPropertyChange(() => this.OrderMsgs);

                this.IsWaitingLoadOrderMsgs = false;
                this.NotifyOfPropertyChange(() => this.IsWaitingLoadOrderMsgs);
                DispatcherHelper.DoEvents();
            });
        }

        private void LoadMsgs() {
            this.IsWaitingLoadOrderMsgs = true;
            this.NotifyOfPropertyChange(() => this.IsWaitingLoadOrderMsgs);
            DispatcherHelper.DoEvents();

            Task.Factory.StartNew(() => {
                var startTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-3), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
                List<AE.Message2> msgs = MessageSync.QueryMsg(startTime, null, null);
                this.Msgs = new BindableCollection<AE.Message2>(
                    msgs.Where(m => !m.SenderID.StartsWith("cn", StringComparison.OrdinalIgnoreCase))
                    .ToLookup(o => o.Sender)
                    .Select(l => l.OrderByDescending(ll => ll.CreateOn).FirstOrDefault())
                    .OrderByDescending(m => m.CreateOn)
                    );
                this.NotifyOfPropertyChange(() => this.Msgs);

                this.IsWaitingLoadMsgs = false;
                this.NotifyOfPropertyChange(() => this.IsWaitingLoadMsgs);
                DispatcherHelper.DoEvents();
            });
        }

        public void ReLoad() {

        }

        public void ViewOrder(AE.Message msg) {
            var vm = GlobalData.GetInstance<IOrderSearcher>();
            var cond = new OrderSearchCondition() {
                OrderNO = msg.OrderNO
            };

            vm.Search(cond);
            vm.Show();
        }

        //CM 的attach 不能识别重载
        public void ViewOrder2(AE.Message2 msg) {
            var vm = GlobalData.GetInstance<IOrderSearcher>();
            var cond = new OrderSearchCondition() {
                OrderNO = msg.OrderNO
            };

            vm.Search(cond);
            vm.Show();
        }

        public void ViewHistory(AE.Message2 msg) {
            var vm = new MessageListViewModel(msg.Account, msg.SenderID);
            GlobalData.GetInstance<ISheel>().Show(vm);
        }

        public void ShowProduct(AE.Message2 msg) {
            Process.Start(string.Format("http://www.aliexpress.com/item/a/{0}.html", msg.ProductID));
        }

    }
}
