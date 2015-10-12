using AsNum.Common.Extends;
using AsNum.Xmj.AliSync;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AsNum.Xmj.OrderManager.ViewModels {
    //[PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class OrderDetailViewModel : VMScreenBase {

        public override string Title {
            get {
                return "";
            }
        }

        public Order Order {
            get;
            set;
        }

        public string Receiver {
            get {
                return this.Order.AdjReceiver != null ? this.Order.AdjReceiver.Name : this.Order.OrgReceiver.Name;
            }
        }

        public string Address {
            get {
                return this.Order.AdjReceiver != null ? this.Order.AdjReceiver.Address : this.Order.OrgReceiver.FullAddress;
            }
        }

        public string Phone {
            get {
                return this.Order.AdjReceiver != null ? this.Order.AdjReceiver.Phone : this.Order.OrgReceiver.Phone;
            }
        }

        public string Mobi {
            get {
                return this.Order.AdjReceiver != null ? this.Order.AdjReceiver.Mobi : this.Order.OrgReceiver.Mobi;
            }
        }

        public string PostCode {
            get {
                return this.Order.AdjReceiver != null ? this.Order.AdjReceiver.ZipCode : this.Order.OrgReceiver.ZipCode;
            }
        }

        public string Level { get; private set; }

        public string LogisticsType {
            get {
                return string.Join(",", this.Order.Details.Select(d => d.LogisticsType));
            }
        }

        public OrderDetailViewModel(Order order) {
            this.Order = order;
            var level = order.Buyer.Level;
            //一天更新一次
            if (level == null || level.UpdateOn.AddDays(1) < DateTime.Now) {
                Task.Factory.StartNew(() => this.UpdateLevel());
            } else {
                this.Level = level.Level;
            }
        }

        private async Task UpdateLevel() {
            var method = new API.Methods.BuyerLevel() {
                BuyerID = this.Order.BuyerID
            };
            var api = AccountHelper.GetAccount(this.Order.Account);
            var client = new API.APIClient(api.User, api.Pwd);
            var level = await client.Execute(method);

            var biz = GlobalData.GetInstance<IBuyer>();
            biz.UpdateLevel(this.Order.BuyerID, level);
            this.Level = level;

            this.NotifyOfPropertyChange(() => this.Level);
        }

        public void OpenOrder() {
            Process.Start(string.Format("http://trade.aliexpress.com/orderDetail.htm?orderId={0}", this.Order.OrderNO));
        }

        public void HistoryOrder() {
            var cond = new OrderSearchCondition() {
                BuyerID = this.Order.BuyerID
            };
            var orderSearcher = GlobalData.GetInstance<IOrderSearcher>();
            orderSearcher.Show(string.Format("{0} 的购买记录", this.Order.Buyer.Name));
            orderSearcher.Search(cond);
        }

        public void CopyOrderNo() {
            //Clipboard.SetText(this.Order.OrderNO, TextDataFormat.Text);
            //http://stackoverflow.com/questions/12769264/openclipboard-failed-when-copy-pasting-data-from-wpf-datagrid
            Clipboard.SetDataObject(this.Order.OrderNO);
        }

        public void Update() {
            var sync = GlobalData.GetInstance<IOrderSync>();
            sync.Sync(this.Order.OrderNO);
        }

        public void MsgsList() {
            var vm = new MessageListViewModel(this.Order.Account, this.Order.BuyerID);
            var sheel = GlobalData.GetInstance<ISheel>();
            sheel.Show(vm);
        }
    }
}
