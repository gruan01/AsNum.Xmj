using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsNum.Xmj.ManualAuth {
    /// <summary>
    /// AuthView.xaml 的交互逻辑
    /// </summary>
    public partial class AuthView : UserControl {
        public AuthView() {
            InitializeComponent();
        }

        //private async void WebBrowser_Navigating(object sender, NavigatingCancelEventArgs e) {
        //    if (e.WebRequest != null)
        //        using (var stm = await e.WebRequest.GetRequestStreamAsync())
        //        using (StreamReader sr = new StreamReader(stm)) {
        //            var result = await sr.ReadToEndAsync();
        //        }
        //}
    }
}
