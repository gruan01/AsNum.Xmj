using AsNum.Xmj.API.Attributes;

namespace AsNum.Xmj.API.Entity {
    public enum OnlineLogisticStatus {
        /// <summary>
        /// 等待分配物流单号
        /// </summary>
        [SpecifyNameValue(Name = "init")]
        Init,

        /// <summary>
        /// 等待仓库操作
        /// </summary>
        [SpecifyNameValue(Name = "wait_warehouse_receive_goods")]
        Wait_warehouse_receive_goods,

        /// <summary>
        /// 揽收成功
        /// </summary>
        [SpecifyNameValue(Name = "pickup_success")]
        Pickup_success,

        /// <summary>
        /// 揽收失败
        /// </summary>
        [SpecifyNameValue(Name = "pickup_fail")]
        Pickup_Fail,

        /// <summary>
        /// 入库失败
        /// </summary>
        [SpecifyNameValue(Name = "warehouse_reject_goods")]
        Warehouse_reject_goods,

        /// <summary>
        /// 等待出库
        /// </summary>
        [SpecifyNameValue(Name = "wait_Warehouse_sendGoods")]
        Wait_Warehouse_sendGoods,

        /// <summary>
        /// 等待发货
        /// </summary>
        [SpecifyNameValue(Name = "out_stock_success")]
        Out_stock_success,

        /// <summary>
        /// 出库失败
        /// </summary>
        [SpecifyNameValue(Name = "out_stock_fail")]
        Out_stock_fail,

        /// <summary>
        /// 发货失败
        /// </summary>
        [SpecifyNameValue(Name = "send_goods_fail")]
        Send_goods_fail,

        [SpecifyNameValue(Name = "send_goods_success")]
        Send_goods_success,

        Closed
    }
}
