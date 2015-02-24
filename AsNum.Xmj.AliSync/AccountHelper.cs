using AsNum.Common.Extends;
using System.Collections.ObjectModel;
using System.Linq;

namespace AsNum.Xmj.AliSync {
    public class AccountHelper {
        public static ObservableCollection<Account> LoadAccounts() {
            return PersistFileHelper.Load<ObservableCollection<Account>>("accounts");
        }

        public static Account GetAccount(string account) {
            var accs = LoadAccounts();
            return accs.FirstOrDefault(a => a.User.Equals(account, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
