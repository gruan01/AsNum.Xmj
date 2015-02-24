using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Linq;

namespace AsNum.Xmj {
    [Export(typeof(StartUpViewModel)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class StartUpViewModel : VMScreenBase {
        public override string Title {
            get {
                return "起始页";
            }
        }

        public BindableCollection<IStartUpModel> Models {
            get;
            set;
        }

        public StartUpViewModel() {
            this.Models = new BindableCollection<IStartUpModel>(GlobalData.MefContainer.GetExports<IStartUpModel>().Select(i => i.Value));
            //var x = Dispatcher.CurrentDispatcher;
            //Task.Factory.StartNew(() => {
            foreach (var m in this.Models) {
                //x.BeginInvoke(DispatcherPriority.Send, new System.Action(() => {
                m.Load();
                //}));
            }
            //});
        }

    }
}
