using AsNum.Common.Security;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AsNum.Common.Extends {
    public static class PersistFileHelper {

        private static Lazy<string> MachineKey = new Lazy<string>(() => {
            return "abc";
        });

        private static string DefaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AsNum.Aliexpress");

        public static void Save(object o, string file, string key = null) {
            if (o == null)
                throw new ArgumentNullException("o");

            if (!o.GetType().IsSerializable)
                throw new ArgumentException(string.Format("{0} 不可序例化", o.GetType().FullName));

            if (string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException("file");

            if (string.IsNullOrWhiteSpace(key))
                key = MachineKey.Value;

            var path = DefaultPath;
            if (Path.IsPathRooted(file)) {
                path = Path.GetDirectoryName(file);
            } else
                file = Path.Combine(path, file);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //var aa = AesHelper.Encrypt("ABC", key);
            //var bb = AesHelper.Decrypt(aa, key);

            var bf = new BinaryFormatter();
            using (var msm = new MemoryStream())
            using (var fs = new FileStream(file, FileMode.Create)) {
                bf.Serialize(msm, o);
                var a = Security.AesHelper.Encrypt(msm.GetBytes(), key);
                bf.Serialize(fs, a);
            }
        }

        public static T Load<T>(string file, string key = null) where T : class {

            if (string.IsNullOrWhiteSpace(file))
                throw new ArgumentNullException("file");

            if (string.IsNullOrWhiteSpace(key))
                key = MachineKey.Value;

            var path = DefaultPath;
            if (Path.IsPathRooted(file)) {
                path = Path.GetDirectoryName(file);
            } else
                file = Path.Combine(path, file);

            if (File.Exists(file)) {
                var bf = new BinaryFormatter();
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read)) {
                    var bytes = (byte[])bf.Deserialize(fs);
                    var a = AesHelper.Decrypt(bytes, key);
                    using (var msm = new MemoryStream(a)) {
                        try {
                            return (T)bf.Deserialize(msm);
                        } catch {
                            return null;
                        }
                    }
                }
            }
            return null;
        }
    }
}
