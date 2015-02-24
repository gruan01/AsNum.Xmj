using AsNum.Common.Extends;
using AsNum.Xmj.Data;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;

namespace AsNum.Xmj.Biz {

    [Export(typeof(IReceiver))]
    public class ReceiverBiz : BaseBiz, IReceiver {
        public AdjReceiver Save(AdjReceiver receiver) {
            using (var db = new Entities()) {
                var ex = db.AdjReceivers.FirstOrDefault(r => r.OrderNO == receiver.OrderNO);
                if (ex != null) {
                    receiver.CopyToExcept(ex, r => r.OrderFor, r => r.Country);
                } else {
                    db.AdjReceivers.Add(receiver);
                }

                this.Errors = db.GetErrors();
                if (!this.HasError) {
                    db.SaveChanges();
                }

                //带出修改后的国家， 
                return db.AdjReceivers.Include(r => r.Country).FirstOrDefault(r => r.OrderNO == receiver.OrderNO);
            }
        }


        public void RemoveAdjReceiver(string orderNO) {
            using (var db = new Entities()) {
                var ex = db.AdjReceivers.FirstOrDefault(r => r.OrderNO == orderNO);
                if (ex != null) {
                    db.AdjReceivers.Remove(ex);
                    db.SaveChanges();
                }
            }
        }
    }
}
