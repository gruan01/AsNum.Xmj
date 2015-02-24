using AsNum.Xmj.Common;
using Caliburn.Micro;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AsNum.Xmj.Logger.ViewModels {
    [Export(typeof(AsNum.Xmj.Common.Interfaces.ILog))]
    public class InfoShowerViewModel : VMScreenBase, AsNum.Xmj.Common.Interfaces.ILog {

        public override string Title {
            get {
                return "消息";
            }
        }

        public BindableCollection<InfoItem> Msgs {
            get;
            set;
        }

        //public Queue<InfoItem> Msgs {
        //    get;
        //    set;
        //}

        public InfoItem LastMsg {
            get {
                if (this.Msgs != null && this.Msgs.Count > 0)
                    return this.Msgs.Last();
                return new InfoItem();
            }
        }

        [ImportingConstructor]
        public InfoShowerViewModel(LogObserverable observer) {
            observer.Attach(this);
            this.Msgs = new BindableCollection<InfoItem>();
            //this.Msgs = new Queue<InfoItem>(200);
        }

        public override object GetView(object context = null) {
            return base.GetView(context);
        }

        public void Log(Exception ex) {
            this.Log(ex.GetBaseException().Message, false);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Log(string msg, bool canClear = true) {

            if (this.Msgs.Count >= 200) {
                var m = this.Msgs.FirstOrDefault(mm => mm.CanClear);
                this.Msgs.Remove(m);
            }

            this.Msgs.Add(new InfoItem() {
                Time = DateTime.Now,
                Msg = msg,
                CanClear = canClear
            });

            //if (this.Msgs.Count > 200)
            //    this.Msgs.Dequeue();

            //this.Msgs.Enqueue(new InfoItem() {
            //    Time = DateTime.Now,
            //    Msg = msg,
            //    CanClear = canClear
            //});

            //this.NotifyOfPropertyChange("Msgs");

            //if (this.Msgs.Count > 200) {
            //    var rms = this.Msgs.Take(this.Msgs.Count - 200);
            //    this.Msgs.RemoveRange(rms);
            //}

            this.NotifyOfPropertyChange("LastMsg");
        }


        public void Log(System.Data.IDbCommand cmd) {
            
        }
    }
}
