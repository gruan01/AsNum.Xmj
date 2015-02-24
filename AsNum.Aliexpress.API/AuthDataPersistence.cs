using AsNum.Common.Extends;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;


namespace AsNum.Xmj.API {
    public static class AuthDataPersistence {

        private static Dictionary<string, Auth> Auths = new Dictionary<string, Auth>();

        static AuthDataPersistence() {
            //TODO 这里要想办法给它改掉
            //WPF
            if (Application.Current != null)
                Application.Current.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            else //WINFORM
                System.Windows.Forms.Application.ApplicationExit += Application_ApplicationExit;
        }

        private static void Application_ApplicationExit(object sender, EventArgs e) {
            SaveToFile();
        }

        static void Dispatcher_ShutdownStarted(object sender, EventArgs e) {
            SaveToFile();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Save(Auth opts) {
            Auths.Set(opts.User.ToLower(), opts);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SaveToFile() {
            foreach (var k in Auths) {
                var opts = k.Value;
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AsNum.Aliexpress");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var file = Path.Combine(path, opts.User);

                var bf = new BinaryFormatter();
                using (var fs = new FileStream(file, FileMode.Create)) {
                    bf.Serialize(fs, opts);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Auth Load(string user) {
            user = user.ToLower();
            //先查内存
            var auth = Auths.Get(user, null);
            if (auth != null)
                return auth;

            var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AsNum.Aliexpress", user);
            if (File.Exists(file)) {
                var bf = new BinaryFormatter();
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read)) {
                    try {
                        auth = (Auth)bf.Deserialize(fs);
                        Auths.Set(user, auth);
                        return auth;
                    } catch {
                    }
                }
            }
            return null;
        }
    }
}
