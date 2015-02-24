using AsNum.Xmj.BizEntity.Conditions;

namespace AsNum.Xmj.Common.Interfaces {
    public interface IOrderSearcher {
        //void Search(QueryEx<Order> ex);
        void Show(string title = "");

        void Search(OrderSearchCondition cond);
    }
}
