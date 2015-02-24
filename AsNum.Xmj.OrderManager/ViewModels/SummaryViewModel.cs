using AsNum.Common.Extends;
using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {

    public enum SummaryTitles {
        [Description("即将超时")]
        Timeout,
        [Description("等待发货")]
        WaitSendGoods,

        [Description("部分发货")]
        PartSendGoods,

        [Description("未到账")]
        Risk,
        [Description("申请取消")]
        InCancel,
        [Description("有纠纷")]
        HaveInsure,
        [Description("已填单未发货")]
        ShamShipping,
        [Description("待评价")]
        WaitEvaluate
    }

    public class Item : PropertyChangedBase {
        public SummaryTitles Title {
            get;
            set;
        }

        public int Count {
            get;
            set;
        }

        public bool Loaded {
            get;
            set;
        }
    }

    [Export(typeof(IStartUpModel)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class SummaryViewModel : VMScreenBase, IStartUpModel {
        public override string Title {
            get {
                return "订单状态汇总";
            }
        }


        public ObservableCollection<Item> Summaries {
            get;
            set;
        }

        private List<string> WaitEvaluateOrders = new List<string>();


        [Import]
        public IOrder OrderBiz { get; set; }

        public void Load() {
            this.Summaries = new ObservableCollection<Item>(
                Enum.GetValues(typeof(SummaryTitles)).Cast<SummaryTitles>()
                .Select(t => new Item() {
                    Title = t
                }));


            this.NotifyOfPropertyChange(() => this.Summaries);

            Task.Factory
                .StartNew(() =>
                    this.LoadAsync()
                )
            .ContinueWith(t => {
                var ex = t.Exception;
            }, TaskContinuationOptions.OnlyOnFaulted);
        }


        private void SetItem(SummaryTitles title, int count, bool loaded) {
            var item = this.Summaries.First(s => s.Title == title);
            item.Count = count;
            item.Loaded = true;
            item.NotifyOfPropertyChange("Count");
            item.NotifyOfPropertyChange("Loaded");
        }

        private void LoadAsync() {
            var d = DateTime.Now.AddDays(1);
            var cnt = this.OrderBiz.Count(new OrderSearchCondition() {
                Status = OrderStatus.WAIT_SELLER_SEND_GOODS,
                OffTime = d
            });
            this.SetItem(SummaryTitles.Timeout, cnt, true);

            cnt = this.OrderBiz.Count(new OrderSearchCondition() {
                Status = OrderStatus.WAIT_SELLER_SEND_GOODS
            });
            this.SetItem(SummaryTitles.WaitSendGoods, cnt, true);

            cnt = this.OrderBiz.Count(new OrderSearchCondition() {
                Status = OrderStatus.SELLER_PART_SEND_GOODS
            });
            this.SetItem(SummaryTitles.PartSendGoods, cnt, true);

            cnt = this.OrderBiz.Count(new OrderSearchCondition() {
                Status = OrderStatus.RISK_CONTROL
            });
            this.SetItem(SummaryTitles.Risk, cnt, true);


            cnt = this.OrderBiz.Count(new OrderSearchCondition() {
                Status = OrderStatus.IN_CANCEL
            });
            this.SetItem(SummaryTitles.InCancel, cnt, true);

            cnt = this.OrderBiz.Count(new OrderSearchCondition() {
                InIssue = true
            });
            this.SetItem(SummaryTitles.HaveInsure, cnt, true);

            cnt = this.OrderBiz.Count(new OrderSearchCondition() {
                Status = OrderStatus.WAIT_BUYER_ACCEPT_GOODS,
                IsShamShipping = true
            });
            this.SetItem(SummaryTitles.ShamShipping, cnt, true);

            Task.Factory.StartNew(() => {
                this.LoadWaitEvaluateOrder();
            });
        }

        private void LoadWaitEvaluateOrder() {
            this.WaitEvaluateOrders = new List<string>();
            var acsetting = new AccountSetting();
            foreach (var acc in acsetting.Value) {
                var api = new APIClient(acc.User, acc.Pwd);
                this.WaitEvaluateOrders.AddRange(api.Execute(new OrderWaitingEvaluateList()));
            }

            this.SetItem(SummaryTitles.WaitEvaluate, this.WaitEvaluateOrders.Count, true);
        }

        public void ReLoad() {
            throw new NotImplementedException();
        }

        public void View(Item data) {
            var seacher = GlobalData.GetInstance<IOrderSearcher>();
            var cond = new OrderSearchCondition();
            switch (data.Title) {
                case SummaryTitles.HaveInsure:
                    cond.InIssue = true;
                    break;
                case SummaryTitles.Risk:
                    cond.Status = OrderStatus.RISK_CONTROL;
                    break;
                case SummaryTitles.Timeout:
                    var d = DateTime.Now.AddDays(1);
                    cond.Status = OrderStatus.WAIT_SELLER_SEND_GOODS;
                    cond.OffTime = d;
                    break;
                case SummaryTitles.WaitSendGoods:
                    cond.Status = OrderStatus.WAIT_SELLER_SEND_GOODS;
                    break;
                case SummaryTitles.PartSendGoods:
                    cond.Status = OrderStatus.SELLER_PART_SEND_GOODS;
                    break;
                case SummaryTitles.ShamShipping:
                    cond.Status = OrderStatus.WAIT_BUYER_ACCEPT_GOODS;
                    cond.IsShamShipping = true;
                    break;
                case SummaryTitles.InCancel:
                    cond.Status = OrderStatus.IN_CANCEL;
                    break;
                case SummaryTitles.WaitEvaluate:
                    var vm = new OrderEvaluateViewModel(this.WaitEvaluateOrders);
                    GlobalData.GetInstance<ISheel>().Show(vm);
                    return;
            }
            seacher.Search(cond);
            seacher.Show(EnumHelper.GetDescription(data.Title));
        }
    }
}
