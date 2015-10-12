using AsNum.Common.Extends;
using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.BizEntity.Models;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace AsNum.Xmj.OnlineLogistics.ViewModels {
    public class ApplyViewModel : VMScreenBase {
        public override string Title {
            get { return "申请线上发货单"; }
        }

        public List<LocalLogisticsCompany> LocalLogisticsCompany { get; set; }

        public BindableCollection<ApplyItem> Datas { get; set; }

        public bool IsShowBusy { get; set; }
        public string BusyText { get; set; }

        public IOrder OrderBiz { get; set; }

        public ApplyViewModel() {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
            this.Datas = new BindableCollection<ApplyItem>();
            this.Datas.CollectionChanged += Datas_CollectionChanged;
        }

        void Datas_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.NewItems != null)
                foreach (ApplyItem item in e.NewItems)
                    item.PropertyChanged += item_PropertyChanged;

            if (e.OldItems != null)
                foreach (ApplyItem item in e.OldItems)
                    item.PropertyChanged -= item_PropertyChanged;
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            var p = e.PropertyName;
            if (p.Equals("OrderNO", System.StringComparison.OrdinalIgnoreCase)) {
                var item = (ApplyItem)sender;
                if (item != null) {
                    this.LoadData(item);
                    this.SetLocalLogisticCompany(item);
                }
            }
        }

        public void LoadData(ApplyItem item) {
            if (!string.IsNullOrWhiteSpace(item.OrderNO)) {
                var order = this.OrderBiz.GetOrder(item.OrderNO);
                if (order != null) {
                    item.Account = order.Account;
                    item.Status = order.Status;
                    item.OrderNote = (order.Note ?? new OrderNote()).Note;
                    //item.LogisticType = string.Join(",", order.Details.Select(s => EnumHelper.GetDescription(s.LogisticsType.ToEnum<AsNum.Xmj.Entity.LogisticsTypes>())));
                    item.LogisticType = string.Join(",", order.Details.Select(s => s.LogisticsType));

                    item.Receiver = new Xmj.API.Entity.OnlineLogisticsContacts();
                    item.Receiver.CountryCode = order.Receiver.CountryCode;
                    item.Receiver.CountryName = order.Receiver.Country.ZhName;
                    item.Receiver.Address = order.Receiver.Address;
                    item.Receiver.City = order.Receiver.City;
                    item.Receiver.Mobile = order.Receiver.Mobi;
                    item.Receiver.Name = order.Receiver.Name;
                    item.Receiver.Phone = order.Receiver.Phone;
                    item.Receiver.PostCode = order.Receiver.ZipCode;
                    item.Receiver.Province = order.Receiver.Province;

                    item.Sender = new Xmj.API.Entity.OnlineLogisticsContacts();
                    item.Sender.CountryCode = "CN";
                    item.Sender.Address = order.AccountOf.SenderAddress;
                    item.Sender.City = order.AccountOf.SenderCity;
                    item.Sender.Mobile = order.AccountOf.SenderMobi;
                    item.Sender.Name = order.AccountOf.SenderName;
                    item.Sender.Phone = order.AccountOf.SenderPhone;
                    item.Sender.PostCode = order.AccountOf.SenderPostCode;
                    item.Sender.Province = order.AccountOf.SenderProvince;

                    item.Pickup = new Xmj.API.Entity.OnlineLogisticsContacts();
                    item.Pickup.CountryCode = "CN";
                    item.Pickup.County = order.AccountOf.PickupCounty;
                    item.Pickup.Address = order.AccountOf.PickupAddress;
                    item.Pickup.City = order.AccountOf.PickupCity;
                    item.Pickup.Mobile = order.AccountOf.PickupMobi;
                    item.Pickup.Name = order.AccountOf.PickupName;
                    item.Pickup.Phone = order.AccountOf.PickupPhone;
                    item.Pickup.PostCode = order.AccountOf.PickupPostCode;
                    item.Pickup.Province = order.AccountOf.PickupProvince;

                    item.LocalTrackNO = order.OrderNO;

                    var ds = order.Details.GroupBy(d => d.ProductID)
                        .Select(g => new OnlineLogisticsDeclareInfo {
                            ProductID = g.Key,
                            Count = g.Sum(gg => gg.Qty),
                            Weight = 0.5,
                            WithBattery = false,
                            Amount = 5
                        });

                    item.IsSummary = true;
                    item.Declares = new BindableCollection<OnlineLogisticsDeclareInfo>(ds);

                    item.NotifyOfPropertyChange(() => item.NeedPickup);
                    item.NotifyOfPropertyChange(() => item.Account);
                    item.NotifyOfPropertyChange(() => item.Status);
                    item.NotifyOfPropertyChange(() => item.OrderNote);
                    item.NotifyOfPropertyChange(() => item.Receiver);
                    item.NotifyOfPropertyChange(() => item.Sender);
                    item.NotifyOfPropertyChange(() => item.Pickup);
                    item.NotifyOfPropertyChange(() => item.LocalTrackNO);
                    item.NotifyOfPropertyChange(() => item.LogisticType);
                    item.NotifyOfPropertyChange(() => item.IsSummary);

                    item.NotifyOfPropertyChange(() => item.Declares);

                    this.LoadServices(item);
                    this.LoadLogisticsInfo(item);
                }
            }
        }

        public async Task LoadLogisticsInfo(ApplyItem item) {
            await Task.Factory.StartNew(async () => {
                var acc = AccountHelper.GetAccount(item.Account);
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new LogisticsGetOnlineLogisticsInfo() {
                    OrderID = item.OrderNO
                };
                //排除取消的
                var datas = await api.Execute(method);
                item.ExistsLogisticInfos = datas.Where(i => i.Status != OnlineLogisticStatus.Closed).ToList();
                item.NotifyOfPropertyChange(() => item.ExistsLogisticInfos);
            });
        }

        private async Task LoadServices(ApplyItem item) {
            var acc = AccountHelper.GetAccount(item.Account);
            var api = new APIClient(acc.User, acc.Pwd);
            var method = new LogisticsServiceByOrderID() {
                OrderID = item.OrderNO
            };
            item.Services = await api.Execute(method);
            item.NotifyOfPropertyChange(() => item.Services);
        }

        private async Task SetLocalLogisticCompany(ApplyItem item) {
            if (this.LocalLogisticsCompany == null) {
                var acc = AccountHelper.GetAccount(item.Account);
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new LogisticsGetLocalCompanies();
                this.LocalLogisticsCompany = await api.Execute(method);

                item.LogisticsCompanies = this.LocalLogisticsCompany;
                item.NotifyOfPropertyChange(() => item.LogisticsCompanies);
            } else {
                item.LogisticsCompanies = this.LocalLogisticsCompany;
                item.NotifyOfPropertyChange(() => item.LogisticsCompanies);
            }
        }

        private List<string> SelectedOrders = new List<string>();

        public void Remove() {
            if (this.Datas != null) {
                var willRemoved = this.Datas.Where(d => d.Status != Xmj.Entity.OrderStatus.WAIT_SELLER_SEND_GOODS && d.Status != Xmj.Entity.OrderStatus.SELLER_PART_SEND_GOODS).ToList();
                this.Datas.RemoveRange(willRemoved);
                this.NotifyOfPropertyChange(() => this.Datas);
            }
        }

        public void RemoveRepeated() {
            if (this.Datas != null) {
                var willRemoved = this.Datas.GroupBy(i => i.OrderNO)
                    .SelectMany(g => g.Skip(1)).ToList();

                this.Datas.RemoveRange(willRemoved);
                this.NotifyOfPropertyChange(() => this.Datas);
            }
        }

        public async Task Apply() {
            this.RemoveRepeated();

            if (this.Datas != null) {
                var willApply = this.Datas.Where(d =>
                    !d.Applied
                    &&
                    (d.Status == Xmj.Entity.OrderStatus.SELLER_PART_SEND_GOODS ||
                    d.Status == Xmj.Entity.OrderStatus.WAIT_SELLER_SEND_GOODS))
                    .ToList();

                this.IsShowBusy = true;
                this.BusyText = "正在操作...";
                this.NotifyOfPropertyChange(() => this.IsShowBusy);
                this.NotifyOfPropertyChange(() => this.BusyText);
                DispatcherHelper.DoEvents();

                //Task.Factory.StartNew(() => {
                willApply.ForEach(async i => {
                    var acc = AccountHelper.GetAccount(i.Account);
                    var method = new LogisticsCreateOnlineLogisticsNO() {
                        Declares = i.IsSummary ? new List<OnlineLogisticsDeclareInfo>() { i.Declares[0] } : i.Declares.ToList(),
                        LocalLogisticCompanyID = i.LocalLogisticCompany,
                        //LocalLogisticCompanyName,
                        LocalTrackingNO = i.LocalTrackNO,
                        OrderFrom = "ESCROW",
                        OrderID = i.OrderNO,
                        Pickup = i.NeedPickup ? i.Pickup : null,
                        Receiver = i.Receiver,
                        Remark = "WHAT IS REMARK",
                        Service = i.Service.ServiceID,
                        Sender = i.Sender
                    };
                    var api = new APIClient(acc.User, acc.Pwd);
                    var result = await api.Execute(method);
                    i.Created = result.Success;
                    i.Info = i.Created ? "申请成功" : result.ErrorInfo;

                    i.NotifyOfPropertyChange(() => i.Created);
                    i.NotifyOfPropertyChange(() => i.Info);
                });
                //}).ContinueWith((O) => {
                this.IsShowBusy = false;
                this.NotifyOfPropertyChange(() => this.IsShowBusy);
                DispatcherHelper.DoEvents();
                //});
            }
        }

        public async void Refresh() {
            if (this.Datas == null)
                return;

            //如果状态还是 init 的, 是没有 TrackNO 的, 还要判断状态,拉J8倒.
            //var willRefresh = this.Datas.Where(d => d.Created);//&& (d.ExistsLogisticInfos == null || d.ExistsLogisticInfos.Count == 0));
            //foreach (var item in willRefresh) {


            foreach (var item in this.Datas) {
                await this.LoadLogisticsInfo(item);
            }
        }

        public void Download() {
            if (this.Datas == null)
                return;

            using (var dlg = new FolderBrowserDialog()) {
                if (dlg.ShowDialog() == DialogResult.OK) {
                    var willDownload = this.Datas.Where(i => i.ExistsLogisticInfos != null && i.ExistsLogisticInfos.Count > 0);
                    willDownload.GroupBy(i => i.Account)
                        .ToList()
                        .ForEach(i => this.Download(i.Key, i.SelectMany(ii => ii.ExistsLogisticInfos), dlg.SelectedPath));
                }
            }
        }

        public async Task Download(string account, IEnumerable<OnlineLogisticsInfo> infos, string path) {
            var fileName = string.Format("{0}_{1}.pdf", DateTime.Now.ToString("yyyyMMddHHmm"), Regex.Replace(account.Split('@')[0], @"[^\w]", "_", RegexOptions.IgnoreCase));
            fileName = Path.Combine(path, fileName);
            //Task.Factory.StartNew(() => {
            using (var fs = new FileStream(fileName, FileMode.Create)) {
                var acc = AccountHelper.GetAccount(account);
                var method = new LogisticsOnlineLogisticsPrintInfo() {
                    LogisticsNOs = infos.Select(i => i.TrackNO).ToList()
                };
                var api = new APIClient(acc.User, acc.Pwd);
                var bytes = await api.Execute(method);
                fs.Write(bytes, 0, bytes.Length);
            }
            Process.Start(fileName);
            //})
            //.ContinueWith((t) => {
            System.Windows.MessageBox.Show(string.Format("账户 {0} , 下载失败", account));
            //}, TaskContinuationOptions.OnlyOnFaulted);
        }

        public void SendShipment() {
            var items = this.Datas.Select(d => new ShipmentItem() {
                Account = d.Account,
                TrackNO = d.ExistsLogisticInfos != null ? string.Join(" , ", d.ExistsLogisticInfos.Select(e => e.TrackNO)) : "",
                OrderNO = d.OrderNO
            });
            var bs = GlobalData.GetInstance<IBatchShipment>();
            bs.SendShipment(items);
            bs.Show();
        }


        public void SetSelectedOrder(SelectionChangedEventArgs e) {
            var adds = e.AddedItems.OfType<ApplyItem>().Cast<ApplyItem>().Select(i => i.OrderNO);
            var rms = e.RemovedItems.OfType<ApplyItem>().Cast<ApplyItem>().Select(i => i.OrderNO);
            this.SelectedOrders = this.SelectedOrders.Except(rms).Distinct().ToList();
            this.SelectedOrders.AddRange(adds);
        }

        public void SetSelectedOrder2(SelectedCellsChangedEventArgs e) {
            var adds = e.AddedCells.Where(i => i.Item.GetType().Equals(typeof(ApplyItem))).Select(i => (i.Item as ApplyItem).OrderNO);
            var rms = e.RemovedCells.Where(i => i.Item.GetType().Equals(typeof(ApplyItem))).Select(i => (i.Item as ApplyItem).OrderNO);
            this.SelectedOrders = this.SelectedOrders.Except(rms).Distinct().ToList();
            this.SelectedOrders.AddRange(adds);
        }

        public void ViewOrder() {
            if (this.SelectedOrders.Count > 0) {
                var searcher = GlobalData.GetInstance<IOrderSearcher>();
                var cond = new OrderSearchCondition() {
                    SpecifyOrders = this.SelectedOrders
                };
                searcher.Show();
                searcher.Search(cond);
            }
        }
    }
}
