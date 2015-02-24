using AsNum.Common.Extends;
using AsNum.Xmj.BizEntity.Models;
using AsNum.Xmj.Data;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace AsNum.Xmj.Biz {

    [Export(typeof(IAccount))]
    public class AccountBiz : BaseBiz, IAccount {
        public IEnumerable<Entity.Owner> AllAccounts() {
            using (var db = new Entities()) {
                return db.Owners.OrderBy(a => a.Account).ToList();
            }
        }


        public void Save(IEnumerable<Entity.Owner> accounts) {
            using (var db = new Entities()) {
                foreach (var acc in accounts) {
                    var ex = db.Owners.FirstOrDefault(a => a.Account == acc.Account);
                    if (ex == null)
                        db.Owners.Add(ex);
                    else
                        acc.CopyToExcept(ex);
                }

                this.Errors = db.GetErrors();
                if (!this.HasError)
                    db.SaveChanges();
            }
        }


        public IEnumerable<TurnoverByAccount> TurnoverByAccount(DateTime? endDate = null, int monthCount = 6) {
            using (var db = new Entities()) {
                return db.Query<TurnoverByAccount>(
                    @"SELECT 
	                    SUM(Amount) AS TotalAmount,
	                    Account,
	                    CONVERT(NVARCHAR(6), PaymentOn, 112) AS YearMonth
                    FROM
	                    Orders
                    WHERE
	                    PaymentOn IS NOT NULL
                        AND DATEDIFF(MONTH, PaymentOn, @p0) BETWEEN 0 AND @p1
                    Group BY
	                    Account, CONVERT(NVARCHAR(6), PaymentOn, 112)"
                    , new {
                        p0 = endDate == null ? DateTime.Now : endDate,
                        p1 = monthCount
                    }).ToList();
            }
        }


        public IEnumerable<TurnoverByAccount> TurnoverInNDays(int days) {
            using (var db = new Entities()) {
                return db.Query<TurnoverByAccount>(@"SELECT 
	                    SUM(Amount) AS TotalAmount,
	                    Account
                    FROM
	                    Orders
                    WHERE
	                    PaymentOn IS NOT NULL
                        AND DATEDIFF(DAY, PaymentOn, GETDATE()) <= @p0
                    Group BY
	                    Account", new { p0 = days }).ToList();
            }
        }
    }
}
