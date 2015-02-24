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
    public class PropertyMapViewModel : VMScreenBase {
        public override string Title {
            get {
                return "属性挂靠";
            }
        }

        public OrderDetail Detail {
            get;
            private set;
        }

        public BindableCollection<PProductCatelog> Catelogs {
            get;
            set;
        }

        public PropertyMapViewModel(OrderDetail detail) {
            this.Detail = detail;

            using (var uw = Global.MefContainer.GetExportedValue<IUnitOfWork>()) {
                var rep = uw.GetRepository<PProductCatelog>();
                this.Catelogs = new BindableCollection<PProductCatelog>(rep.All);
            }
        }
    }
}
