using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Entity;
using System.Collections.Generic;

namespace AsNum.Xmj.IBiz {
    public interface IOrder : IBaseBiz {

        IEnumerable<Order> Search(OrderSearchCondition cond);

        int Count(OrderSearchCondition cond);

        Order GetOrder(string orderNo, bool include = true);

        Order AddOrEdit(Order order);

        /// <summary>
        /// 更新虚假发货状态
        /// </summary>
        /// <param name="orderNO"></param>
        /// <param name="isSham"></param>
        void UpdateShamShippingStatus(string orderNO, bool isSham);

        /// <summary>
        /// 保存订单留言
        /// </summary>
        /// <param name="messages"></param>
        void SaveOrderMessage(IEnumerable<OrderMessage> messages);

        /// <summary>
        /// 订单备注
        /// </summary>
        /// <param name="note"></param>
        void SaveOrderNote(OrderNote note);

        /// <summary>
        /// 采购信息
        /// </summary>
        /// <param name="detail"></param>
        void SavePurchaseDetail(PurchaseDetail detail);
    }
}
