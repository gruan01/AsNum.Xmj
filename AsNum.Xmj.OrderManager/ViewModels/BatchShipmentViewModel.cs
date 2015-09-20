using AsNum.Common.Extends;
using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using APIE = AsNum.Xmj.API.Entity;
using System;
using System.Globalization;

namespace AsNum.Xmj.OrderManager.ViewModels {

    [Export(typeof(IBatchShipment)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class BatchShipmentViewModel : VMScreenBase, IBatchShipment {
        public override string Title {
            get {
                return "批量发货";
            }
        }


        private ObservableCollection<ShipmentItem> items;
        public ObservableCollection<ShipmentItem> Items {
            get {
                return this.items;
            }
            set {
                this.items = value;
                this.NotifyOfPropertyChange(() => this.CanShipment);
            }
        }

        public ShipmentItem CurrItem {
            get;
            set;
        }

        //public static List<LogisticServices> LogisticsTypes {
        //    get;
        //    private set;
        //}

        public static List<string> SendTypes {
            get;
            private set;
        }

        public bool CanShipment {
            get;
            set;
        }

        public IOrder OrderBiz { get; set; }
        public ILogisticFee LogisticFeeBiz { get; set; }

        public static ListCollectionView LSV {
            get; set;
        }

        static BatchShipmentViewModel() {
            SendTypes = EnumHelper.GetDescriptions<ShipmentSendTypes>().Values.ToList();
            //LogisticsTypes = GlobalData.GetInstance<ILogisticsService>().GetAll().ToList();// EnumHelper.GetDescriptions<AsNum.Xmj.API.Entity.LogisticsTypes>().Values.ToList();
            LSV = new ListCollectionView(GlobalData.LogisticService.ToList());
            LSV.GroupDescriptions.Add(new PropertyGroupDescription("IsUsual", new UsualGroupConverter()));
        }
        public BatchShipmentViewModel() {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
            this.LogisticFeeBiz = GlobalData.MefContainer.GetExportedValue<ILogisticFee>();

            this.Items = new BindableCollection<ShipmentItem>();
            this.Items.CollectionChanged += Items_CollectionChanged;
        }

        void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if (e.NewItems != null)
                foreach (ShipmentItem item in e.NewItems)
                    item.PropertyChanged += ShipmentItemChanged;

            if (e.OldItems != null)
                foreach (ShipmentItem item in e.OldItems)
                    item.PropertyChanged -= ShipmentItemChanged;

            this.CheckCanShipment();
        }

        private void ShipmentItemChanged(object sender, PropertyChangedEventArgs e) {
            var p = e.PropertyName;
            if (p.Equals("Account") || p.Equals("Country") || p.Equals("Note") || p.Equals("OrderStatus") || p.Equals("Status"))
                return;

            if (p.Equals("OrderNO", System.StringComparison.OrdinalIgnoreCase)) {
                var item = (ShipmentItem)sender;
                if (item != null) {
                    LoadData(item);
                }
            }

            this.CheckCanShipment();
        }


        private void CheckCanShipment() {
            this.CanShipment = !this.Items.Any(i => string.IsNullOrWhiteSpace(i.OrderNO)
                                || string.IsNullOrWhiteSpace(i.TrackNO));

            this.NotifyOfPropertyChange(() => this.CanShipment);
        }

        public void LoadData(ShipmentItem item) {
            if (!string.IsNullOrWhiteSpace(item.OrderNO)) {
                var order = this.OrderBiz.GetOrder(item.OrderNO);
                if (order != null) {
                    item.Account = order.Account;
                    item.Country = order.Receiver.Country.ZhName;
                    item.Note = order.Note != null ? order.Note.Note : "";
                    item.OrderStatus = order.Status;
                    item.Status = ShipmentStatus.Ready;
                } else {
                    item.Account = "";
                    item.Country = "";
                    item.Note = "";
                    item.OrderStatus = Entity.OrderStatus.UNKNOW;
                    item.Status = ShipmentStatus.Warning;
                }

            } else {
                item.Account = "";
                item.Country = "";
                item.Note = "";
                item.OrderStatus = Entity.OrderStatus.UNKNOW;
                item.Status = ShipmentStatus.Warning;
            }

            item.NotifyOfPropertyChange(() => item.Account);
            item.NotifyOfPropertyChange(() => item.Country);
            item.NotifyOfPropertyChange(() => item.Note);
            item.NotifyOfPropertyChange(() => item.OrderStatus);
            item.NotifyOfPropertyChange(() => item.Status);
        }

        public bool IsBusy {
            get;
            set;
        }

        public void Shipment() {
            //var accs = AccountHelper.LoadAccounts();
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);

            Task.Factory.StartNew(() => {
                foreach (var item in this.Items) {
                    this.CurrItem = item;
                    this.NotifyOfPropertyChange(() => this.CurrItem);
                    DispatcherHelper.DoEvents();

                    //var acc = accs.FirstOrDefault(a => a.User.Equals(item.Account, System.StringComparison.OrdinalIgnoreCase));
                    var acc = AccountHelper.GetAccount(item.Account);
                    if (acc == null)
                        continue;

                    Shipment(item, acc);
                }
            }).ContinueWith((o) => {

                this.WriteToDb(this.Items);

                this.IsBusy = false;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            }, TaskContinuationOptions.None);
        }

        private void WriteToDb(IEnumerable<ShipmentItem> items) {
            this.LogisticFeeBiz.Save(items.Select(i => new LogisticFee() {
                TrackNO = i.TrackNO,
                Fee = i.Fee ?? 0,
                Weight = i.Weight ?? 0
            }));
        }

        /// <summary>
        /// 声明发货
        /// </summary>
        /// <param name="item"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        private bool Shipment(ShipmentItem item, Account acc) {
            var api = new APIClient(acc.User, acc.Pwd);
            var method = new OrderShipment() {
                LogisticsType = item.LogisticsType.Code,
                OrderNO = item.OrderNO.Trim(),//如果有空格， StackTrace会为 []]
                SendType = item.SendType,
                TrackingNO = item.TrackNO.Trim()
            };
            var result = api.Execute(method);
            //var result = new NormalResult() {
            //    Success = false,
            //    ErrorInfo = "TEST"
            //};
            item.Info = result.ErrorInfo;
            item.Status = result.Success ? ShipmentStatus.Success : ShipmentStatus.Error;

            item.NotifyOfPropertyChange(() => item.Info);
            item.NotifyOfPropertyChange(() => item.Status);

            //Thread.Sleep(1000);
            return result.Success;
        }

