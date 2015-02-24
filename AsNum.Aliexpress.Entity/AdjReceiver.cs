using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {

    /// <summary>
    /// <remarks>
    /// TPC
    /// </remarks>
    /// </summary>
    public class AdjReceiver : ReceiverBase {
        [ForeignKey("OrderNO")]
        public new virtual Order OrderFor {
            get;
            set;
        }
    }
}
