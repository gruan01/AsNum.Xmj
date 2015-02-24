using AsNum.Xmj.Entity;
using System.Collections.Generic;

namespace AsNum.Xmj.IBiz {
    public interface ILogisticFee {

        void Save(IEnumerable<LogisticFee> fees);

    }
}
