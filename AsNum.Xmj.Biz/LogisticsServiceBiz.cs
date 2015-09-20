using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsNum.Xmj.Entity;
using AsNum.Xmj.Data;
using System.Data.Entity.Migrations;
using AsNum.Common.Extends;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.Biz {

    [Export(typeof(ILogisticsService))]
    public class LogisticsServiceBiz : BaseBiz, ILogisticsService {
        public IEnumerable<LogisticServices> GetAll() {
            using (var db = new Entities()) {
                return db.LogisticServices.OrderByDescending(l => l.IsUsual)
                    .ThenByDescending(l => l.Order)
                    .ThenBy(l => l.NameEn)
                    .ToList();
            }
        }

        public void Save(IEnumerable<LogisticServices> services) {
            using (var db = new Entities()) {
                var codes = services.Select(s => s.Code);
                var ds = db.LogisticServices.Where(l => !codes.Contains(l.Code));
                db.LogisticServices.RemoveRange(ds);
                db.LogisticServices.AddOrUpdate(services.ToArray());

                this.Errors = db.GetErrors();
                if (!this.HasError)
                    db.SaveChanges();
            }
        }
    }
}
