using AsNum.Xmj.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;

namespace AsNum.ScreenShoter {

    [Export(typeof(IScreenShoter))]
    public class ScreenShoter : IScreenShoter, IDisposable {
        private List<IScreenShoterObserver> Observers = new List<IScreenShoterObserver>();

        private ShotForm Form = null;

        #region IScreenShoter
        public void Attach(IScreenShoterObserver observer) {
            this.Observers.Add(observer);
        }

        public void Detach(IScreenShoterObserver observer) {
            this.Observers.Remove(observer);
        }

        public void Notify(Bitmap img) {
            foreach (var o in this.Observers) {
                o.Update(img);
            }
        }
        #endregion

        public ScreenShoter() {
            this.Form = new ShotForm();
            this.Form.SelectComplete += form_SelectComplete;
            this.Form.Show();
        }

        void form_SelectComplete(object sender, CompleteEventArgs e) {
            this.Notify(e.Img);
        }

        public void Launch() {
            this.Form.Launch();
        }

        private bool isDisposed = false;
        private void Dispose(bool disposing) {
            if (!isDisposed) {
                if (disposing && this.Form != null) {
                    this.Form.Dispose();
                }
                this.isDisposed = true;
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
