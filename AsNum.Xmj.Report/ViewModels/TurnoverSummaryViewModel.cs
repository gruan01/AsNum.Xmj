using AsNum.Common.Extends;
using AsNum.Xmj.BizEntity.Models;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.Report.ViewModels {

    [Export(typeof(IStartUpModel)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class TurnoverSummaryViewModel : VMScreenBase, IStartUpModel {
        public override string Title {
            get { return "成交额汇总"; }
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

        public IAccount AccountBiz { get; set; }

        public List<TurnoverByAccount> SelectedMonthData { get; set; }

        public TurnoverSummaryViewModel() {
            this.AccountBiz = GlobalData.MefContainer.GetExportedValue<IAccount>();
        }

        public void Load() {
            Task.Factory.StartNew(() => {
                this.LoadAsync();
            });
        }

        public void LoadAsync() {
            this.Datas = this.AccountBiz.TurnoverByAccount().ToList();

            this.DatasByMonth = this.Datas
                .GroupBy(r => r.YearMonth)
                .Select(g => new Tuple<string, decimal>(g.Key, g.Sum(gg => gg.TotalAmount)))
                .OrderBy(g => g.Item1)
                .ToList();

            this.Curr = this.DatasByMonth.Last();
            this.NotifyOfPropertyChange(() => this.Curr);
            this.NotifyOfPropertyChange(() => this.DatasByMonth);
        }

        public void ReLoad() {

        }
    }
}
