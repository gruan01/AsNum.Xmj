using AsNum.Xmj.Common;
using System.ComponentModel;

namespace AsNum.Xmj.AliSync.Settings {
    [DisplayName("多少天以内的数据")]
    public class SmartSyncDays : AppSettingBase<int> {

        public override int DefaultValue {
            get {
                return 120;
            }
        }

    }
}
