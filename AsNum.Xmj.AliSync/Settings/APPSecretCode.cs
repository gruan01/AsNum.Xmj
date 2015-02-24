using AsNum.Xmj.API;
using AsNum.Xmj.Common;
using System.ComponentModel;

namespace AsNum.Xmj.AliSync.Settings {
    [DisplayName("签名串"), Description("API 签名串")]
    public class APPSecretCode : AppSettingBase<string> {

        public override string Key {
            get {
                return ConstValue.SECKeyAppSettingKey;
            }
        }

    }
}
