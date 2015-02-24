using AsNum.Common.Extends;
using AsNum.Xmj.Data;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AsNum.Xmj.Biz {
    [Export(typeof(ILogisticFee))]
    public class LogisticFeeBiz : BaseBiz, ILogisticFee {
        public void Save(IEnumerable<LogisticFee> fees) {
            using (var db = new Entities()) {
                var tns = fees.Select(f => f.TrackNO);
                var exs = db.LogisticFees.Where(f => tns.Contains(f.TrackNO));
                foreach (var fee in fees) {
                    var ex = exs.FirstOrDefault(e => e.TrackNO.Equals(fee.TrackNO));
                    if (ex != null) {
                        fee.CopyToExcept(ex);
                    }
                }

                this.Errors = db.GetErrors();
                if (!this.HasError)
                    db.SaveChanges();
            }
        }
    }
}
