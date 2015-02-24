using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices;
using ZXing;
using System.Windows.Media;

namespace BarCodeScanner.Views {
    public partial class ScannerPage : PhoneApplicationPage {

        private PhotoCamera Camera;
        private BarcodeReader Reader;
        private Rect RR;
        private int CameraW, CameraH;

        public ScannerPage() {
            InitializeComponent();
            this.Loaded += ScannerPage_Loaded;
        }

        void ScannerPage_Loaded(object sender, RoutedEventArgs e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                var root = GetRoot(this.rect);
                var rp = this.rect.TransformToVisual(root as UIElement).Transform(new Point(0, 0));
                this.RR = new Rect(rp, new Size(this.rect.ActualWidth, this.rect.ActualHeight));
            });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e) {
            this.Camera.AutoFocusCompleted -= Camera_AutoFocusCompleted;
            this.Camera.Dispose();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            this.Camera = new PhotoCamera(CameraType.Primary);
            this.vb.SetSource(this.Camera);
            this.Camera.AutoFocusCompleted += Camera_AutoFocusCompleted;
            this.Camera.Initialized += Camera_Initialized;
            CameraButtons.ShutterKeyHalfPressed += CameraButtons_ShutterKeyHalfPressed;

            this.Reader = new BarcodeReader();
            this.Reader.Options.TryHarder = true;
            this.Reader.AutoRotate = true;
            this.Reader.TryInverted = true;
        }

        void CameraButtons_ShutterKeyHalfPressed(object sender, EventArgs e) {
            this.Camera.Focus();
        }

        void Camera_Initialized(object sender, CameraOperationCompletedEventArgs e) {
            this.Camera.Resolution = this.Camera.AvailableResolutions.First();
            this.CameraW = Convert.ToInt32(this.Camera.Resolution.Width);
            this.CameraH = Convert.ToInt32(this.Camera.Resolution.Height);
        }

        void Camera_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e) {
            var luminance = new PhotoCameraLuminanceSource(this.CameraW, this.CameraH);
            this.Camera.GetPreviewBufferY(luminance.PreviewBufferY);
            luminance.Cutout((int)this.RR.Left, (int)this.RR.Top, (int)this.RR.Width, (int)this.RR.Height);
            var result = this.Reader.Decode(luminance);

            Deployment.Current.Dispatcher.BeginInvoke(() => {
                if (result != null)
                    this.Tip.Text = result.Text;
                else
                    this.Tip.Text = "未识别" + DateTime.Now.ToString("HH:mm:ss");
            });
        }

        private DependencyObject GetRoot(DependencyObject o) {
            DependencyObject parent;
            while ((parent = VisualTreeHelper.GetParent(o)) != null) {
                return GetRoot(parent);
            }
            return o;
        }

        private void LayoutRoot_Tap(object sender, System.Windows.Input.GestureEventArgs e) {
            if (this.Camera != null) {
                this.Camera.Focus();
            }
        }
    }
}