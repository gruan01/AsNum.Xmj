using AsNum.Aliexpress.Common;
using AsNum.Aliexpress.Data.Repositories;
using AsNum.Aliexpress.Entity;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class PProductCatelogViewModel : VMScreenBase {
        public override string Title {
            get {
                return "属性挂载分类管理";
            }
        }

        public BindableCollection<PProductCatelog> Catelogs {
            get;
            set;
        }

        public PProductCatelogViewModel() {
            this.LoadData();
        }

        public void Save() {
            using (var uw = Global.MefContainer.GetExportedValue<IUnitOfWork>()) {
                var rep = uw.GetRepository<PProductCatelog>();
                rep.AddOrUpdate(this.Catelogs);
                uw.Commit();
            }
        }

        private void LoadData() {
            using (var uw = Global.MefContainer.GetExportedValue<IUnitOfWork>()) {
                var rep = uw.GetRepository<PProductCatelog>();
                this.Catelogs = new BindableCollection<PProductCatelog>(rep.All);
            }
            this.NotifyOfPropertyChange("Catelogs");
        }

        public void Reload() {
            this.LoadData();
        }
    }
}
