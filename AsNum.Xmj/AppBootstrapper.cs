using AsNum.Common;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AsNum.Xmj {
    public class AppBootstrapper : Bootstrapper<ISheel> {

        //private CompositionContainer Container = null;

        protected override void StartRuntime() {
            base.StartRuntime();

            //用 Assembly.Instance.Add 可以用于解决加载不同DLL内的View
            var dllFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories);
            foreach (var dll in dllFiles) {
                try {
                    var asm = Assembly.LoadFrom(dll);
                    if (asm.GetTypes().Any(t =>
                        t.GetInterfaces().Contains(typeof(IViewAware))
                        || t.GetInterfaces().Contains(typeof(IScreen))
                        )) {
                        AssemblySource.Instance.Add(asm);
                    }
                } catch {
                }
            }
        }

        ////主屏幕启动后才会执行这个方法，所以 AssemblySource 的操作不能放到这里
        //protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
        //    base.OnStartup(sender, e);
        //}

        protected override void Configure() {

            var catalog = MefHelper.SafeDirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory);

            AssemblySource.Instance.Select(x => new AssemblyCatalog(x))
                .OfType<ComposablePartCatalog>()
                .ToList().ForEach((c) => {
                    catalog.Catalogs.Add(c);
                });

            GlobalData.MefContainer = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);

            var batch = new CompositionBatch();
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());

            batch.AddExportedValue(GlobalData.MefContainer);

            //this.Container.ComposeParts(this);
            GlobalData.MefContainer.Compose(batch);

            EFLogger.EFLogger.Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="key">viewFirst 时候,这个参数有值</param>
        /// <returns></returns>
        protected override object GetInstance(Type service, string key) {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = GlobalData.MefContainer.GetExportedValues<Object>(contract);
            var a = GlobalData.MefContainer.GetExportedValues<Object>();
            if (exports.Count() > 0)
                return exports.First();

            //throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
            return null;
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType) {
            return GlobalData.MefContainer.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance) {
            GlobalData.MefContainer.SatisfyImportsOnce(instance);
        }

        //protected override IEnumerable<System.Reflection.Assembly> SelectAssemblies() {
        //    return base.SelectAssemblies();
        //}

        protected override void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            //base.OnUnhandledException(sender, e);

            var logObserver = GlobalData.MefContainer.GetExportedValue<LogObserverable>();
            var ex = e.Exception.GetBaseException();
            logObserver.Notify(ex);

            e.Handled = true;
        }

        protected override void OnExit(object sender, EventArgs e) {
            base.OnExit(sender, e);
        }
    }
}
