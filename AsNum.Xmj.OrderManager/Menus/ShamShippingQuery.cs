using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.OrderManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.Menus {
    [Export(typeof(IMenuItem)), ExportMetadata("TopMenuTag", TopMenuTags.OrderAndProduct)]
    public class ShamShippingMenu : MenuItemBase {
        public override string Header {
            get {
                return "已填单未发货";
            }
        }

        public override string Group {
            get {
                return "order";
            }
        }

        public override void Execute(object obj) {

            var cond = new OrderSearchCondition() {
                Status = OrderStatus.WAIT_BUYER_ACCEPT_GOODS,
                IsShamShipping = true
            };
            var vm = new OrderQueryViewModel();
            vm.Search(cond);
            vm.Show("已填单未发货的订单");
        }
    }
}
