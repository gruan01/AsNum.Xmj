using AsNum.Aliexpress.Common;
using AsNum.Aliexpress.Data.Repositories;
using AsNum.Aliexpress.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {

    public class PurchaseNoteViewModel : VMScreenBase {

        public event EventHandler Saved = null;

        public override string Title {
            get {
                return "采购备注";
            }
        }

        public OrderNote OrderNote {
            get;
            set;
        }

        public PurchaseNoteViewModel(string orderNO) {

            using (var uw = Global.MefContainer.GetExportedValue<IUnitOfWork>()) {
                var rep = uw.GetRepository<OrderNote>();
                this.OrderNote = rep.Find(n => n.OrderNO == orderNO);
            }
            if (this.OrderNote == null) {
                this.OrderNote = new OrderNote() {
                    OrderNO = orderNO
                };
            }
        }

        public void Save() {
            using (var uw = Global.MefContainer.GetExportedValue<IUnitOfWork>()) {
                var rep = uw.GetRepository<OrderNote>();
                rep.AddOrUpdate(this.OrderNote);
                uw.Commit();
            }

            if (this.Saved != null)
                this.Saved.Invoke(this.OrderNote, new EventArgs());
        }
    }
}
