using AsNum.Common.Extends;
using AsNum.Xmj.Common.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;

namespace CopyProduct {
    [Export(typeof(ILog))]
    internal class Logger : ILog {

        private TextBox TxtLog {
            get {
                return Application.OpenForms.OfType<Form1>().First().Controls.OfType<TextBox>().FirstOrDefault(r => r.Name == "txtLog");
            }
        }



        public void Log(Exception ex) {
            var baseEx = ex.GetBaseException();
            this.Log(baseEx.Message, false);
        }

        public void Log(string msg, bool canClear = true) {
            if (this.TxtLog == null)
                return;

            var txt = string.Format("{0}\t{1}\r\n", DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), msg);
            this.TxtLog.InvokeIfNeed(() => {
                this.TxtLog.AppendText(txt);
            });
        }


        public void Log(System.Data.IDbCommand cmd) {
            
        }
    }
}
