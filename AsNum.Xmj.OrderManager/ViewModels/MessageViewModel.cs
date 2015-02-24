using AsNum.Aliexpress.Common;
using AsNum.Aliexpress.Data.Repositories;
using AsNum.Aliexpress.Entity;
using AsNum.Common;
using AsNum.Xmj.AliSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class MessageViewModel : VMScreenBase {
        public override string Title {
            get {
                return "订单留言";
            }
        }

        private string OrderNo = "";
        private string Account = "";

        public List<OrderMessageEx> Msgs {
            get;
            set;
        }

        public MessageViewModel(string orderNO, string account) {
            this.OrderNo = orderNO;
            this.Account = account;

            using (var uw = Global.MefContainer.GetExportedValue<IUnitOfWork>()) {
                var rep = uw.GetRepository<OrderMessage>();
                this.Msgs = this.DealMessage(rep.Query(m => m.OrderNO == orderNO)
                    .OrderByDescending(m => m.CreateOn)
                    .ToList());
            }
        }

        private List<OrderMessageEx> DealMessage(List<OrderMessage> msgs) {
            if (msgs == null || msgs.Count == 0)
                return null;

            var mes = msgs.Select(m => {
                var me = new OrderMessageEx();
                DynamicCopy.CopyProperties(m, me);
                return me;
            }).ToList();

            var f = mes.First().Sender;
            mes.ForEach(m => {
                m.Left = m.Sender.Equals(f);
            });

            return mes;
        }

        public void Sync() {
            var msgs = MessageSync.SyncByOrderNO(this.OrderNo, this.Account);
            var msgs1 = msgs.Select(m => new OrderMessage {
                ID = m.ID,
                Content = m.Content,
                OrderNO = this.OrderNo,
                Sender = m.Sender,
                CreateOn = m.CreateOn
            }).ToList();
            this.Msgs = this.DealMessage(msgs1);

            this.NotifyOfPropertyChange("Msgs");

            if (this.Msgs != null && this.Msgs.Count > 0)
                using (var uw = Global.MefContainer.GetExportedValue<IUnitOfWork>()) {
                    var rep = uw.GetRepository<OrderMessage>();
                    foreach (var m in msgs1) {
                        rep.AddOrUpdate(m);
                    }
                    uw.Commit();
                }
        }
    }
}
