using AsNum.Common.Extends;
using System;
using System.Windows.Forms;

namespace AsNum.Xmj.APITestTool {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;

            //TypeDescriptorHelper.SetDisplayAttributFromResource<OrderQueryList>(AsNum.Aliexpress.API.Res.Resource.ResourceManager);
            TypeDescriptorHelper.AutoSetDisplayAttributeFromResource("AsNum.Aliexpress.API", AsNum.Xmj.API.Res.Resource.ResourceManager);

            Application.Run(new Form1());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            var ex = e.Exception.GetBaseException();
            MessageBox.Show(ex.Message);
        }
    }
}
