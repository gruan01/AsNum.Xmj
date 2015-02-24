using System.Windows;
using System.Windows.Media;

namespace AsNum.WPF.Controls {
    public static class Helper {

        public static Window GetParentWindow( this DependencyObject child) {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) {
                return null;
            }

            Window parent = parentObject as Window;
            if (parent != null) {
                return parent;
            } else {
                return GetParentWindow(parentObject);
            }
        }

    }
}
