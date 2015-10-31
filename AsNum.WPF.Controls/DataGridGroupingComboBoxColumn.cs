using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AsNum.WPF.Controls {
    public class DataGridGroupingComboBoxColumn : DataGridComboBoxColumn {

        private ObservableCollection<GroupStyle> _groupStyle = new ObservableCollection<GroupStyle>();

        public static DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(DataGridGroupingComboBoxColumn), new PropertyMetadata(false));
        public bool IsEditable {
            get { return (bool)this.GetValue(IsEditableProperty); }
            set { this.SetValue(IsEditableProperty, value); }
        }

        public ObservableCollection<GroupStyle> GroupStyle {
            get { return _groupStyle; }
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem) {
            ComboBox objBox;

            objBox = (ComboBox)base.GenerateEditingElement(cell, dataItem);
            objBox.IsEditable = this.IsEditable;

            foreach (GroupStyle group in _groupStyle) {
                objBox.GroupStyle.Add(group);
            }

            return objBox;
        }

    }
}
