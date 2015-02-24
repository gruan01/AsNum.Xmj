using System.Drawing;

namespace AsNum.Xmj.Common.Interfaces {
    public interface IScreenShoter {
        void Attach(IScreenShoterObserver observer);
        void Detach(IScreenShoterObserver observer);
        void Notify(Bitmap img);
    }
}
