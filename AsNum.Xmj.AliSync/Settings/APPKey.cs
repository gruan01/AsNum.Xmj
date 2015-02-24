using AsNum.Xmj.API;
using AsNum.Xmj.Common;
using System.ComponentModel;

namespace AsNum.Xmj.AliSync.Settings {
    [Description("API Key"), DisplayName("Key")]
    public class APPKey : AppSettingBase<string> {

        public override string Key {
            get {
                return ConstValue.AppKeyAppSettingKey;
            }
        }

    }
}
