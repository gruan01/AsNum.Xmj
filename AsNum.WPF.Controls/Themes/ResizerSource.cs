using System.Windows;
using System.Windows.Input;

namespace AsNum.WPF.Controls.Themes {
    public partial class ResizerSource {

        private void PART_Grip_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 1) {
                (sender as FrameworkElement).CaptureMouse();
                Resizer.StartResizeCommand.Execute(sender as FrameworkElement, sender as FrameworkElement);
                e.Handled = true;
            }
        }

        private void PART_Grip_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            FrameworkElement resizeGrip = sender as FrameworkElement;
            //Debug.Assert(resizeGrip != null);

            if (resizeGrip.IsMouseCaptured) {
                Resizer.EndResizeCommand.Execute(null, sender as FrameworkElement);
                resizeGrip.ReleaseMouseCapture();
                e.Handled = true;
            }
        }

        private void PART_Grip_MouseMove(object sender, MouseEventArgs e) {
            if ((sender as FrameworkElement).IsMouseCaptured) {
                Resizer.UpdateSizeCommand.Execute(null, sender as FrameworkElement);
                e.Handled = true;
            }
        }

        private void PART_Grip_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                Resizer.AutoSizeCommand.Execute(null, sender as FrameworkElement);
                e.Handled = true;
            }
        }

    }
}
