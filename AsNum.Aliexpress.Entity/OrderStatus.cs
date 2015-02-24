using System.ComponentModel;

namespace AsNum.Xmj.Entity {
    public enum OrderStatus : byte {

        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        UNKNOW = 0,

        /// <summary>
        /// 等待买家付款
        /// </summary>
        [Description("等待买家付款")]
        PLACE_ORDER_SUCCESS = 1,

        /// <summary>
        /// 买家申请取消
        /// </summary>
        [Description("买家申请取消")]
        IN_CANCEL = 2,

        /// <summary>
        /// 等待发货
        /// </summary>
        [Description("等待发货")]
        WAIT_SELLER_SEND_GOODS = 3,

        /// <summary>
        /// 部分发货
        /// </summary>
        [Description("部分发货")]
        SELLER_PART_SEND_GOODS = 4,

        /// <summary>
        /// 等待买家收货
        /// </summary>
        [Description("等待买家收货")]
        WAIT_BUYER_ACCEPT_GOODS = 5,

        /// <summary>
        /// 已结束的订单
        /// </summary>
        [Description("已结束的订单")]
        FINISH = 6,

        ///// <summary>
        ///// 含纠纷的订单
        ///// </summary>
        //[Description("含纠纷的订单")]
        //IN_ISSUE = 7,

        /// <summary>
        /// 冻结中的订单
        /// </summary>
        [Description("冻结中的订单")]
        IN_FROZEN = 8,

        /// <summary>
        /// 等待您确认金额
        /// </summary>
        [Description("等待您确认金额")]
        WAIT_SELLER_EXAMINE_MONEY = 9,

        [Description("资金未到账")]
        RISK_CONTROL = 10,

        [Description("资金处理中")]
        FUND_PROCESSING = 11

    }
}
