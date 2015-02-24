using AsNum.Common.Extends;
using AsNum.Xmj.API.Res;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Windows.Forms;

namespace CopyProduct {
    static class Program {

        private static IUnityContainer Container = new UnityContainer();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TypeDescriptorHelper.AutoSetDisplayAttributeFromResource("AsNum.Aliexpress.API", Resource.ResourceManager);

            Container.LoadConfiguration();

            Application.ThreadException += Application_ThreadException;
            var form = Container.Resolve<Form1>();
            Application.Run(form);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            var ex = e.Exception.GetBaseException();
            MessageBox.Show(ex.Message);
        }
    }
}
