using AsNum.Xmj.Common;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Xmj.Report.ViewModels {
    public class AccountQuickCodeViewModel : VMScreenBase {
        public override string Title {
            get {
                return "账户识别码维护";
            }
        }

        public List<Owner> Owners {
            get;
            private set;
        }

        public IAccount AccountBiz { get; set; }

        public AccountQuickCodeViewModel() {
            this.AccountBiz = GlobalData.MefContainer.GetExportedValue<IAccount>();
            this.Owners = this.AccountBiz.AllAccounts().ToList();
        }

        public void Save() {
            this.AccountBiz.Save(this.Owners);
            if (this.AccountBiz.HasError) {
                throw new Exception(string.Join(";", this.AccountBiz.Errors.Select(e => e.Value)));
            }
        }
    }
}
