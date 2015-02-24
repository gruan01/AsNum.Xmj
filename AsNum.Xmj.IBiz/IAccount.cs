using AsNum.Xmj.BizEntity.Models;
using AsNum.Xmj.Entity;
using System;
using System.Collections.Generic;

namespace AsNum.Xmj.IBiz {
    public interface IAccount : IBaseBiz {

        IEnumerable<Owner> AllAccounts();

        void Save(IEnumerable<Owner> accounts);

        IEnumerable<TurnoverByAccount> TurnoverByAccount(DateTime? endDate = null, int monthCount = 6);

        IEnumerable<TurnoverByAccount> TurnoverInNDays(int days);
    }
}
