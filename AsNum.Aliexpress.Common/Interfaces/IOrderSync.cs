using AsNum.Xmj.Entity;

namespace AsNum.Xmj.Common.Interfaces {
    public interface IOrderSync {
        void Sync(string orderID);
        void Sync(OrderStatus status = OrderStatus.UNKNOW, bool smart = true);
    }
}
