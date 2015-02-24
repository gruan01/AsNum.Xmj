using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System.ComponentModel.Composition;

namespace AsNum.Xmj.OrderManager.ViewModels {
    [Export(typeof(IOrderDealSubView)), PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class ReceiverViewModel : VMScreenBase, IOrderDealSubView {

        public override string Title {
            get {
                return "收件信息";
            }
        }
        //public string OrderNO {
        //    get;
        //    set;
        //}

        public Receiver Receiver {
            get;
            set;
        }

        public AdjReceiver Receiver2 {
            get;
            set;
        }

        private bool canReceiver2 = false;
        public bool CanReceiver2 {
            get {
                return this.canReceiver2;
            }
            set {
                this.canReceiver2 = value;
                if (value && this.Receiver2 == null) {
                    //DynamicCopy.CopyProperties(this.Receiver as ReceiverBase, this.Receiver2);
                    this.Receiver2 = new AdjReceiver() {
                        Address = this.Receiver.Address,
                        City = this.Receiver.City,
                        //Country = this.Receiver.Country,
                        CountryCode = this.Receiver.CountryCode,
                        Mobi = this.Receiver.Mobi,
                        Name = this.Receiver.Name,
                        OrderNO = this.Receiver.OrderNO,
                        Phone = this.Receiver.Phone,
                        PhoneArea = this.Receiver.PhoneArea,
                        Province = this.Receiver.Province,
                        ZipCode = this.Receiver.ZipCode
                    };
                } else {
                    //this.Receiver2 = null;
                }
                this.NotifyOfPropertyChange("Receiver2");
            }
        }

        private Order Order;

        public IReceiver ReceiverBiz { get; set; }


        public ReceiverViewModel() {
            this.ReceiverBiz = GlobalData.MefContainer.GetExportedValue<IReceiver>();
        }

        public void Build(Order order) {
            this.Order = order;
            this.Receiver = order.OrgReceiver;
            this.Receiver2 = order.AdjReceiver;
            this.CanReceiver2 = this.Receiver2 != null;

            this.NotifyOfPropertyChange(() => this.CanReceiver2);
            this.NotifyOfPropertyChange(() => this.Receiver);
            this.NotifyOfPropertyChange(() => this.Receiver2);
        }

        public void Save() {
            if (this.CanReceiver2) {
                this.Receiver2 = this.ReceiverBiz.Save(this.Receiver2);
            } else {
                this.ReceiverBiz.RemoveAdjReceiver(this.Order.OrderNO);
            }

            this.NotifyOfPropertyChange("Receiver2");
        }

    }
}
