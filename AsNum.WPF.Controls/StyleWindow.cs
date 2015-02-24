using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AsNum.WPF.Controls {
    public class StyleWindow : Window {

        public static DependencyProperty TitleAdditionalContentProperty = DependencyProperty.Register("TitleAdditionalContent", typeof(object), typeof(StyleWindow));

        public object TitleAdditionalContent {
            get {
                return (object)this.GetValue(TitleAdditionalContentProperty);
            }
            set {
                this.SetValue(TitleAdditionalContentProperty, value);
            }
        }

        static StyleWindow() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StyleWindow), new FrameworkPropertyMetadata(typeof(StyleWindow)));
        }

        public StyleWindow()
            : base() {
            //绑定命令,配合自定义的最大化／最小化，关闭按钮
            var showSysMenu = new CommandBinding(SystemCommands.ShowSystemMenuCommand, OnShowSystemMenuCommand);
            this.CommandBindings.Add(showSysMenu);

            var closeWindow = new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindowCommand);
            this.CommandBindings.Add(closeWindow);

            var maxWindow = new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindowCommand);
            var restoreWindow = new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindowCommand);
            this.CommandBindings.Add(maxWindow);
            this.CommandBindings.Add(restoreWindow);

            var minWindow = new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindowCommand);
            this.CommandBindings.Add(minWindow);

            FullScreenManager.RepairWpfWindowFullScreenBehavior(this);
        }

        private void OnMinimizeWindowCommand(object sender, ExecutedRoutedEventArgs e) {
            var w = Window.GetWindow(this);
            SystemCommands.MinimizeWindow(this);
        }

        private void MaxOrRestoreWindow() {
            var w = Window.GetWindow(this);
            //Action<Window> act = w.WindowState == System.Windows.WindowState.Maximized ? (w)=> SystemCommands.RestoreWindow(w) : (w)=>SystemCommands.MaximizeWindow(w);
            if (w.WindowState == System.Windows.WindowState.Maximized)
                SystemCommands.RestoreWindow(w);
            else {
                SystemCommands.MaximizeWindow(w);
            }
        }

        private void OnRestoreWindowCommand(object sender, ExecutedRoutedEventArgs e) {
            this.MaxOrRestoreWindow();
        }

        private void OnMaximizeWindowCommand(object sender, ExecutedRoutedEventArgs e) {
            this.MaxOrRestoreWindow();
        }

        private void OnCloseWindowCommand(object sender, ExecutedRoutedEventArgs e) {
            var w = Window.GetWindow(this);
            SystemCommands.CloseWindow(w);
        }

        private void OnShowSystemMenuCommand(object sender, ExecutedRoutedEventArgs e) {
            Window w = Window.GetWindow(this);
            Point p = new Point(w.Left + 24, w.Top + 24);

            SystemCommands.ShowSystemMenu(w, p);
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            if (this.TitleAdditionalContent != null) {
                var ta = (ContentControl)this.Template.FindName("TitleAdditional", this);
                ta.Content = this.TitleAdditionalContent;
            }
        }
    }
}
