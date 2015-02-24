using System.Windows.Threading;

namespace AsNum.WPF.Controls {
    public static class DispatcherHelper {

        public static void DoEvents() {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        private static object ExitFrame(object f) {
            ((DispatcherFrame)f).Continue = false;

            return null;
        }

    }
}
