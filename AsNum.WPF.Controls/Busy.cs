using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;

namespace AsNum.WPF.Controls {

    public enum MaskTypes {
        None,
        Adorned,
        Window
    }
    public class Busy {

        #region Text
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(string), typeof(Busy), new PropertyMetadata(TextChanged));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Update((FrameworkElement)d);
        }

        public static void SetText(FrameworkElement target, string value) {
            target.SetValue(TextProperty, value);
        }

        public static string GetText(FrameworkElement target) {
            return (string)target.GetValue(TextProperty);
        }
        #endregion

        #region MaskType
        public static readonly DependencyProperty MaskTypeProperty = DependencyProperty.RegisterAttached("MaskType", typeof(MaskTypes), typeof(Busy), new PropertyMetadata(MaskTypes.Adorned, MaskTypeChanged));

        private static void MaskTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Update((FrameworkElement)d);
        }

        public static void SetMaskType(FrameworkElement target, MaskTypes type) {
            target.SetValue(MaskTypeProperty, type);
        }

        public static MaskTypes GetMaskType(FrameworkElement target) {
            return (MaskTypes)target.GetValue(MaskTypeProperty);
        }
        #endregion

        #region Show
        public static readonly DependencyProperty ShowProperty = DependencyProperty.RegisterAttached("Show", typeof(bool), typeof(Busy), new PropertyMetadata(false, ShowChanged));
        private static void ShowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            Update((FrameworkElement)d);
        }

        public static void SetShow(FrameworkElement target, bool value) {
            target.SetValue(ShowProperty, value);
        }

        public static bool GetShow(FrameworkElement target) {
            return (bool)target.GetValue(ShowProperty);
        }
        #endregion

        #region adorner
        public static readonly DependencyProperty AdornerProperty = DependencyProperty.RegisterAttached("Adorner", typeof(BusyIndicatorAdorner), typeof(Busy));

        public static void SetAdorner(FrameworkElement target, BusyIndicatorAdorner adorner) {
            target.SetValue(AdornerProperty, adorner);
        }

        public static BusyIndicatorAdorner GetAdorner(FrameworkElement target) {
            return (BusyIndicatorAdorner)target.GetValue(AdornerProperty);
        }

        #endregion

        #region
        public static readonly DependencyProperty ContentControlTemplateProperty = DependencyProperty.Register("ContentControlTemplate", typeof(ControlTemplate), typeof(Busy));

        public static ControlTemplate GetContentControlTemplate(FrameworkElement target) {
            return (ControlTemplate)target.GetValue(ContentControlTemplateProperty);
        }

        public static void SetContentControlTemplate(FrameworkElement target, ControlTemplate ctrl) {
            target.SetValue(ContentControlTemplateProperty, ctrl);
        }

        #endregion

        private static void Update(FrameworkElement target) {
            var layer = AdornerLayer.GetAdornerLayer(target);
            if (layer == null) {
                Dispatcher.CurrentDispatcher.BeginInvoke(new Action<FrameworkElement>(o => Update(o)), DispatcherPriority.Loaded, target);
                return;
            }

            var text = GetText(target);
            var show = GetShow(target);
            var maskType = GetMaskType(target);
            var template = GetContentControlTemplate(target);

            var adorner = GetAdorner(target);

            if (show) {
                if (adorner == null) {
                    adorner = new BusyIndicatorAdorner(target) {
                        MaskType = maskType,
                        ContentControlTemplate = template,
                        Text = text
                    };
                    layer.Add(adorner);
                    SetAdorner(target, adorner);
                } else {
                    adorner.MaskType = maskType;
                    adorner.ContentControlTemplate = template;
                    adorner.Text = text;
                }
                //adorner.Visibility = Visibility.Visible;
            } else {
                if (adorner != null) {
                    layer.Remove(adorner);
                    //如果不 Remove 并设置为 null, 在 使用AvalonDock的程序里，切换标签会使 adorner 的 Parent 丢失
                    //如果设置为 null ，会在再一次显示的时候，重建
                    //adorner.Visibility = Visibility.Collapsed;
                    SetAdorner(target, null);
                }
            }
        }
    }
}
