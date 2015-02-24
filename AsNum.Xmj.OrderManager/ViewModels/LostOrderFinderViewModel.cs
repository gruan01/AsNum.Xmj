using AsNum.Xmj.Common;
using AsNum.Xmj.IBiz;
using System.Linq;
using System.Text.RegularExpressions;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class LostOrderFinderViewModel : VMScreenBase {
        public override string Title {
            get {
                return "缺失订单查询";
            }
        }

        public string OrderNOs {
            get;
            set;
        }

        public string Losted {
            get;
            set;
        }

        public string Duplicate {
            get;
            set;
        }

        public IOrder OrderBiz { get; set; }

        public LostOrderFinderViewModel() {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
        }

        public void Find() {
            var ons = Regex.Split(this.OrderNOs, "\r\n").Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            var result = this.OrderBiz.Search(new BizEntity.Conditions.OrderSearchCondition() {
                SpecifyOrders = ons
            }).Select(o => o.OrderNO).ToList();

            this.Losted = string.Join("\r\n", ons.Except(result));

            var d = ons.GroupBy(s => s).Where(g => g.Count() > 1);
            this.Duplicate = string.Join("\r\n", d.Select(dd => dd.First()));

            this.NotifyOfPropertyChange("Losted");
            this.NotifyOfPropertyChange("Duplicate");
        }
    }
}
