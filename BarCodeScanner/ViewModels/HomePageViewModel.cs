using Caliburn.Micro;

namespace BarCodeScanner.ViewModels {
    public class HomePageViewModel {

        private INavigationService NS;

        public HomePageViewModel(INavigationService ns) {
            this.NS = ns;
        }

        public void Scan() {
            NS.UriFor<ScannerPageViewModel>().Navigate();
        }
    }
}
