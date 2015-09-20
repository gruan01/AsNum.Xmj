using AsNum.Common;
using AsNum.WPF.Controls;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Controls;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace AsNum.Xmj.OrderManager.ViewModels {
    [Export(typeof(IOrderSearcher)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class OrderQueryViewModel : VMScreenBase, IOrderSearcher {

        private string title = "订单查询";
        public override string Title {
            get {
                return this.title;
            }
        }

        public OrderSearchCondition Cond { get; set; }

        public List<string> Accounts {
            get;
            set;
        }

        //public static List<LogisticServices> DeliveryTypes {
        //    get;
        //    set;
        //}

        public static List<AsNum.Xmj.BizEntity.Conditions.OrderSearchCondition.TimesTypes> TimesTypes { get; set; }

        public List<Country> Countries {
            get;
            set;
        }


        public BindableCollection<Order> QueryResult {
            get;
            set;
        }

        public PaginationViewModel PaginationVM {
            get;
            private set;
        }

        public bool HighlightMoreThan1 {
            get;
            set;
        }

        public bool HighlightPurchased {
            get;
            set;
        }

        public BindableCollection<IOrderDealSubView> SubViews {
            get;
            set;
        }

        private int selectedSubViewIndex;
        public int SelectedSubViewIndex {
            get {
                return this.selectedSubViewIndex;
            }
            set {
                this.selectedSubViewIndex = value;
                if (value > -1)
                    this.PrevSelectedSubViewIndex = value;
            }
        }

        private int PrevSelectedSubViewIndex = 0;

        public bool IsBusy {
            get;
            set;
        }

        public IOrder OrderBiz { get; set; }
        public IAccount AccountBiz { get; set; }
        public ICountry CountryBiz { get; set; }

        private QueryEx<Order> QEX = new QueryEx<Order>();

        static OrderQueryViewModel() {
            //DeliveryTypes = GlobalData.GetInstance<ILogisticsService>().GetAll().ToList();// Enum.GetValues(typeof(LogisticsTypes)).Cast<LogisticsTypes>().ToList();
            TimesTypes = Enum.GetValues(typeof(AsNum.Xmj.BizEntity.Conditions.OrderSearchCondition.TimesTypes)).Cast<AsNum.Xmj.BizEntity.Conditions.OrderSearchCondition.TimesTypes>().ToList();
        }
        public OrderQueryViewModel() {

            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
            this.AccountBiz = GlobalData.MefContainer.GetExportedValue<IAccount>();
            this.CountryBiz = GlobalData.MefContainer.GetExportedValue<ICountry>();

            this.Accounts = this.AccountBiz.AllAccounts().Select(a => a.Account).ToList();
            this.Accounts.Insert(0, "");
            this.Countries = this.CountryBiz.AllCountry().ToList();

            this.PaginationVM = new PaginationViewModel() {
                PageSize = 200
            };

            this.Cond = new OrderSearchCondition();

            this.PaginationVM.Paging += PaginationVM_Paging;
        }

        void PaginationVM_Paging(object sender, PaginationEventArgs e) {
            this.Search(this.Cond);
        }

        public AutoCompleteFilterPredicate<object> CountryFilter {
            get {
                return new AutoCompleteFilterPredicate<object>(
                    (str, item) => {
                        return MatchCountry(str, item);
                    });

            }
        }

        private bool MatchCountry(string search, object value) {
            var country = value as Country;
            return country != null
                &&
                (country.CountryCode.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1
                || country.ZhName.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1
                || country.EnName.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1);
        }

        public void SearchAll() {
            this.Cond.IncludeStatus = null;
            this.PaginationVM.Reset();
            this.Search(this.Cond);
        }

        public void SearchWS() {
            this.Cond.IncludeStatus = new List<OrderStatus>() {
                OrderStatus.WAIT_SELLER_SEND_GOODS,
                OrderStatus.SELLER_PART_SEND_GOODS,
                OrderStatus.IN_CANCEL
            };
            this.PaginationVM.Reset();
            this.Search(this.Cond);
        }

        public void SearchWSR() {
            this.Cond.IncludeStatus = new List<OrderStatus>() {
                    OrderStatus.WAIT_SELLER_SEND_GOODS,
                    OrderStatus.SELLER_PART_SEND_GOODS,
                    OrderStatus.RISK_CONTROL,
                    OrderStatus.IN_CANCEL
                };
            this.PaginationVM.Reset();
            this.Search(this.Cond);
        }

        public void Search(OrderSearchCondition cond) {
            this.IsBusy = true;
            this.NotifyOfPropertyChange(() => this.IsBusy);
            DispatcherHelper.DoEvents();

            cond.Pager.Page = this.PaginationVM.CurrPage - 1;
            cond.Pager.PageSize = this.PaginationVM.PageSize;
            var results = this.OrderBiz.Search(cond);
            this.PaginationVM.Total = cond.Pager.Count;

            this.QueryResult = new BindableCollection<Order>(results);

            this.NotifyOfPropertyChange("QueryResult");
            if (this.QueryResult.Count > 0)
                this.CurrOrder = this.QueryResult.First();
            else
                this.CurrOrder = null;

            this.NotifyOfPropertyChange(() => this.CurrOrder);
            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.IsBusy);
        }

        private Order currOrder = null;
        public Order CurrOrder {
            get {
                return this.currOrder;
            }
            set {
                this.currOrder = value;

                if (value != null) {
                    if (this.SubViews == null) {
                        this.SubViews = new BindableCollection<IOrderDealSubView>(GlobalData.MefContainer.GetExports<IOrderDealSubView>().Select(i => i.Value));
                        this.SelectedSubViewIndex = this.PrevSelectedSubViewIndex;
                    }

                    foreach (var vm in this.SubViews) {
                        vm.Build(value);
                    }
                } else {
                    this.SubViews = null;
                }

                //this.DetailVM = null;
                //this.NotifyOfPropertyChange(() => this.DetailVM);
                //this.DetailVM = new OrderDetailViewModel(value.OrderNO);
                //this.NotifyOfPropertyChange(() => this.DetailVM);

                this.NotifyOfPropertyChange(() => this.SubViews);
                this.NotifyOfPropertyChange(() => this.SelectedSubViewIndex);
                this.NotifyOfPropertyChange(() => this.SubOrders);
            }
        }

        //void PurchaseVM_Saved(object sender, System.EventArgs e) {
        //    var orderNote = (OrderNote)sender;
        //    this.CurrOrder.Note = orderNote;
        //    this.NotifyOfPropertyChange("CurrOrder");
        //}

        public BindableCollection<OrderDetail> SubOrders {
            get {
                if (this.CurrOrder != null) {
                    return new BindableCollection<OrderDetail>(this.CurrOrder.Details);
                } else
                    return null;
            }
        }

        public void OpenUrl(object d) {

        }

        public void OpenProduct(OrderDetail d) {
            var url = string.Format("http://www.aliexpress.com/item/a/{0}.html", d.ProductID);
            System.Diagnostics.Process.Start(url);
        }

        public void OpenSnap(OrderDetail d) {
            System.Diagnostics.Process.Start(d.SnapURL);
        }

        public void EditSKUMap(OrderDetail d) {
            //var vm = new PropertyMapViewModel(d);
            //Global.MefContainer.GetExport<ISheel>().Value.Show(vm);
            //Global.MefContainer.GetExport<IWindowManager>().Value.ShowWindow(vm);
        }

        //public void ShowDetailVM(DataGridRowDetailsEventArgs eventArgs) {
        //    //try {

        //    if (eventArgs.DetailsElement == null)
        //        return;

        //    var ctrl = (ContentControl)eventArgs.DetailsElement.FindName("detailView");
        //    var view = new OrderDetailView();
        //    var o = eventArgs.Row.Item as Order;
        //    var model = new OrderDetailViewModel(o.OrderNO);
        //    //ctrl.Content = view;
        //    //Caliburn.Micro.View.SetModel(view, model);
        //    ViewModelBinder.Bind(model, view, null);
        //    ctrl.Content = view;
        //    //} catch (Exception ex) {
        //    //}
        //}


        public void Show(string title = "") {
            if (!string.IsNullOrEmpty(title)) {
                this.title = title;
                //this.NotifyOfPropertyChange("Title");
            }
            GlobalData.GetInstance<ISheel>().Show(this);
        }

        public int CurrIndex {
            get;
            set;
        }
        public void PreRecord() {
            this.CurrIndex--;
            if (this.CurrIndex < 0)
                this.CurrIndex = 0;
            this.NotifyOfPropertyChange(() => this.CurrIndex);
        }

        public void NextRecord() {
            this.CurrIndex++;
            if (this.QueryResult != null && this.QueryResult.Count > 0 && this.CurrIndex >= this.QueryResult.Count)
                this.CurrIndex = this.QueryResult.Count - 1;
            else if (this.QueryResult == null || this.QueryResult.Count == 0)
                this.CurrIndex = 0;

            this.NotifyOfPropertyChange(() => this.CurrIndex);
        }

        //private DataGridCellInfo currCell;
        //public DataGridCellInfo CurrCell {
        //    get {
        //        return currCell;
        //    }
        //    set {
        //        currCell = value;
        //        if (value.Column != null) {
        //            this.CurrOrder = value.Item as Order;
        //        }
        //    }
        //}

        public void LoadingRowDetails(DataGridRowDetailsEventArgs e) {
            try {
                var cnt = e.DetailsElement.FindName("cnt") as ContentControl;
                var vm = new OrderDetailViewModel((e.Row.DataContext as Order));
                View.SetModel(cnt, vm);
            } catch {
            }
        }

        public void UnLoadingRowDetails(object o) {

        }

        private SolidColorBrush White = new SolidColorBrush(Colors.White);
        private SolidColorBrush HighlightColor = new SolidColorBrush(Colors.Teal);
        private SolidColorBrush HightPurchasedColor = new SolidColorBrush(Colors.BlueViolet);
        public void SetBackground(DataGridRowEventArgs e) {
            var color = White;
            if (this.HighlightMoreThan1) {
                var data = (Order)e.Row.Item;
                if (data != null && (data.Details.Count > 1 || data.Details.Any(d => d.Qty > 1))) {
                    color = HighlightColor;
                }
            }
            if (this.HighlightPurchased) {
                var data = (Order)e.Row.Item;
                if (data != null && (data.PurchasseDetail != null && data.PurchasseDetail.Completed)) {
                    color = HightPurchasedColor;
                }
            }
            e.Row.Background = color;
        }
    }
}
