using AsNum.Xmj.Data;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.Biz {

    [Export(typeof(IBuyer))]
    public class BuyerBiz : IBuyer {
        public void UpdateLevel(string buyerID, string level) {
            using (var db = new Entities()) {
                var l = db.BuyerLevels.Find(buyerID);
                if (l == null) {
                    l = new Entity.BuyerLevel();
                    db.BuyerLevels.Add(l);
                }

                l.BuyerID = buyerID;
                l.Level = level;
                l.UpdateOn = DateTime.Now;

                db.SaveChanges();
            }
        }
    }
}
