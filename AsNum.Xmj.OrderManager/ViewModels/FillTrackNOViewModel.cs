using AsNum.WPF.Controls;
using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;
using ZXing;
using AE = AsNum.Xmj.API.Entity;

namespace AsNum.Xmj.OrderManager.ViewModels {
    public class FillTrackNOViewModel : VMScreenBase, IScreenShoterObserver {
        public override string Title {
            get {
                return "填写发货通知";
            }
        }


        public string OrderNO {
            get;
            set;
        }

        private Order Order;


        public List<AE.LogisticsTypes> DeliveryTypes {
            get;
            set;
        }

        public AE.LogisticsTypes SelectedDeliveryType {
            get;
            set;
        }

        public string TrackWebSite {
            get;
            set;
        }

        public string TrackNO {
            get;
            set;
        }

        public bool IsFullShiped {
            get;
            set;
        }

        public string Note {
            get;
            set;
        }

        public bool IsBusy {
            get;
            set;
        }

        public string BusyString {
            get;
            set;
        }

        public IScreenShoter Shoter;

        public Action<string> OnSuccess;

        public IOrder OrderBiz { get; set; }

        public FillTrackNOViewModel(string orderNO) {
            this.OrderBiz = GlobalData.MefContainer.GetExportedValue<IOrder>();
            this.DeliveryTypes = Enum.GetValues(typeof(AE.LogisticsTypes)).Cast<AE.LogisticsTypes>().ToList();
            this.SelectedDeliveryType = AE.LogisticsTypes.CPAM;

            this.OrderNO = orderNO;
            this.Order = this.OrderBiz.GetOrder(orderNO);

            this.Shoter = GlobalData.GetInstance<IScreenShoter>();
        }

        public void Confirm() {
            this.IsBusy = true;
            this.BusyString = "请稍候...";
            this.NotifyOfPropertyChange(() => this.IsBusy);
            this.NotifyOfPropertyChange(() => this.BusyString);
            DispatcherHelper.DoEvents();

            var success = false;
            var s = new AccountSetting();
            var account = s.Value.FirstOrDefault(a => a.User.Equals(this.Order.Account, StringComparison.OrdinalIgnoreCase));
            if (account != null) {
                var method = new OrderShipment() {
                    OrderNO = this.OrderNO,
                    TrackingNO = this.TrackNO,
                    TrackingWebSite = this.TrackWebSite,
                    SendType = this.IsFullShiped ? AE.ShipmentSendTypes.Full : AE.ShipmentSendTypes.Part,
                    Description = this.Note,
                    LogisticsType = this.SelectedDeliveryType
                };

                var api = new APIClient(account.User, account.Pwd);
                var o = api.Execute(method);
                //var o = new AE.NormalResult() {
                //    Success = true
                //};
                success = o.Success;
                this.BusyString = success ? "成功" : "失败";
            } else {
                this.BusyString = "未取到账户信息";
            }

            this.NotifyOfPropertyChange(() => this.BusyString);
            DispatcherHelper.DoEvents();
            Thread.Sleep(2000);

            this.IsBusy = false;
            this.NotifyOfPropertyChange(() => this.IsBusy);

            if (success && this.OnSuccess != null) {
                this.OnSuccess(this.OrderNO);
            }
        }

        //public void ReadFromImage() {
        //    if (this.Shoter != null) {
        //        this.Shoter.Attach(this);
        //    }
        //}

        private bool readFromImage;
        public bool ReadFromImage {
            get {
                return this.readFromImage;
            }
            set {
                this.readFromImage = value;
                if (this.Shoter != null) {
                    if (value)
                        this.Shoter.Attach(this);
                    else
                        this.Shoter.Detach(this);
                }
            }
        }

        public void Update(Bitmap img) {
            img = ToGray(img);
            var reader = new BarcodeReader();
            reader.Options.TryHarder = true;
            reader.AutoRotate = true;
            reader.TryInverted = true;
            //reader.Options.PureBarcode = true;
            var result = reader.Decode(img);
            if (result != null) {
                this.TrackNO = result.Text;
                this.NotifyOfPropertyChange(() => this.TrackNO);
            } else {
                MessageBox.Show("未识别,请重试");
            }
        }

        private static Bitmap ToGray(Bitmap bmp) {
            for (int i = 0; i < bmp.Width; i++) {
                for (int j = 0; j < bmp.Height; j++) {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    //利用公式计算灰度值
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }
    }
}
