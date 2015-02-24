using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AsNum.Common.WPF {
    public class UniPadding {

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.RegisterAttached("Padding", typeof(float), typeof(UniPadding), new PropertyMetadata(PaddingChanged));

        public static void SetPadding(FrameworkElement target, float value) {
            target.SetValue(PaddingProperty, value);
        }

        public static float GetPadding(FrameworkElement target) {
            return (float)target.GetValue(PaddingProperty);
        }

        private static void PaddingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var fe = (FrameworkElement)d;
            fe.Loaded += fe_Loaded;
        }

        static void fe_Loaded(object sender, RoutedEventArgs e) {
            Update((FrameworkElement)sender);
        }

        private static void Update(FrameworkElement target) {
            var cc = VisualTreeHelper.GetChildrenCount(target);

            for (var i = 0; i < cc; i++) {
                var child = VisualTreeHelper.GetChild(target, i);
                //BindingOperations.SetBinding(child)
                var dp = TypeDescriptor.GetProperties(child).Find("Margin", false);
                if (dp != null) {
                    Binding binding = new Binding();
                    binding.Source = target;
                    binding.Path = new PropertyPath(UniPadding.PaddingProperty);// new PropertyPath("Padding");
                    var dpd = DependencyPropertyDescriptor.FromProperty(dp);
                    BindingOperations.SetBinding(child, dpd.DependencyProperty, binding);
                }
            }
        }
    }
}
