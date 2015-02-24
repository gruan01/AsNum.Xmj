using AsNum.Xmj.Entity;
using System.Collections.Generic;

namespace AsNum.Xmj.Common.Interfaces {
    public interface IQuickTrackButton {
        void Track(List<OrdeLogistic> logistics);

        string Title {
            get;
        }
    }
}
