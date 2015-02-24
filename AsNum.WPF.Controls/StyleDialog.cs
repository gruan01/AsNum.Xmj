using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace AsNum.WPF.Controls {

    [Flags]
    public enum SysBtns : byte {
        Minimize = 1,
        Maximize = 2,
        Close = 4,
        Help = 8
    }

    internal class DialogCommands {
        public static RoutedCommand Close = new RoutedCommand();
    }

    public class StyleDialog : ContentControl {

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(StyleDialog), new PropertyMetadata("", Changed));
        public static readonly DependencyProperty SysBtnsProperty = DependencyProperty.Register("SysBtns", typeof(SysBtns), typeof(StyleDialog), new PropertyMetadata(SysBtns.Close, Changed));
        public static readonly DependencyProperty ShowMaskProperty = DependencyProperty.Register("ShowMask", typeof(bool), typeof(StyleDialog), new PropertyMetadata(true, Changed));
        public static readonly DependencyProperty ShowProperty = DependencyProperty.Register("Show", typeof(bool), typeof(StyleDialog), new PropertyMetadata(false, Changed));
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

        public bool ShowMask {
            get {
                return (bool)this.GetValue(ShowMaskProperty);
            }
            set {
                this.SetValue(ShowMaskProperty, value);
            }
        }

        public bool Show {
            get {
                return (bool)this.GetValue(ShowProperty);
            }
            set {
                this.SetValue(ShowProperty, value);
            }
        }

        private StyleDialogAdorner Adorner;

        static StyleDialog() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StyleDialog), new FrameworkPropertyMetadata(typeof(StyleDialog)));
            ContentProperty.OverrideMetadata(typeof(StyleDialog), new FrameworkPropertyMetadata(new PropertyChangedCallback(Changed)));
            WidthProperty.OverrideMetadata(typeof(StyleDialog), new FrameworkPropertyMetadata(new PropertyChangedCallback(Changed)));
            HeightProperty.OverrideMetadata(typeof(StyleDialog), new FrameworkPropertyMetadata(new PropertyChangedCallback(Changed)));
        }


        public StyleDialog() {
            this.CommandBindings.Add(new CommandBinding(DialogCommands.Close, CloseExecute, CanClose));
            this.Loaded += StyleDialog_Loaded;
        }

        void StyleDialog_Loaded(object sender, RoutedEventArgs e) {
            var window = this.GetParentWindow();
            //window.StateChanged += StyleDialog_StateChanged;
            window.SizeChanged += window_SizeChanged;
        }

        void window_SizeChanged(object sender, SizeChangedEventArgs e) {
            if (this.Adorner != null) {
                //如果不在 SizeChanged 做这些处理，当窗口最大化时，MeasureOverride 取到的窗口大小是未大化之前的值，反之相反。
                this.Adorner.InvalidateMeasure();
                this.Adorner.InvalidateArrange();
                this.Adorner.UpdateLayout();
            }
        }

        //void StyleDialog_StateChanged(object sender, EventArgs e) {

        //}

        private void CloseExecute(object sender, ExecutedRoutedEventArgs e) {
            this.Show = false;
        }

        private void CanClose(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((StyleDialog)d).Update();
        }

        public void Update() {

            var layer = AdornerLayer.GetAdornerLayer(this);
            if (layer == null) {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action<FrameworkElement>(o => Update()), DispatcherPriority.Loaded, this);
                return;
            }

            if (this.Show) {
                if (this.Adorner == null) {
                    this.Adorner = new StyleDialogAdorner(this);
                    //layer.Add(this.Adorner);
                }
                if (this.Adorner.Parent != layer)
                    layer.Add(this.Adorner);

                //layer.IsEnabledChanged
                //layer.Unloaded += layer_Unloaded;

                this.Adorner.Dialog.Title = this.Title;
                this.Adorner.Dialog.ShowMask = this.ShowMask;
                this.Adorner.Dialog.SysBtns = this.SysBtns;

                this.Adorner.Dialog.DWidth = this.Width;
                this.Adorner.Dialog.DHeight = this.Height;

                this.Adorner.Dialog.Content = this.Content;
            } else {
                if (this.Adorner != null) {
                    layer.Remove(this.Adorner);
                    //this.Adorner = null;
                }
            }
        }

        void layer_Unloaded(object sender, RoutedEventArgs e) {

        }
    }
}
