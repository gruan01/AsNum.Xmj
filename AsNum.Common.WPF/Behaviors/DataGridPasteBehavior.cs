using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace AsNum.Common.WPF.Behaviors {
    public class DataGridPasteBehavior : Behavior<DataGrid> {

        protected override void OnAttached() {
            base.OnAttached();
            this.AssociatedObject.KeyDown += AssociatedObject_KeyDown;
            this.AssociatedObject.KeyUp += AssociatedObject_KeyUp;
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
            this.AssociatedObject.KeyUp -= AssociatedObject_KeyUp;
        }

        //http://support.microsoft.com/kb/828964
        void AssociatedObject_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Delete) {
                this.DeleteDelectedContent();
            }
        }

        void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) {
                this.LoadDataFromClipboard();
            }
        }


        private void DeleteDelectedContent() {
            //只是删除了表面,实质并没有删除
            //foreach (var cell in this.AssociatedObject.SelectedCells) {
            //    var ctrl = cell.Column.GetCellContent(cell.Item);
            //    if (ctrl is TextBlock)
            //        ((TextBlock)ctrl).Text = "";
            //    if (ctrl is ComboBox)
            //        ((ComboBox)ctrl).SelectedIndex = 0;
            //}

            foreach (var cell in this.AssociatedObject.SelectedCells) {

                //row.IsNewItem 是什么意思?不是新行也有是 true 的.
                //var row = this.AssociatedObject.ItemContainerGenerator.ContainerFromItem(cell.Item) as DataGridRow;
                //if (row != null && row.IsNewItem)
                //    continue;

                if (cell.Column.ClipboardContentBinding == null || cell.Column.IsReadOnly)
                    continue;
                var propertyName = ((Binding)cell.Column.ClipboardContentBinding).Path.Path;
                PropertyInfo pi = cell.Item.GetType().GetProperty(propertyName);
                if (pi != null) {
                    pi.SetValue(cell.Item, null, null);
                }
            }
        }

        private void LoadDataFromClipboard() {
            var datas = ClipboardHelper.ParseClipboardData();
            var beginColumn = this.AssociatedObject.CurrentCell.Column.DisplayIndex;
            var beginRow = this.AssociatedObject.Items.IndexOf(this.AssociatedObject.CurrentItem);

            if (this.AssociatedObject.SelectedCells.Count == 1) {
                for (var i = beginRow; i < datas.Count + beginRow; i++) {
                    var data = datas[i - beginRow];
                    this.AssociatedObject.CurrentItem = this.AssociatedObject.Items[i];
                    for (var j = beginColumn; j < this.AssociatedObject.Columns.Count && j < data.Length + beginColumn; j++) {
                        var column = this.AssociatedObject.Columns[j];
                        column.OnPastingCellClipboardContent(this.AssociatedObject.CurrentItem, data[j - beginColumn]);
                    }
                }
            } else {
                foreach (var cell in this.AssociatedObject.SelectedCells) {
                    cell.Column.OnPastingCellClipboardContent(cell.Item, datas[0][0]);
                }
            }
        }

    }
}
