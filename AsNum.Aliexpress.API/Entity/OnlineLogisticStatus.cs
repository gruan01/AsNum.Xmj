using AsNum.Xmj.API.Attributes;

namespace AsNum.Xmj.API.Entity {
    public enum OnlineLogisticStatus {
        /// <summary>
        /// 等待分配物流单号
        /// </summary>
        [SpecifyValue("init")]
        Init,

        /// <summary>
        /// 等待仓库操作
        /// </summary>
        [SpecifyValue("wait_warehouse_receive_goods")]
        Wait_warehouse_receive_goods,

        /// <summary>
        /// 揽收成功
        /// </summary>
        [SpecifyValue("pickup_success")]
        Pickup_success,

        /// <summary>
        /// 揽收失败
        /// </summary>
        [SpecifyValue("pickup_fail")]
        Pickup_Fail,

        /// <summary>
        /// 入库失败
        /// </summary>
        [SpecifyValue("warehouse_reject_goods")]
        Warehouse_reject_goods,

        /// <summary>
        /// 等待出库
        /// </summary>
        [SpecifyValue("wait_Warehouse_sendGoods")]
        Wait_Warehouse_sendGoods,

        /// <summary>
        /// 等待发货
        /// </summary>
        [SpecifyValue("out_stock_success")]
        Out_stock_success,

        /// <summary>
        /// 出库失败
        /// </summary>
        [SpecifyValue("out_stock_fail")]
        Out_stock_fail,

        /// <summary>
        /// 发货失败
        /// </summary>
        [SpecifyValue("send_goods_fail")]
        Send_goods_fail,

        [SpecifyValue("send_goods_success")]
        Send_goods_success,

        Closed
    }
}
