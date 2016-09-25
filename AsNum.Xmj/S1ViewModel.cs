using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj {

    [Export(typeof(ISheel))]
    public class S1ViewModel : Caliburn.Micro.PropertyChangedBase, ISheel {

        public string T { get; set; }

        public S1ViewModel() {
            this.T = DateTime.Now.ToString();
            this.NotifyOfPropertyChange(() => this.T);
        }

        public void Show(IScreen obj, bool once = false) {
            throw new NotImplementedException();
        }

        public void TTT() {

        }
    }
}
