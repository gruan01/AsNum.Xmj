﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace AsNum.Common.WPF.Behaviors {
    public class DataGridMutiSelectBehavior : Behavior<DataGrid> {

        #region SelectedItems Attached Property
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems",
            typeof(ObservableCollection<object>),
            typeof(DataGridMutiSelectBehavior),
            new PropertyMetadata(new ObservableCollection<object>(), PropertyChangedCallback));

        #endregion

        #region private
        private bool _selectionChangedInProgress; // Flag to avoid infinite loop if same viewmodel is shared by multiple controls
        #endregion

        public DataGridMutiSelectBehavior() {
            SelectedItems = new ObservableCollection<object>();
        }

        public ObservableCollection<object> SelectedItems {
            get {
                return (ObservableCollection<object>)GetValue(SelectedItemsProperty);
            }
            set {
                SetValue(SelectedItemsProperty, value);
            }
        }

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.SelectionChanged += OnSelectionChanged;
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= OnSelectionChanged;
        }

        private static void PropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs args) {
            NotifyCollectionChangedEventHandler handler = (s, e) => SelectedItemsChanged(sender, e);
            if (args.OldValue is ObservableCollection<object>) {
                (args.OldValue as ObservableCollection<object>).CollectionChanged -= handler;
            }

            if (args.NewValue is ObservableCollection<object>) {
                (args.NewValue as ObservableCollection<object>).CollectionChanged += handler;
            }
        }

        private static void SelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (sender is DataGridMutiSelectBehavior) {
                var listViewBase = (sender as DataGridMutiSelectBehavior).AssociatedObject;

                var listSelectedItems = listViewBase.SelectedItems;
                if (e.OldItems != null) {
                    foreach (var item in e.OldItems) {
                        if (listSelectedItems.Contains(item)) {
                            listSelectedItems.Remove(item);
                        }
                    }
                }

                if (e.NewItems != null) {
                    foreach (var item in e.NewItems) {
                        if (!listSelectedItems.Contains(item)) {
                            listSelectedItems.Add(item);
                        }
                    }
                }
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (_selectionChangedInProgress)
                return;

            if (this.SelectedItems == null)
                this.SelectedItems = new ObservableCollection<object>();

            _selectionChangedInProgress = true;
            foreach (var item in e.RemovedItems) {
                if (SelectedItems.Contains(item)) {
                    SelectedItems.Remove(item);
                }
            }

            foreach (var item in e.AddedItems) {
                if (!SelectedItems.Contains(item)) {
                    SelectedItems.Add(item);
                }
            }
            _selectionChangedInProgress = false;
        }

    }
}
