using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class PaginationViewModel : PropertyChangedBase {

        private int total = 0;
        public int Total {
            get {
                return this.total;
            }
            set {
                this.total = value;
                this.NotifyOfPropertyChange(() => this.Buttons);
            }
        }

        private int currPage = 1;
        public int CurrPage {
            get {
                return this.currPage;
            }
            set {
                this.currPage = value;
                this.NotifyOfPropertyChange(() => this.Buttons);
            }
        }

        private int pageSize = 10;
        public int PageSize {
            get {
                return this.pageSize;
            }
            set {
                this.pageSize = value;
                this.NotifyOfPropertyChange(() => this.Buttons);
            }
        }

        private int labelCount = 10;
        public int LableCount {
            get {
                return this.labelCount;
            }
            set {
                this.labelCount = value;
                this.NotifyOfPropertyChange(() => this.Buttons);
            }
        }

        public List<int> Buttons {
            get {
                return Enumerable.Range(0, (int)Math.Ceiling((decimal)(this.Total / this.PageSize))).ToList();
            }
        }
    }
}
