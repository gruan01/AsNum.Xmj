using AsNum.Common.Extends;
using AsNum.Xmj.AliSync;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.Common;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.ProductManager.ViewModels {
    public class ProductQueryViewModel : VMScreenBase {
        public override string Title {
            get {
                return "产品查询";
            }
        }

        public string Subject {
            get;
            set;
        }

        public BindableCollection<CheckableSuccinctProduct> Products {
            get;
            set;
        }


        public List<ProductStatus> Status {
            get;
            private set;
        }

        public ProductStatus SelectedStatus {
            get;
            set;
        }

        public List<string> Accounts {
            get;
            private set;
        }

        private string selectedAccount;
        public string SelectedAccount {
            get {
                return this.selectedAccount;
            }
            set {
                this.selectedAccount = value;
                this.NotifyOfPropertyChange(() => this.GroupsForSelectedAccount);
            }
        }

        private List<ProductGroup2> Groups;

        public BindableCollection<ProductGroup2> GroupsForSelectedAccount {
            get {
                if (string.IsNullOrWhiteSpace(this.SelectedAccount))
                    return new BindableCollection<ProductGroup2>();
                else {
                    var gs = this.Groups.Where(g => g.Account.Equals(this.SelectedAccount, StringComparison.OrdinalIgnoreCase)).ToList();
                    var subs = gs.Where(g => g.Children != null).SelectMany(g => g.Children).ToList();
                    gs.AddRange(subs);
                    gs.RemoveAll(g => !g.CanChoice);
                    return new BindableCollection<ProductGroup2>(gs);
                }
            }
        }

        public int? SelectedGroup {
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


        private bool? checkAllState;
        public bool? CheckAllState {
            get {
                return this.checkAllState;
            }
            set {
                this.checkAllState = value;
                if (value != null)
                    foreach (var p in this.Products) {
                        p.IsChecked = value.Value;
                    }
            }
        }

        public ProductQueryViewModel() {
            this.Status = Enum.GetValues(typeof(ProductStatus)).Cast<ProductStatus>().ToList();
            this.Accounts = AccountHelper.LoadAccounts().Select(a => a.User).ToList();
            this.Accounts.Insert(0, "");

            this.IsBusy = true;
            this.BusyText = "正在加载产品分组...";
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyText);

            Task.Factory.StartNew(() => {
                this.Groups = ProductSync.QueryGroups();

                this.IsBusy = false;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            });
        }

        private void Query(int? expiryDays) {
            this.IsBusy = true;
            this.BusyText = "正在查询，请稍候...";
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyText);

            Task.Factory.StartNew(() => {
                this.Products = new BindableCollection<CheckableSuccinctProduct>(
                    ProductSync.Query(this.Subject, this.SelectedStatus, this.SelectedAccount, this.SelectedGroup, expiryDays)
                    .Select(p => new CheckableSuccinctProduct(p)));

                this.NotifyOfPropertyChange(() => this.Products);

                this.IsBusy = false;
                this.NotifyOfPropertyChange(() => this.IsBusy);
            });
        }

        public void Query() {
            this.Query(null);
        }

        public void WillExpiry() {
            this.Query(3);
        }

        public void OfflineSelected() {
            this.IsBusy = true;
            this.BusyText = "正在下架选中的产品...";
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyText);
            var g = this.Products.Where(p => p.IsChecked).GroupBy(p => p.Account);
            Task.Factory.StartNew(() => {
                foreach (var gg in g) {
                    ProductSync.OfflineProducts(gg.Key, gg.Select(p => p.ProductID.ToString()).ToList());
                }

                this.Query();
            });
        }

        public void ExtendExpiryDate() {
            this.IsBusy = true;
            this.BusyText = "正在处理...";
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyText);
            var g = this.Products.Where(p => p.IsChecked);
            var total = g.Count();
            Task.Factory.StartNew(() => {
                int i = 1;
                foreach (var gg in g) {
                    this.BusyText = string.Format("正在处理...{0} / {1}", i++, total);
                    this.NotifyOfPropertyChange(() => this.BusyText);
                    ProductSync.ExtendExpiryDate(gg.Account, gg.ProductID.ToString());
                }

                this.WillExpiry();
            });
        }
    }

    public class CheckableSuccinctProduct : SuccinctProduct, INotifyPropertyChanged {

        public CheckableSuccinctProduct(SuccinctProduct p) {
            //DynamicCopy.CopyProperties(p, this);
            DynamicCopy.CopyTo(p, this);
        }

        private bool isChecked;
        public bool IsChecked {
            get {
                return this.isChecked;
            }
            set {
                this.isChecked = value;
                this.OnPropertyChanged("IsChecked");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
