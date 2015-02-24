using AsNum.Xmj.Entity;

namespace AsNum.Xmj.IBiz {
    public interface IReceiver {

        AdjReceiver Save(AdjReceiver Receiver);

        void RemoveAdjReceiver(string orderNO);

    }
}
