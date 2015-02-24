using AsNum.Common.Security;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;

namespace AsNum.Xmj.Report.ViewModels {
    public class AddressExportViewModel : VMScreenBase {
        public override string Title {
            get {
                return "发货地址导出";
            }
        }

        public List<OrderStatus> Status {
            get;
            set;
        }

        public List<Owner> Owners {
            get;
            set;
        }

        public string SelectedAccounts {
            get;
            set;
        }

        public string Excepts {
            get;
            set;
        }

        public string Includes {
            get;
            set;
        }

        public int OffTimeHour {
            get;
            set;
        }

        public bool WaitSend {
            get;
            set;
        }

        public bool RiskControl {
            get;
            set;
        }

        public bool PartSend {
            get;
            set;
        }

        public List<Order> Orders {
            get;
            set;
        }

        public bool MultiSheet {
            get;
            set;
        }

        public bool SortByNote {
            get;
            set;
        }

        private List<string> SameAddressFlags = null;

        public IAccount AccountBiz { get; set; }
        public IOrder OrderBiz { get; set; }

        public AddressExportViewModel() {

            this.AccountBiz = GlobalData.MefContainer.GetExportedValue<IAccount>();
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();

            this.WaitSend = true;
            this.Owners = this.AccountBiz.AllAccounts().ToList();
            this.SelectedAccounts = string.Join(",", this.Owners.Select(a => a.Account));
        }

        public void View() {
            this.SelectedOrders = new List<string>();
            this.Orders = this.Search();

            this.SameAddressFlags = this.Orders.Select(o => o.Receiver).GroupBy(r => new {
                r.Name,
                r.FullAddress
            }).Where(g => g.Count() > 1)
            .Select(g => string.Format("{0}|{1}", g.Key.Name, g.Key.FullAddress).ToMD5())
            .ToList();

            this.NotifyOfPropertyChange("Orders");
        }

        public void Export() {
            this.View();
            var dialog = new SaveFileDialog();
            dialog.Filter = "xls|*.xls";
            dialog.FileName = string.Format("发货地址_{0}", DateTime.Now.ToString("yyyyMMdd"));
            if (dialog.ShowDialog() == true) {
                var file = dialog.FileName;
                var exporter = new AddressReporter();
                if (this.MultiSheet) {
                    //var datas = this.Orders.Select(o => o.Receiver).GroupBy(r => r.OrderFor.Account).ToDictionary(r => r.Key, r => r.ToList());
                    var datas = this.Orders.GroupBy(r => r.Account).ToDictionary(r => r.Key, r => r.ToList());
                    exporter.ExportDeliveryInfo(datas, file);
                } else {
                    exporter.ExportDeliveryInfo(this.Orders, file);
                }
            }
        }

