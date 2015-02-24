using AsNum.Xmj.API;
using AsNum.Xmj.Common;

namespace AsNum.Xmj.APITestTool {
    public class APPSecretCode : AppSettingBase<string> {

        public override string Key {
            get {
                return ConstValue.SECKeyAppSettingKey;
            }
        }

    }
}
