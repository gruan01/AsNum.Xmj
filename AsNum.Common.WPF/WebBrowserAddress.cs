using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AsNum.Common.WPF {
    public class WebBrowserAddress {

        public static readonly DependencyProperty AddressProperty =
               DependencyProperty.RegisterAttached(
                   "Address", 
                   typeof(string), 
                   typeof(WebBrowserAddress), 
                   new UIPropertyMetadata(null, AddressPropertyChanged));

        public static string GetAddress(DependencyObject obj) {
            return (string)obj.GetValue(AddressProperty);
        }

        public static void SetAddress(DependencyObject obj, string value) {
            obj.SetValue(AddressProperty, value);
        }

        public static void AddressPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            WebBrowser browser = o as WebBrowser;
            if (browser != null) {
                string uri = e.NewValue as string;
                browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
            }
        }

    }
}
