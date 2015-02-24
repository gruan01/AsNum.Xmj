using Caliburn.Micro;

namespace AsNum.Xmj.Common {
    public abstract class VMScreenBase : Screen {

        //public ViewModes ViewMode {
        //    get {
        //        return GlobalData.ViewMode;
        //    }
        //    set {
        //        GlobalData.ViewMode = value;
        //        this.NotifyOfPropertyChange(() => this.ViewMode);
        //    }
        //}

        private bool closeAble = true;

        public bool CloseAble {
            get {
                return this.closeAble;
            }
            set {
                this.closeAble = value;
            }
        }

        public abstract string Title { get; }

        public new string DisplayName {
            get {
                return this.Title;
            }
        }
    }
}
