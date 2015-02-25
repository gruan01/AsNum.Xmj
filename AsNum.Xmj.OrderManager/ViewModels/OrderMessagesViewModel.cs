using AsNum.Common.Extends;
using AsNum.Xmj.Common;
using AsNum.Xmj.Entity;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class OrderMessagesViewModel : VMScreenBase {
        public override string Title {
            get { return "订单留言列表"; }
        }

        public BindableCollection<OrderMessageEx> Messages { get; set; }

        public OrderMessagesViewModel() {

        }

        public void Build(Order order) {
            this.Messages = new BindableCollection<OrderMessageEx>(this.DealMessage(order.Messages));
            this.NotifyOfPropertyChange(() => this.Messages);
        }

        private List<OrderMessageEx> DealMessage(IEnumerable<OrderMessage> msgs) {
            if (msgs == null || msgs.Count() == 0)
                return new List<OrderMessageEx>();

            var mes = msgs.Select(m => {
                var me = new OrderMessageEx();
                DynamicCopy.CopyTo(m, me);
                return me;
            }).ToList();

            var f = mes.First().Sender;
            mes.ForEach(m => {
                m.Left = m.Sender.Equals(f);
            });

            return mes;
        }
    }
}
