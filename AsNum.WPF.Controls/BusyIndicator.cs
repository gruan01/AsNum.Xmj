using System.Windows;
using System.Windows.Controls;

namespace AsNum.WPF.Controls {

    [TemplatePart(Name = "CNT", Type = typeof(Control))]
    [TemplatePart(Name = "MASK", Type = typeof(Border))]
    public class BusyIndicator : ContentControl {

        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(BusyIndicator), new PropertyMetadata("Waiting..."));
        public static DependencyProperty MaskTypeProperty = DependencyProperty.Register("MaskType", typeof(MaskTypes), typeof(BusyIndicator), new PropertyMetadata(MaskTypes.Adorned));
        public static DependencyProperty ContentControlTemplateProperty = DependencyProperty.Register("ContentControlTemplate", typeof(ControlTemplate), typeof(BusyIndicator));

        public string Text {
            get {
                return (string)this.GetValue(TextProperty);
            }
            set {
                this.SetValue(TextProperty, value);
            }
        }

        public MaskTypes MaskType {
            get {
                return (MaskTypes)this.GetValue(MaskTypeProperty);
            }
            set {
                this.SetValue(MaskTypeProperty, value);
            }
        }

        public ControlTemplate ContentControlTemplate {
            get {
                return (ControlTemplate)this.GetValue(ContentControlTemplateProperty);
            }
            set {
                this.SetValue(ContentControlTemplateProperty, value);
            }
        }

        static BusyIndicator() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        }

        public BusyIndicator() {
            this.DataContext = this;
        }
        public override void OnApplyTemplate() {
            var mask = this.Template.FindName("MASK", this) as Border;
            mask.Visibility = this.MaskType != MaskTypes.None ? Visibility.Visible : Visibility.Collapsed;
            if (this.ContentControlTemplate != null) {
                var tp = this.Template.FindName("CNT", this) as Control;
                tp.Template = this.ContentControlTemplate;
            }
        }
    }
}
