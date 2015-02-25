using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AsNum.Common.WPF {
    public class DataGridRowIndex {

        public static readonly DependencyProperty ShowProperty = DependencyProperty.RegisterAttached("Show", typeof(bool), typeof(DataGridRowIndex), new PropertyMetadata(ShowChanged));

        public static void SetShow(FrameworkElement target, bool value) {
            target.SetValue(ShowProperty, value);
        }

        private static void ShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var dg = (FrameworkElement)d;
            dg.Loaded += dg_Loaded;
        }

        static void dg_Loaded(object sender, RoutedEventArgs e) {
            Update((FrameworkElement)sender);
        }

        private static void Update(FrameworkElement target) {
            var dg = (DataGrid)target;
            if (dg == null)
                return;

            dg.LoadingRow += dg_LoadingRow;
        }

        static void dg_LoadingRow(object sender, DataGridRowEventArgs e) {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}
