using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace AsNum.Common.WPF.Behaviors {
    public class UnifiedPaddingBeahavior : Behavior<FrameworkElement> {

        public Thickness Padding {
            get;
            set;
        }

        protected override void OnAttached() {
            base.OnAttached();

            this.AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e) {
            var cc = VisualTreeHelper.GetChildrenCount(this.AssociatedObject);

            for (var i = 0; i < cc; i++) {
                var child = VisualTreeHelper.GetChild(this.AssociatedObject, i);
                //BindingOperations.SetBinding(child)
                var dp = TypeDescriptor.GetProperties(child).Find("Margin", false);
                if (dp != null) {
                    Binding binding = new Binding();
                    binding.Source = this;
                    binding.Path = new PropertyPath("Padding");
                    var dpd = DependencyPropertyDescriptor.FromProperty(dp);
                    BindingOperations.SetBinding(child, dpd.DependencyProperty, binding);
                }
            }
        }

    }
}
