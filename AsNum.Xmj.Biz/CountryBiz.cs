using AsNum.Xmj.Data;
using AsNum.Xmj.IBiz;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AsNum.Xmj.Biz {

    [Export(typeof(ICountry))]
    public class CountryBiz : ICountry {

        public IEnumerable<Entity.Country> AllCountry() {
            using (var db = new Entities()) {
                return db.Countries.OrderBy(c => c.CountryCode).ToList();
            }
        }
    }
}
