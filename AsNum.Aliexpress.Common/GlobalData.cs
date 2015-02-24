using System.ComponentModel;
using System.ComponentModel.Composition.Hosting;

namespace AsNum.Xmj.Common {
    public static class GlobalData {

        public static CompositionContainer MefContainer = null;

        public static T GetInstance<T>() {
            return MefContainer.GetExportedValue<T>();
        }

        private static GlobalDataHolder instance;
        public static GlobalDataHolder Instance {
            get {
                if (instance == null)
                    instance = new GlobalDataHolder();
                return instance;
            }
        }




        public class GlobalDataHolder : INotifyPropertyChanged {

            internal GlobalDataHolder() { }

            public event PropertyChangedEventHandler PropertyChanged;
            public void NotifyPropertyChanged(string propertyName) {
                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            private ViewModes viewMode;
            public ViewModes ViewMode {
                get {
                    return this.viewMode;
                }
                set {
                    if (this.viewMode != value) {
                        this.viewMode = value;
                        this.NotifyPropertyChanged("ViewMode");
                    }
                }
            }
        }
    }
}
