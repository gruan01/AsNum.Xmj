using System.Runtime.Remoting.Contexts;

namespace AsNum.Xmj.API {
    [Synchronization(Locked=true)]
    internal class AuthHelper {
        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void DoAuth(Auth auth) {
            auth.DoAuth(auth.User, auth.Pwd);
        }
    }
}
