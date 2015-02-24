using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AsNum.WPF.Controls {
    public class Summary : ItemsControl {


        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Summary));

        public static DependencyProperty ItemTitlePathProperty = DependencyProperty.Register("ItemTitlePath", typeof(string), typeof(Summary));

        public static DependencyProperty ItemValuePathProperty = DependencyProperty.Register("ItemValuePath", typeof(string), typeof(Summary));

        public static DependencyProperty ItemDetailTemplateProperty = DependencyProperty.Register("ItemDetailTemplate", typeof(DataTemplate), typeof(Summary));

        public static DependencyProperty ItemTitleTemplateProperty = DependencyProperty.Register("ItemTitleTemplate", typeof(DataTemplate), typeof(Summary));

        public static DependencyProperty ItemValueTemplateProperty = DependencyProperty.Register("ItemValueTemplate", typeof(DataTemplate), typeof(Summary));

        public string Title {
            get {
                return (string)this.GetValue(TitleProperty);
            }
            set {
                this.SetValue(TitleProperty, value);
            }
        }

        public string ItemTitlePath {
            get {
                return (string)this.GetValue(ItemTitlePathProperty);
            }
            set {
                this.SetValue(ItemTitlePathProperty, value);
            }
        }

        public string ItemValuePath {
            get {
                return (string)this.GetValue(ItemValuePathProperty);
            }
            set {
                this.SetValue(ItemValuePathProperty, value);
            }
        }

        public DataTemplate ItemDetailTemplate {
            get {
                return (DataTemplate)this.GetValue(ItemDetailTemplateProperty);
            }
            set {
                this.SetValue(ItemDetailTemplateProperty, value);
            }
        }

        public DataTemplate ItemTitleTemplate {
            get {
                return (DataTemplate)this.GetValue(ItemTitleTemplateProperty);
            }
            set {
                this.SetValue(ItemTitleTemplateProperty, value);
            }
        }

        public DataTemplate ItemValueTemplate {
            get {
                return (DataTemplate)this.GetValue(ItemValueTemplateProperty);
            }
            set {
                this.SetValue(ItemValueTemplateProperty, value);
            }
        }

        static Summary() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Summary), new FrameworkPropertyMetadata(typeof(Summary)));
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            var ele = element as SummaryItem;

            {
                Binding binding = new Binding();
                ele.SetBinding(SummaryItem.ItemProperty, binding);
            }

            if (!string.IsNullOrEmpty(this.ItemTitlePath)) {
                Binding binding = new Binding(this.ItemTitlePath);
                ele.SetBinding(SummaryItem.TitleProperty, binding);
            }

            if (!string.IsNullOrEmpty(this.ItemValuePath)) {
                Binding binding = new Binding(this.ItemValuePath);
                ele.SetBinding(SummaryItem.ValueProperty, binding);
            }

            ele.DetailTemplate = this.ItemDetailTemplate;
            ele.TitleTemplate = this.ItemTitleTemplate;
            ele.ValueTemplate = this.ItemValueTemplate;
        }

        protected override DependencyObject GetContainerForItemOverride() {
            return new SummaryItem();
        }

    }
}
