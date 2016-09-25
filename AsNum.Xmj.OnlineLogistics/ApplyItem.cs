using AsNum.Xmj.API.Entity;
using Caliburn.Micro;
using System.Collections.Generic;

namespace AsNum.Xmj.OnlineLogistics {
    public class ApplyItem : PropertyChangedBase {

        private string orderNO { get; set; }
        public string OrderNO {
            get { return this.orderNO; }
            set {
                if (value != null)
                    this.orderNO = value.Trim();
                this.NotifyOfPropertyChange(() => this.OrderNO);
            }
        }

        public AsNum.Xmj.Entity.OrderStatus Status { get; set; }

        public string OrderNote { get; set; }


        private SupportOnlineLogisticsService _service = null;
        public SupportOnlineLogisticsService Service {
            get {
                return this._service;
            }
            set {
                this._service = value;
            }
        }

        public string LocalLogisticCompany { get; set; }

        public string LocalTrackNO { get; set; }

        public OnlineLogisticsContacts Receiver { get; set; }
        public OnlineLogisticsContacts Sender { get; set; }

        private bool needPickup = true;
        public bool NeedPickup {
            get {
                return this.needPickup;
            }
            set {
                this.needPickup = value;
            }
        }
        public OnlineLogisticsContacts Pickup { get; set; }


        /// <summary>
        /// 货物未送达时退回
        /// </summary>
        public bool WhenUndeliverReturn { get; set; } = true;

        /// <summary>
        /// 退货地址
        /// </summary>
        public OnlineLogisticsContacts Refund { get; set; }


        public BindableCollection<OnlineLogisticsDeclareInfo> Declares { get; set; }

        public string Account { get; set; }

        public List<SupportOnlineLogisticsService> Services { get; set; }

        public List<LocalLogisticsCompany> LogisticsCompanies { get; set; }

        public List<OnlineLogisticsContacts> Pickups { get; set; }

        public List<OnlineLogisticsContacts> Senders { get; set; }

        public List<OnlineLogisticsContacts> Refunds { get; set; }

        public string LogisticType { get; set; }

        private bool isSummary = true;
        public bool IsSummary {
            get {
                return this.isSummary;
            }
            set {
                this.isSummary = value;
                this.NotifyOfPropertyChange(() => this.IsSummary);
            }
        }

        /// <summary>
        /// 已经申请过的线上物流单号
        /// </summary>
        public List<OnlineLogisticsInfo> ExistsLogisticInfos { get; set; }

        /// <summary>
        /// 是否已申请单号
        /// </summary>
        public bool Created { get; set; }

        /// <summary>
        /// API 错误信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 已申请
        /// </summary>
        public bool Applied {
            get {
                return this.Created || (this.ExistsLogisticInfos != null && this.ExistsLogisticInfos.Count > 0);
            }
        }
    }
}
