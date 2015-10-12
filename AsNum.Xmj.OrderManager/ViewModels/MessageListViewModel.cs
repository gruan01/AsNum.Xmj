using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync;
using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class MessageListViewModel : VMScreenBase {
        public override string Title {
            get {
                return "站内信";
            }
        }

        public string BuyerID {
            get;
            set;
        }


        public string BuyerName {
            get;
            set;
        }

        public string Account {
            get;
            set;
        }

        public BindableCollection<Message2> Msgs {
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

        public string Content {
            get;
            set;
        }

        public MessageListViewModel(string account, string buyerID) {
            this.BuyerID = buyerID;
            this.Account = account;

            this.Load();
        }

        public async Task Load() {
            this.BusyText = "正在加载...";
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyText);
            DispatcherHelper.DoEvents();

            await Task.Factory.StartNew(async() => {
                var s = new AccountSetting();
                var acc = s.Value.FirstOrDefault(a => a.User.Equals(this.Account, StringComparison.OrdinalIgnoreCase));
                if (acc != null) {
                    var api = new APIClient(acc.User, acc.Pwd);
                    var method = new MessageQuery() {
                        BuyerID = this.BuyerID,
                        PageSize = 50
                    };
                    var datas = await api.Execute(method);
                    this.Msgs = new BindableCollection<Message2>(datas.Results);
                    this.NotifyOfPropertyChange(() => this.Msgs);

                    this.IsBusy = false;
                    this.NotifyOfPropertyChange(() => this.IsBusy);

                    var buyer = this.Msgs.FirstOrDefault(m => !m.SenderID.StartsWith("cn", StringComparison.OrdinalIgnoreCase));
                    if (buyer != null)
                        this.BuyerName = buyer.Sender;
                    else
                        this.BuyerName = this.BuyerID;

                    this.NotifyOfPropertyChange(() => this.BuyerName);
                }
            });
        }

        public void Refresh() {
            this.Load();
        }

        public void ShowHistory() {
            var vm = GlobalData.GetInstance<IOrderSearcher>();
            var sheel = GlobalData.GetInstance<ISheel>();
            var cond = new OrderSearchCondition() {
                BuyerID = this.BuyerID
            };

            vm.Search(cond);
            vm.Show(this.BuyerName + "的购买记录");
        }

        public void ShowProduct(Message2 msg) {
            Process.Start(string.Format("http://www.aliexpress.com/item/a/{0}.html", msg.ProductID));
        }


        public void ShowImage(Message2 msg) {
            if (!string.IsNullOrWhiteSpace(msg.FileUrl))
                Process.Start(msg.FileUrl);
        }

        public void Send() {
            this.BusyText = "正在发送...";
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyText);
            Task.Factory.StartNew(() => {
                MessageSync.SendMessage(this.Account, this.BuyerID, this.Content);
                this.IsBusy = false;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            });
        }
    }
}
