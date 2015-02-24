using System.Windows;

namespace AsNum.Common.WPF {
    /// <summary>
    /// <remarks>
    /// http://www.dotblogs.com.tw/gn01617787/archive/2013/03/31/99675.aspx
    /// </remarks>
    /// </summary>
    public class BindingProxy : Freezable {
        protected override Freezable CreateInstanceCore() {
            return new BindingProxy();
        }

        public object Data {
            get {
                return (object)GetValue(DataProperty);
            }
            set {
                SetValue(DataProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Data.
        // This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object),
            typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}
