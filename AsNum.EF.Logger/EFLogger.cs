using System.Data.Entity;

namespace AsNum.Xmj.EFLogger {
    public static class EFLogger {

        public static void Init() {
            DbConfiguration.SetConfiguration(new Configuration());
        }


        internal static bool IsEnable = false;
    }
}
