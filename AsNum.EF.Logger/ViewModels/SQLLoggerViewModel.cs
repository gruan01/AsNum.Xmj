using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Data;

namespace AsNum.Xmj.EFLogger.ViewModels {
    public class SQLLoggerViewModel : VMScreenBase, ILog {
        public override string Title {
            get { return "SQL捕获"; }
        }

        [Import]
        private LogObserverable LogObserver { get; set; }

        public Caliburn.Micro.BindableCollection<IDbCommand> CMDS { get; set; }

        private IDbCommand currCmd;
        public IDbCommand CurrCmd {
            get {
                return this.currCmd;
            }
            set {
                this.currCmd = value;
                if (value != null) {
                    this.CmdText = value.CommandText;
                    this.NotifyOfPropertyChange(() => this.CmdText);
                }
            }
        }

        public string CmdText {
            get;
            private set;
        }

        public bool CanStart {
            get {
                return !EFLogger.IsEnable;
            }
        }

        public bool CanStop {
            get {
                return EFLogger.IsEnable;
            }
        }

        public SQLLoggerViewModel() {
            //MefHelper.ComposeParts(this);
            GlobalData.MefContainer.ComposeParts(this);

            this.LogObserver.Attach(this);

            this.CMDS = new Caliburn.Micro.BindableCollection<IDbCommand>();
        }

        public void Start() {
            EFLogger.IsEnable = true;
            this.NotifyOfPropertyChange(() => this.CanStart);
            this.NotifyOfPropertyChange(() => this.CanStop);
        }

        public void Stop() {
            EFLogger.IsEnable = false;
            this.NotifyOfPropertyChange(() => this.CanStart);
            this.NotifyOfPropertyChange(() => this.CanStop);
        }

        public void Log(Exception ex) {

        }

        public void Log(string msg, bool canClear = true) {

        }

        public void Log(System.Data.IDbCommand cmd) {
            this.CMDS.Add(cmd);
        }

        public void Clear() {
            this.CMDS.Clear();
            //this.NotifyOfPropertyChange(() => this.CMDS);
        }
    }
}