        public void SendShipment(IEnumerable<BizEntity.Models.ShipmentItem> items) {
            foreach (var i in items) {
                var item = new ShipmentItem() {
                    Account = i.Account,
                    OrderNO = i.OrderNO,
                    TrackNO = i.TrackNO
                };
                this.Items.Add(item);
                item.NotifyOfPropertyChange("OrderNO");
            }
        }


        public void Show() {
            GlobalData.GetInstance<ISheel>().Show(this);
        }
    }

    public enum ShipmentStatus {
        [Description("就绪")]
        Ready,
        [Description("警告")]
        Warning,
        [Description("错误")]
        Error,
        [Description("成功")]
        Success
    }
    public class ShipmentItem : PropertyChangedBase {

        private string orderNo;
        public string OrderNO {
            get {
                return this.orderNo;
            }
            set {
                this.orderNo = value;
                this.NotifyOfPropertyChange(() => this.orderNo);
            }
        }

        private string trackNo;
        public string TrackNO {
            get {
                return this.trackNo;
            }
            set {
                this.trackNo = value;
                this.NotifyOfPropertyChange(() => this.TrackNO);
            }
        }

        private LogisticServices logisticsType = null;
        public LogisticServices LogisticsType {
            get {
                return this.logisticsType;
            }
            set {
                this.logisticsType = value;
                this.NotifyOfPropertyChange(() => this.LogisticsType);
            }
        }

        private ShipmentSendTypes sendType;
        public ShipmentSendTypes SendType {
            get {
                return this.sendType;
            }
            set {
                this.sendType = value;
                this.NotifyOfPropertyChange(() => this.SendType);
            }
        }


        public decimal? Weight { get; set; }
        public decimal? Fee { get; set; }

        public string Account {
            get;
            set;
        }

        public string Country {
            get;
            set;
        }

        public string Note {
            get;
            set;
        }

        public Entity.OrderStatus OrderStatus {
            get;
            set;
        }

        public ShipmentStatus Status {
            get;
            set;
        }

        public string Info {
            get;
            set;
        }
    }

    public class UsualGroupConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var isUsual = (bool)value;
            return isUsual ? "常用" : "不常用";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
