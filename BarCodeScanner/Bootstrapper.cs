using BarCodeScanner.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BarCodeScanner {
    public class Bootstrapper : PhoneBootstrapper {

        private PhoneContainer Container;
        protected override void StartRuntime() {
            base.StartRuntime();
        }
        protected override void Configure() {
            base.Configure();

            this.Container = new PhoneContainer();
            this.Container.RegisterPhoneServices(this.RootFrame);

            this.Container.PerRequest<HomePageViewModel>();
            this.Container.PerRequest<ScannerPageViewModel>();
        }

        protected override object GetInstance(Type service, string key) {
            return this.Container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return this.Container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            this.Container.BuildUp(instance);
        }

        protected override void OnUnhandledException(object sender, System.Windows.ApplicationUnhandledExceptionEventArgs e) {
            base.OnUnhandledException(sender, e);

            if (!Debugger.IsAttached)
                Debugger.Launch();
        }
    }
}
