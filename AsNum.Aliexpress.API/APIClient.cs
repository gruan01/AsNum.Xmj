using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using System.Threading.Tasks;

namespace AsNum.Xmj.API {
    public class APIClient {

        static APIClient() {
            IConfigurationSource configurationSource = ConfigurationSourceFactory.Create();
            var injector = new PolicyInjector(configurationSource);
            try {
                PolicyInjection.SetPolicyInjector(injector);
            } catch { }
        }

        private Auth Auth = null;

        public string AuthUser
        {
            get
            {
                if (this.Auth != null)
                    return this.Auth.User;
                else
                    return "";
            }
        }

        //private List<SuccinctOrder> orders = null;
        //public List<SuccinctOrder> Orders {
        //    get {
        //        if (this.orders == null)
        //            this.orders = new List<SuccinctOrder>();
        //        return this.orders;
        //    }
        //    private set {
        //        this.orders = value;
        //    }
        //}

        public APIClient(string user, string pwd, string authCode = null) {
            this.Auth = APIClient.GetAuth(user, pwd);
            if (!string.IsNullOrWhiteSpace(authCode)) {
                this.Auth.Code = authCode;
                this.Auth.GetAccessToken();
            }
        }

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private static Auth GetAuth(string user, string pwd) {
            var auth = AuthDataPersistence.Load(user);

            if (auth == null)
                auth = PolicyInjection.Create<Auth>(user, pwd);
            else {
                auth = PolicyInjection.Wrap<Auth>(auth);
                //auth 在某些属性更改的时候，会自动保存，如果保存时密码错了，就一直是错的，
                //所以要在这里更新密码
                auth.Pwd = pwd;
            }
            return auth;
        }

        public async Task<string> GetResult<T>(T method) where T : MethodBase {
            var m = PolicyInjection.Wrap<T>(method);
            return await m.GetResult(this.Auth);
        }

        public async Task<string> GetResult(MethodBase method) {
            var m = (MethodBase)PolicyInjection.Wrap(method.GetType(), method);
            return await m.GetResult(this.Auth);
        }

        public async Task<T> Execute<T>(MethodBase<T> method) where T : class {
            var m = (MethodBase<T>)PolicyInjection.Wrap(method.GetType(), method);
            return await m.Execute(this.Auth);
        }
    }
}
