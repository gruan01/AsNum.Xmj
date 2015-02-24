using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AsNum.WPF.Controls {
    internal class InnerStyleDialog : ContentControl {

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(InnerStyleDialog));
        public static readonly DependencyProperty SysBtnsProperty = DependencyProperty.Register("SysBtns", typeof(SysBtns), typeof(InnerStyleDialog), new PropertyMetadata(SysBtns.Close));
        public static readonly DependencyProperty ShowMaskProperty = DependencyProperty.Register("ShowMask", typeof(bool), typeof(InnerStyleDialog));
        public static readonly DependencyProperty DWidthProperty = DependencyProperty.Register("DWidth", typeof(double), typeof(InnerStyleDialog));
        public static readonly DependencyProperty DHeightProperty = DependencyProperty.Register("DHeight", typeof(double), typeof(InnerStyleDialog));

        public string Title {
            get {
                return (string)this.GetValue(TitleProperty);
            }
            set {
                this.SetValue(TitleProperty, value);
            }
        }

        public SysBtns SysBtns {
            get {
                return (SysBtns)this.GetValue(SysBtnsProperty);
            }
            set {
                this.SetValue(SysBtnsProperty, value);
            }
        }

        public double DWidth {
            get {
                return (double)this.GetValue(DWidthProperty);
            }
            set {
                this.SetValue(DWidthProperty, value);
            }
        }

        public double DHeight {
            get {
                return (double)this.GetValue(DHeightProperty);
            }
            set {
                this.SetValue(DHeightProperty, value);
            }
        }

        public bool ShowMask {
            get {
                return (bool)this.GetValue(ShowMaskProperty);
            }
            set {
                this.SetValue(ShowMaskProperty, value);
            }
        }

        static InnerStyleDialog() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InnerStyleDialog), new FrameworkPropertyMetadata(typeof(InnerStyleDialog)));
        }

        public InnerStyleDialog() {
            var b = new CommandBinding(DialogCommands.Close, CloseExecute);
            this.CommandBindings.Add(b);
            this.Focusable = true;
            this.KeyDown += StyleDialog_KeyUp;
            this.PreviewKeyUp += InnerStyleDialog_PreviewKeyUp;
        }

        void InnerStyleDialog_PreviewKeyUp(object sender, KeyEventArgs e) {

        }

        void StyleDialog_KeyUp(object sender, KeyEventArgs e) {
            if ((this.SysBtns & Controls.SysBtns.Close) == Controls.SysBtns.Close && e.Key == Key.Escape) {
                DialogCommands.Close.Execute(null, this);
            }
        }

        private void CloseExecute(object sender, ExecutedRoutedEventArgs e) {
            var adorner = (StyleDialogAdorner)VisualTreeHelper.GetParent(this);
            DialogCommands.Close.Execute(null, adorner.AdornedElement);
        }
        public override void OnApplyTemplate() {
            var mask = this.Template.FindName("MASK", this) as Border;
            mask.Visibility = this.ShowMask ? Visibility.Visible : Visibility.Collapsed;

            var btn = this.Template.FindName("BTNMIN", this) as Button;
            btn.Visibility = (this.SysBtns & Controls.SysBtns.Minimize) == Controls.SysBtns.Minimize ? Visibility.Visible : System.Windows.Visibility.Collapsed;

            btn = this.Template.FindName("BTNMAX", this) as Button;
            btn.Visibility = (this.SysBtns & Controls.SysBtns.Maximize) == Controls.SysBtns.Maximize ? Visibility.Visible : System.Windows.Visibility.Collapsed;

            btn = this.Template.FindName("BTNCLOSE", this) as Button;
            btn.Visibility = (this.SysBtns & Controls.SysBtns.Close) == Controls.SysBtns.Close ? Visibility.Visible : System.Windows.Visibility.Collapsed;

            base.OnApplyTemplate();
        }

    }
}
