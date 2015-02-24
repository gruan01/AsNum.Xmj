using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    public class Order {

        /// <summary>
        /// 订单ID
        /// </summary>
        [Key, StringLength(20), Required]
        public string OrderNO {
            get;
            set;
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus Status {
            get;
            set;
        }


        [StringLength(20), Required]
        public string BuyerID {
            get;
            set;
        }

        [ForeignKey("BuyerID")]
        public Buyer Buyer {
            get;
            set;
        }

        ///// <summary>
        ///// 原始收件人信息
        ///// </summary>
        //[InverseProperty("OrderFor")]
        public virtual Receiver OrgReceiver {
            get;
            set;
        }


        ///// <summary>
        ///// 收件人调整信息
        ///// </summary>
        //[InverseProperty("OrderFor")]
        public virtual AdjReceiver AdjReceiver {
            get;
            set;
        }

        public virtual ReceiverBase Receiver {
            get {
                return this.AdjReceiver != null ? this.AdjReceiver as ReceiverBase : this.OrgReceiver as ReceiverBase;
            }
        }

        /// <summary>
        /// 订单金额
        /// </summary>
        [Range(0, 1000000), Required]
        public decimal Amount {
            get;
            set;
        }

        /// <summary>
        /// 币别
        /// </summary>
        [StringLength(3), Required]
        public string Currency {
            get;
            set;
        }

        /// <summary>
        /// 付款类型
        /// </summary>
        [StringLength(10)]
        public string PaymentType {
            get;
            set;
        }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime CreteOn {
            get;
            set;
        }

        /// <summary>
        /// 付款时间
        /// </summary>
        public DateTime? PaymentOn {
            get;
            set;
        }


        public virtual OrderNote Note {
            get;
            set;
        }

        public virtual ICollection<OrderMessage> Messages {
            get;
            set;
        }

        public virtual ICollection<OrderDetail> Details {
            get;
            set;
        }

        public virtual ICollection<OrdeLogistic> Logistics {
            get;
            set;
        }

        [ForeignKey("AccountOf")]
        public string Account {
            get;
            set;
        }

        public virtual Owner AccountOf {
            get;
            set;
        }

        /// <summary>
        /// 截止时间，风控时为风时间。待发货为发货截止。。。
        /// </summary>
        public DateTime OffTime {
            get;
            set;
        }

        /// <summary>
        /// 是否纠纷中
        /// </summary>
        public bool InIssue {
            get;
            set;
        }

        /// <summary>
        /// 是否虚假发货,以填单，未发货
        /// </summary>
        public bool IsShamShipping {
            get;
            set;
        }

        public virtual PurchaseDetail PurchasseDetail {
            get;
            set;
        }
    }
}