        private List<Order> Search() {

            List<string> includes = null;
            List<string> excepts = null;

            IEnumerable<Order> results = null;

            if (this.Includes != null)
                includes = Regex.Split(this.Includes, "\r\n").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            if (this.Excepts != null)
                excepts = Regex.Split(this.Excepts, "\r\n").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            var accs = this.SelectedAccounts.Split(',').ToList();

            var cond = new OrderSearchCondition();
            cond.Pager.AllowPage = false;

            List<OrderStatus> ss = new List<OrderStatus>();
            if (this.WaitSend)
                ss.Add(OrderStatus.WAIT_SELLER_SEND_GOODS);
            if (this.RiskControl)
                ss.Add(OrderStatus.RISK_CONTROL);

            if (this.PartSend) {
                ss.Add(OrderStatus.SELLER_PART_SEND_GOODS);
            }

            if (ss.Count > 0)
                cond.IncludeStatus = ss;
            else
                cond.Status = OrderStatus.UNKNOW;

            if (excepts != null && excepts.Count > 0)
                cond.ExcludeOrderNOs = excepts;

            if (accs.Count > 0) {
                cond.SpecifyAccounts = accs;
            }

            results = this.OrderBiz.Search(cond).ToList();//.ToList();

            if (includes != null && includes.Count > 0) {
                var cond2 = new OrderSearchCondition() {
                    SpecifyOrders = includes
                };
                cond2.Pager.AllowPage = false;
                var result2 = this.OrderBiz.Search(cond2);

                results = results.Concat(result2);
            }

            if (this.OffTimeHour > 0) {
                var cond2 = new OrderSearchCondition() {
                    Status = OrderStatus.RISK_CONTROL,
                    OffTime = DateTime.Now.AddHours(this.OffTimeHour)
                };
                cond2.Pager.AllowPage = false;
                var result2 = this.OrderBiz.Search(cond2);

                //var ex3 = new QueryEx<Order>();
                //ex3.Includes.Add(o => o.OrgReceiver.Country);
                //ex3.Includes.Add(o => o.AdjReceiver.Country);
                //ex3.Includes.Add(o => o.Buyer);
                //ex3.Includes.Add(o => o.Note);
                //var t = DateTime.Now.AddHours(this.OffTimeHour);
                //ex3.Where = o => o.Status == OrderStatus.RISK_CONTROL && o.OffTime <= t;
                results = results.Concat(result2);
            }

            results = results.Distinct();

            //Distinct 要放到 OrderBy之前,不然 OrderBy 会被取消
            if (this.SortByNote)
                results = results.OrderBy(o => o.Note.Note);

            return results.ToList();
            //}
        }

        public Color GetSameAddressItemsColorFlag(Order o) {
            var s = string.Format("{0}|{1}", o.Receiver.Name, o.Receiver.FullAddress).ToMD5();
            if (this.SameAddressFlags.Contains(s)) {
                return ParseMD5ToColorStr(s);
            }
            return Colors.White;
        }

        private Color ParseMD5ToColorStr(string md5str) {
            var mas = Regex.Matches(md5str.PadRight(30, 'f'), @"\w{10}");
            //byte[] parts = new byte[3];
            var parts = mas.Cast<Match>().Select(m => (byte)(System.Convert.ToInt64(m.Groups[0].Value, 16) % 255)).ToArray();
            return Color.FromRgb(parts[0], parts[1], parts[2]);
        }

        public void SetSameAddressCellColor(DataGridRowEventArgs o) {
            o.Row.Background = new System.Windows.Media.SolidColorBrush(this.GetSameAddressItemsColorFlag(o.Row.Item as Order));
        }


        private List<string> SelectedOrders = new List<string>();

        public void SetSelectedOrder(SelectionChangedEventArgs e) {
            var adds = e.AddedItems.Cast<Order>().Select(i => i.OrderNO);
            var rms = e.RemovedItems.Cast<Order>().Select(i => i.OrderNO);
            this.SelectedOrders = this.SelectedOrders.Except(rms).Distinct().ToList();
            this.SelectedOrders.AddRange(adds);
        }

        public void SetSelectedOrder2(SelectedCellsChangedEventArgs e) {
            var adds = e.AddedCells.Select(i => (i.Item as Order).OrderNO);
            var rms = e.RemovedCells.Select(i => (i.Item as Order).OrderNO);
            this.SelectedOrders = this.SelectedOrders.Except(rms).Distinct().ToList();
            this.SelectedOrders.AddRange(adds);
        }
        public void EditOrders() {
            if (this.SelectedOrders.Count > 0) {
                var searcher = GlobalData.GetInstance<IOrderSearcher>();
                var cond = new OrderSearchCondition() {
                    SpecifyOrders = this.SelectedOrders
                };
                searcher.Show();
                searcher.Search(cond);
            }
        }

        public void ExceptOrders() {
            this.Excepts = string.Format("{0}\r\n{1}", this.Excepts, string.Join("\r\n", this.SelectedOrders));
            this.NotifyOfPropertyChange(() => this.Excepts);
        }
    }
}
