using System;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AsNum.Common.WPF.Behaviors {
    public class DataGridScrollIntoViewBehavior : Behavior<DataGrid> {

        protected override void OnAttached() {
            base.OnAttached();
            this.AssociatedObject.SelectionChanged += new SelectionChangedEventHandler(AssociatedObject_SelectionChanged);
        }
        void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (sender is DataGrid) {
                DataGrid grid = (sender as DataGrid);
                if (grid.SelectedItem != null) {
                    grid.Dispatcher.BeginInvoke(new Action(()=> {
                        grid.UpdateLayout();
                        grid.ScrollIntoView(grid.SelectedItem, null);
                    }), null);
                }
            }
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.SelectionChanged -=
                new SelectionChangedEventHandler(AssociatedObject_SelectionChanged);
        }
    }

}
