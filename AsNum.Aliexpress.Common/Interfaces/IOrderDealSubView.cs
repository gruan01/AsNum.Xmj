
using AsNum.Xmj.Entity;
namespace AsNum.Xmj.Common.Interfaces {
    public interface IOrderDealSubView {

        string Title {
            get;
        }

        void Build(Order order);
    }
}
