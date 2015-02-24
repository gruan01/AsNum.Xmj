using System.Windows;
using System.Windows.Controls;

namespace AsNum.WPF.Controls {

    [TemplatePart(Name = "PART_Value", Type=typeof(ContentControl))]
    [TemplatePart(Name = "PART_Title", Type = typeof(ContentControl))]
    [TemplatePart(Name = "PART_Detail", Type = typeof(ContentControl))]
    public class SummaryItem : Control {

        public static DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(object), typeof(Summary));
        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(SummaryItem));
        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(SummaryItem));
        public static DependencyProperty DetailTemplateProperty = DependencyProperty.Register("DetailTemplate", typeof(DataTemplate), typeof(SummaryItem));
        public static DependencyProperty TitleTemplateProperty = DependencyProperty.Register("TitleTemplate", typeof(DataTemplate), typeof(SummaryItem));
        public static DependencyProperty ValueTemplateProperty = DependencyProperty.Register("ValueTemplate", typeof(DataTemplate), typeof(SummaryItem));

        public object Item {
            get {
                return (object)this.GetValue(ItemProperty);
            }
            set {
                this.SetValue(ItemProperty, value);
            }
        }
        public string Title {
            get {
                return (string)this.GetValue(TitleProperty);
            }
            set {
                this.SetValue(TitleProperty, value);
            }
        }

        public object Value {
            get {
                return this.GetValue(ValueProperty);
            }
            set {
                this.SetValue(ValueProperty, value);
            }
        }

        public DataTemplate DetailTemplate {
            get {
                return (DataTemplate)this.GetValue(DetailTemplateProperty);
            }
            set {
                this.SetValue(DetailTemplateProperty, value);
            }
        }

        public DataTemplate TitleTemplate {
            get {
                return (DataTemplate)this.GetValue(TitleTemplateProperty);
            }
            set {
                this.SetValue(TitleTemplateProperty, value);
            }
        }

        public DataTemplate ValueTemplate {
            get {
                return (DataTemplate)this.GetValue(ValueTemplateProperty);
            }
            set {
                this.SetValue(ValueTemplateProperty, value);
            }
        }

        static SummaryItem() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SummaryItem), new FrameworkPropertyMetadata(typeof(SummaryItem)));
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            var pv = (ContentControl)this.Template.FindName("PART_Value", this);
            if (pv != null && this.ValueTemplate != null) {
                pv.Content = this.Item;
                pv.ContentTemplate = this.ValueTemplate;
            }

            var pt = (ContentControl)this.Template.FindName("PART_Title", this);
            if (pt != null && this.TitleTemplate != null) {
                pt.Content = this.Item;
                pt.ContentTemplate = this.TitleTemplate;
            }

            var pd = (ContentControl)this.Template.FindName("PART_Detail", this);
            if (pd != null && this.DetailTemplate != null) {
                pd.Content = this.Item;
            }
        }
    }
}
