using AsNum.Common;
using AsNum.Common.Attributes;
using AsNum.Xmj.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AsNum.Xmj.BizEntity.Conditions {
    public class OrderSearchCondition : BaseQuery<Order> {

        [MapTo("OrderNO", Opt = MapToOpts.Include)]
        public string OrderNO { get; set; }


        [MapTo("Status")]
        public OrderStatus? Status { get; set; }

        public List<OrderStatus> IncludeStatus { get; set; }

        [MapTo("OffTime", Opt = MapToOpts.LtOrEqual)]
        public DateTime? OffTime { get; set; }

        [MapTo("InIssue")]
        public bool? InIssue { get; set; }

        [MapTo("IsShamShipping")]
        public bool? IsShamShipping { get; set; }


        public string ReceiverName {
            get;
            set;
        }

        public string TrackNO {
            get;
            set;
        }

        public string ProductID {
            get;
            set;
        }

        public string CustomerName {
            get;
            set;
        }

        public string Note {
            get;
            set;
        }

        /// <summary>
        /// 单个账户
        /// </summary>
        [MapTo("Account")]
        public string Account { get; set; }

        /// <summary>
        /// 包含的账户
        /// </summary>
        public List<string> SpecifyAccounts { get; set; }

        public string LogisticsType { get; set; }

        public string ReceiverCountryCode { get; set; }

        /// <summary>
        /// 包含的订单号
        /// </summary>
        public List<string> SpecifyOrders { get; set; }

        /// <summary>
        /// 排除的订单号
        /// </summary>
        public List<string> ExcludeOrderNOs { get; set; }

        [MapTo("BuyerID")]
        public string BuyerID { get; set; }

        public TimesTypes? TimesType { get; set; }

        public DateTime? BeginAt { get; set; }


        public DateTime? EndAt { get; set; }


        public enum TimesTypes {
            [Description("订单创建时间")]
            ByCreateOn = 0,
            [Description("附款时间")]
            ByPayment = 1,
            [Description("发货时间")]
            BySendout = 2
        }
    }
}
