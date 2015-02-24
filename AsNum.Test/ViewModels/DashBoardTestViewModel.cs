using AsNum.Xmj.Common;
using System.Collections.Generic;

namespace AsNum.Test.ViewModels {
    public class DashBoardTestViewModel : VMScreenBase {
        public override string Title {
            get {
                return "仪表盘测试";
            }
        }



        public List<ViewModes> Modes { get; set; }

        public DashBoardTestViewModel() {
            this.Modes = new List<ViewModes>() { ViewModes.Normal, ViewModes.Small };
        }
    }
}
