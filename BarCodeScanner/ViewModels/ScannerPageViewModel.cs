using Caliburn.Micro;
using Microsoft.Devices;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ZXing;

namespace BarCodeScanner.ViewModels {
    public class ScannerPageViewModel : Screen {

        private INavigationService NS;

        private PhotoCamera Camera;
        private BarcodeReader Reader;
        private Rect RR;
        private int CameraW, CameraH;
        private bool CameraInitialized;
        private int RootWidth, RootHeight;

        public VideoBrush VBrush {
            get;
            set;
        }

        public string Tip {
            get;
            set;
        }

        public Thickness RangeMargin {
            get;
            set;
        }

        public int RangeWidth {
            get;
            set;
        }

        public int RangeHeight {
            get;
            set;
        }

        public bool IsScanning {
            get;
            set;
        }

        public Visibility ScanBtnVisibility {
            get;
            set;
        }

        public ScannerPageViewModel(INavigationService ns) {
            this.NS = ns;

            this.RangeMargin = new Thickness(0, 10, 0, 100);

            this.Camera = new PhotoCamera(CameraType.Primary);

            this.VBrush = new VideoBrush();
            this.VBrush.RelativeTransform = new CompositeTransform() {
                CenterX = 0.5,
                CenterY = 0.5,
                Rotation = 90
            };
            this.VBrush.SetSource(this.Camera);

            this.Camera.AutoFocusCompleted += Camera_AutoFocusCompleted;
            this.Camera.Initialized += Camera_Initialized;

            CameraButtons.ShutterKeyHalfPressed += CameraButtons_ShutterKeyHalfPressed;

            this.Reader = new BarcodeReader();
            this.Reader.Options.TryHarder = true;
            this.Reader.AutoRotate = true;
            this.Reader.TryInverted = true;

            this.RootWidth = (int)Application.Current.RootVisual.RenderSize.Width;
            this.RootHeight = (int)Application.Current.RootVisual.RenderSize.Height;

            this.RangeHeight = 200;
            this.RangeWidth = this.RootWidth;
            this.RangeMargin = new Thickness(0, 150, 0, 200);

            this.RR = new Rect(0, 150, this.RangeWidth, this.RangeHeight);
            this.Tip = "将要扫描的条形码对准绿色区域";
            this.ScanBtnVisibility = Visibility.Collapsed;
        }

        protected override void OnDeactivate(bool close) {
            base.OnDeactivate(close);

            this.Camera.AutoFocusCompleted -= Camera_AutoFocusCompleted;
            this.Camera.Dispose();
        }


        void CameraButtons_ShutterKeyHalfPressed(object sender, EventArgs e) {
            this.SetFocus();
        }

        void Camera_Initialized(object sender, CameraOperationCompletedEventArgs e) {
            this.Camera.Resolution = this.Camera.AvailableResolutions.First();
            this.Camera.FlashMode = FlashMode.Off;

            this.CameraW = Convert.ToInt32(this.Camera.Resolution.Width);
            this.CameraH = Convert.ToInt32(this.Camera.Resolution.Height);
            this.CameraInitialized = true;
            this.ScanBtnVisibility = Visibility.Visible;
            this.NotifyOfPropertyChange(() => this.ScanBtnVisibility);
        }

        void Camera_AutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e) {
            var luminance = new PhotoCameraLuminanceSource(this.CameraW, this.CameraH);
            this.Camera.GetPreviewBufferY(luminance.PreviewBufferY);
            luminance.Cutout((int)this.RR.Left, (int)this.RR.Top, (int)this.RR.Width, (int)this.RR.Height);
            var result = this.Reader.Decode(luminance);


            //this.Tip = result != null ? result.Text : DateTime.Now.ToString("不能识别 HH:mm:ss");
            if (result != null) {
                VibrateController.Default.Start(new TimeSpan(0, 0, 0, 0, 200));
                this.IsScanning = false;
                this.ScanBtnVisibility = Visibility.Visible;
                this.Tip = result.Text;
                this.NotifyOfPropertyChange(() => this.Tip);
                this.NotifyOfPropertyChange(() => this.IsScanning);
                this.NotifyOfPropertyChange(() => this.ScanBtnVisibility);
            } else {
                this.SetFocus();
            }
        }

        private DependencyObject GetRoot(DependencyObject o) {
            DependencyObject parent;
            while ((parent = VisualTreeHelper.GetParent(o)) != null) {
                return GetRoot(parent);
            }
            return o;
        }

        public void SetFocus() {
            this.IsScanning = true;
            this.ScanBtnVisibility = Visibility.Collapsed;
            this.Tip = "请尝试移动您的手机到不同的角度和距离";
            this.NotifyOfPropertyChange(() => this.Tip);
            this.NotifyOfPropertyChange(() => this.IsScanning);
            this.NotifyOfPropertyChange(() => this.ScanBtnVisibility);

            if (this.Camera != null && this.CameraInitialized && this.Camera.IsFocusSupported)
                if (this.Camera.IsFocusAtPointSupported) {
                    var x = (this.RR.X + this.RR.Width / 2) / this.RootWidth;
                    var y = (this.RR.Y + this.RR.Height / 2) / this.RootHeight;
                    try {
                        this.Camera.FocusAtPoint(x, y);
                    } catch {
                        this.Camera.Focus();
                    }
                } else
                    this.Camera.Focus();
        }

        public void Home() {
            NS.UriFor<HomePageViewModel>().Navigate();
        }
    }
}
