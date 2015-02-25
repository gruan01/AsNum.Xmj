using AsNum.Xmj.Common;
using AsNum.Xmj.Entity;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class SubOrderViewModel : VMScreenBase {
        public override string Title {
            get { return "子订单"; }
        }

        public BindableCollection<OrderDetail> SubOrders { get; set; }

        public void Build(Order order) {
            if (order != null)
                this.SubOrders = new BindableCollection<OrderDetail>(order.Details);
            else
                this.SubOrders = null;

            this.NotifyOfPropertyChange(() => this.SubOrders);
        }
    }
}
