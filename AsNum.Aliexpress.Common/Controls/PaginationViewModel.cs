using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Xmj.Common.Controls {

    public class PageItem {

        public int Page {
            get;
            set;
        }

        public string Label {
            get;
            set;
        }

        public bool IsCurrPage {
            get;
            set;
        }
    }
    public class PaginationEventArgs : EventArgs {

        private int currPage = 1;
        public int CurrPage {
            get {
                return this.currPage;
            }
            set {
                this.currPage = value < 1 ? 1 : value;
            }
        }

        private int pageSize = 100;
        public int PageSize {
            get {
                return this.pageSize;
            }
            set {
                this.pageSize = value < 0 ? 0 : value;
            }
        }
    }

    public class PaginationViewModel : PropertyChangedBase {

        public event EventHandler<PaginationEventArgs> Paging;

        private int total = 0;
        public int Total {
            get {
                return this.total;
            }
            set {
                this.total = value < 0 ? 0 : value;
                this.NotifyOfPropertyChange(() => this.Buttons);
                this.NotifyOfPropertyChange(() => this.TotalPage);
                this.NotifyOfPropertyChange(() => this.Total);
            }
        }

        private int currPage = 1;
        public int CurrPage {
            get {
                return this.currPage;
            }
            set {
                this.currPage = value < 1 ? 1 : value;
                this.NotifyOfPropertyChange(() => this.Buttons);
            }
        }

        private int pageSize = 10;
        public int PageSize {
            get {
                return this.pageSize;
            }
            set {
                this.pageSize = value < 1 ? 1 : value;
                this.NotifyOfPropertyChange(() => this.Buttons);
                this.NotifyOfPropertyChange(() => this.TotalPage);
            }
        }

        private int labelCount = 10;
        public int LableCount {
            get {
                return this.labelCount;
            }
            set {
                this.labelCount = value < 1 ? 1 : value;
                this.NotifyOfPropertyChange(() => this.Buttons);
                this.NotifyOfPropertyChange(() => this.TotalPage);
            }
        }


        public int TotalPage {
            get {
                return (int)Math.Ceiling(((decimal)this.Total / this.PageSize));
            }
        }

        private int[] pageSizes = new int[] { 10, 50, 100, 200, 500 };

        public int[] PageSizes {
            get {
                return this.pageSizes;
            }
            set {
                this.pageSizes = value.Where(i => i > 0).ToArray();
            }
        }

        public List<PageItem> Buttons {
            get {
                int begin = this.CurrPage - this.LableCount / 2;
                if (begin < 1)
                    begin = 1;
                int end = begin + this.labelCount;
                if (end > this.TotalPage)
                    end = this.TotalPage;
                if (end - begin > 0) {
                    var items = Enumerable.Range(begin, end - begin + 1)
                        .Select(i => new PageItem() {
                            Page = i,
                            Label = i.ToString(),
                            IsCurrPage = i == this.CurrPage
                        }).ToList();

                    if (this.CurrPage > 1) {
                        items.Insert(0, new PageItem() {
                            Label = "<",
                            Page = this.CurrPage - 1
                        });
                        items.Insert(0, new PageItem() {
                            Label = "|<",
                            Page = 1
                        });
                    }

                    if (this.CurrPage < this.TotalPage) {

                        items.Add(new PageItem() {
                            Label = ">",
                            Page = this.CurrPage + 1
                        });

                        items.Add(new PageItem() {
                            Label = ">|",
                            Page = this.TotalPage
                        });
                    }
                    return items;
                } else
                    return null;
            }
        }

        public void PageChange(PageItem page) {
            this.CurrPage = page.Page;
            if (this.Paging != null)
                this.Paging.Invoke(this, new PaginationEventArgs() {
                    CurrPage = page.Page,
                    PageSize = this.PageSize
                });
        }

        public void Reset() {
            this.CurrPage = 1;
            this.Total = 0;
        }
    }
}
