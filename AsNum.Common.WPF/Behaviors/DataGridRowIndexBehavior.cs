using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AsNum.Common.WPF.Behaviors {
    public class DataGridRowIndexBehavior : Behavior<DataGrid> {

        protected override void OnAttached() {
            base.OnAttached();
            this.AssociatedObject.LoadingRow += AssociatedObject_LoadingRow;
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.LoadingRow -= AssociatedObject_LoadingRow;
        }

        void AssociatedObject_LoadingRow(object sender, DataGridRowEventArgs e) {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

    }
}
