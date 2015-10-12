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
                return "最近的消息";
            }
        }

        public BindableCollection<AE.MessageRelation> OrderMsgs {
            get;
            set;
        }

        public BindableCollection<AE.MessageRelation> Msgs {
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

        public async void Load() {
            await Task.Factory.StartNew(async () => {
                await this.LoadOrderMessages();
                await this.LoadMessages();
            });
        }

        //private void LoadOrderMsgs() {
        //    this.IsWaitingLoadOrderMsgs = true;
        //    this.NotifyOfPropertyChange(() => this.IsWaitingLoadOrderMsgs);
        //    DispatcherHelper.DoEvents();

        //    Task.Factory.StartNew(() => {
        //        var startTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(-3), TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));
        //        List<AE.Message> msgs = MessageSync.QueryOrderMsg(startTime, null, null);
        //        this.OrderMsgs = new BindableCollection<AE.Message>(
        //            msgs.Where(m => !m.SenderID.StartsWith("cn", StringComparison.OrdinalIgnoreCase))
        //            .ToLookup(o => o.OrderNO)
        //            .Select(l => l.OrderByDescending(ll => ll.CreateOn).FirstOrDefault())
        //            .OrderByDescending(m => m.CreateOn)
        //            );
        //        this.NotifyOfPropertyChange(() => this.OrderMsgs);

        //        this.IsWaitingLoadOrderMsgs = false;
        //        this.NotifyOfPropertyChange(() => this.IsWaitingLoadOrderMsgs);
        //        DispatcherHelper.DoEvents();
        //    });
        //}

        private async Task LoadOrderMessages() {
            this.IsWaitingLoadOrderMsgs = true;
            this.NotifyOfPropertyChange(() => this.IsWaitingLoadOrderMsgs);
            DispatcherHelper.DoEvents();
            var datas = await this.LoadMsgs(AE.MessageTypes.Order, 10);
            this.OrderMsgs = new BindableCollection<AE.MessageRelation>(datas);
            this.NotifyOfPropertyChange(() => this.OrderMsgs);
            this.IsWaitingLoadOrderMsgs = false;
            this.NotifyOfPropertyChange(() => this.IsWaitingLoadOrderMsgs);
            DispatcherHelper.DoEvents();
        }

        private async Task LoadMessages() {
            this.IsWaitingLoadMsgs = true;
            this.NotifyOfPropertyChange(() => this.IsWaitingLoadMsgs);
            DispatcherHelper.DoEvents();
            var datas = await this.LoadMsgs(AE.MessageTypes.MessageCenter, 10);
            this.Msgs = new BindableCollection<AE.MessageRelation>(datas);
            this.NotifyOfPropertyChange(() => this.Msgs);
            this.IsWaitingLoadMsgs = false;
            this.NotifyOfPropertyChange(() => this.IsWaitingLoadMsgs);
            DispatcherHelper.DoEvents();
        }

        private async Task<BindableCollection<AE.MessageRelation>> LoadMsgs(AE.MessageTypes msgType, int perCount) {
            var msgs = await MessageSync.QueryRelations(msgType, perCount, true);
            var datas = new BindableCollection<AE.MessageRelation>(
                msgs.Where(m => !m.LastMessageIsOwn).OrderByDescending(m => m.LastMessageCreateOn)
                );

            return datas;
        }

        public void ReLoad() {

        }

        public void ViewOrder(AE.MessageRelation msg) {
            var vm = GlobalData.GetInstance<IOrderSearcher>();
            var cond = new OrderSearchCondition() {
                OrderNO = msg.ChannelID
            };

            vm.Search(cond);
            vm.Show();
        }

        //CM 的attach 不能识别重载
        public void ViewOrder2(AE.MessageRelation msg) {
            var vm = GlobalData.GetInstance<IOrderSearcher>();
            var cond = new OrderSearchCondition() {
                OrderNO = msg.ChannelID
            };

            vm.Search(cond);
            vm.Show();
        }

        public void ViewHistory(AE.MessageRelation msg) {
            var vm = new MessageListViewModel(msg.Account, msg.CustomerID);
            GlobalData.GetInstance<ISheel>().Show(vm);
        }

        public void ShowProduct(AE.Message2 msg) {
            Process.Start(string.Format("http://www.aliexpress.com/item/a/{0}.html", msg.ProductID));
        }

    }
}
