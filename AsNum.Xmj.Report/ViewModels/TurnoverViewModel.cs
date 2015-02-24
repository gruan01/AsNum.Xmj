using AsNum.Common.Extends;
using AsNum.Xmj.BizEntity.Models;
using AsNum.Xmj.Common;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.Report.ViewModels {
    public class TurnoverViewModel : VMScreenBase {
        public override string Title {
            get { return "销售额报表"; }
        }

        private List<TurnoverByAccount> Datas { get; set; }

        public List<Tuple<string, decimal>> DatasByMonth { get; set; }

        private Tuple<string, decimal> curr;
        public Tuple<string, decimal> Curr {
            get {
                return this.curr;
            }
            set {
                this.curr = value;
                if (curr != null) {
                    this.SelectedMonthData = this.Datas.Where(t => t.YearMonth.Equals(value.Item1)).ToList();
                    this.CurrMonth = value.Item1.ToDateTime("yyyyMM", DateTime.Now).ToString("yyyy年MM月");
                    this.NotifyOfPropertyChange(() => this.SelectedMonthData);
                    this.NotifyOfPropertyChange(() => this.CurrMonth);
                }
            }
        }

        public string CurrMonth {
            get;
            set;
        }

        public List<TurnoverByAccount> SelectedMonthData { get; set; }

        public List<TurnoverByAccount> In30DaysData { get; set; }

        public DateTime EndDate { get; set; }

        private int monthCount = 12;
        public int MonthCount {
            get {
                return this.monthCount;
            }
            set {
                this.monthCount = value;
                this.LoadAsync();
            }
        }

        public List<int> MCounts { get; set; }

        public IAccount AccountBiz { get; set; }

        public TurnoverViewModel() {
            this.AccountBiz = GlobalData.MefContainer.GetExportedValue<IAccount>();
            this.MCounts = new List<int>() { 6, 12, 18, 24 };
            this.ReLoad();
        }

        public void ReLoad() {
            this.EndDate = DateTime.Now;
            this.monthCount = 12;
            this.NotifyOfPropertyChange(() => this.MonthCount);

            Task.Factory.StartNew(() => {
                this.LoadAsync();
                this.LoadIn30DaysAsync();
            });
        }

        public void LoadAsync() {
            this.Datas = this.AccountBiz.TurnoverByAccount(this.EndDate, this.MonthCount - 1)
                .ToList();

            this.DatasByMonth = this.Datas
                .GroupBy(r => r.YearMonth)
                .Select(g => new Tuple<string, decimal>(g.Key, g.Sum(gg => gg.TotalAmount)))
                .OrderBy(g => g.Item1)
                .ToList();
            this.Curr = this.DatasByMonth.Last();
            this.NotifyOfPropertyChange(() => this.Curr);
            this.NotifyOfPropertyChange(() => this.DatasByMonth);
        }

        public void LoadIn30DaysAsync() {
            this.In30DaysData = this.AccountBiz.TurnoverInNDays(30).ToList();
            this.NotifyOfPropertyChange(() => this.In30DaysData);
        }

        public void Left() {
            this.EndDate = this.EndDate.AddMonths(-1);
            this.LoadAsync();
        }

        public void Right() {
            this.EndDate = this.EndDate.AddMonths(1);
            this.LoadAsync();
        }
    }
}
