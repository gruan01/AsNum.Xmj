using AsNum.Xmj.AliSync;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.Common;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AsNum.Xmj.OnlineLogistics.ViewModels {
    public class LogisticsServiceViewModel : VMScreenBase {
        public override string Title {
            get {
                return "物流服务";
            }
        }

        public ObservableCollection<LogisticServices> Datas { get; set; }

        public LogisticsServiceViewModel() {
            //this.Datas = new ObservableCollection<LogisticServices>();
            this.LoadFromDb();
        }

        public void LoadFromWeb() {
            var account = AccountHelper.LoadAccounts().FirstOrDefault();
            if (account == null)
                return;
            var client = new API.APIClient(account.User, account.Pwd);
            var method = new LogisticsServiceList();
            var lst = client.Execute(method);

            lst.ForEach(l => {
                var item = this.Datas.FirstOrDefault(d => d.Code.Equals(l.Code, StringComparison.OrdinalIgnoreCase));
                if (item == null) {
                    item = new LogisticServices() {
                        Code = l.Code
                    };
                    this.Datas.Add(item);
                }

                item.CheckRegex = l.CheckRule;
                item.Company = l.Company;
                item.MaxProcessDays = l.MaxProcessDays;
                item.MiniProcessDays = l.MinProcessDays;
                item.NameEn = l.Name;
            });
        }

        public void LoadFromDb() {
            var biz = GlobalData.GetInstance<ILogisticsService>();
            this.Datas = new ObservableCollection<LogisticServices>(biz.GetAll());
        }

        public void Save() {
            var biz = GlobalData.GetInstance<ILogisticsService>();
            biz.Save(this.Datas);

            if(MessageBox.Show("需要重启已应用设置", "提示", MessageBoxButton.YesNo) == MessageBoxResult.OK) {
                System.Windows.Forms.Application.Restart();
                Application.Current.Shutdown();
            }
        }
    }
}
