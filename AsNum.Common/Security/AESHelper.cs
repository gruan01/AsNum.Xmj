using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AsNum.Common.Security {
    public static class AesHelper {
        #region
        /// <summary>
        /// 獲取金鑰
        /// </summary>
        private static string Key {
            get {
                return @")O[NB]6,YF}+efcaj{+oESb9d8>Z'e9M";
            }
        }

        /// <summary>
        /// 獲取向量
        /// </summary>
        private static string IV {
            get {
                return @"L+\~f4,Ir)b$=pkf";
            }
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input">明文字串</param>
        /// <returns>密文</returns>
        public static string Encrypt(string input) {
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(Key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(IV);

            return Convert.ToBase64String(Encrypt(buffer, rgbKey, rgbIV));
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input">明文字串</param>
        /// <param name="key">金鑰</param>
        /// <returns>密文</returns>
        public static string Encrypt(string input, string key) {
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
            byte[] rgbIV = Encoding.UTF8.GetBytes(key.PadRight(16, '0').Substring(0, 16));
            var a = Encrypt(buffer, rgbKey, rgbIV);
            return Convert.ToBase64String(a);
        }

        public static byte[] Encrypt(byte[] buffer, string key) {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
            byte[] rgbIV = Encoding.UTF8.GetBytes(key.PadRight(16, '0').Substring(0, 16));
            return Encrypt(buffer, rgbKey, rgbIV);
        }

        private static byte[] Encrypt(byte[] buffer, byte[] rgbKey, byte[] rgbIV) {

            if (buffer == null || buffer.Length == 0 || rgbKey == null || rgbKey.Length == 0 || rgbIV == null || rgbIV.Length == 0)
                throw new ArgumentNullException();

            using (var memoryStream = new MemoryStream()) {
                using (var rijndaelManaged = new RijndaelManaged { Key = rgbKey, IV = rgbIV, Padding = PaddingMode.PKCS7, Mode = CipherMode.CBC })
                using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write)) {
                    cryptoStream.Write(buffer, 0, buffer.Length);
                }
                //这里 memoryStream 以经关闭, 不能用扩展方法 GetBytes
                return memoryStream.ToArray();
            }
        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="input">密文字串</param>
        /// <returns>明文</returns>
        public static string Decrypt(string input) {
            byte[] buffer = Convert.FromBase64String(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(Key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(IV);

            return Encoding.UTF8.GetString(Decrypt(buffer, rgbKey, rgbIV));
        }

        public static byte[] Decrypt(byte[] bytes, string key) {
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
            byte[] rgbIV = Encoding.UTF8.GetBytes(key.PadRight(16, '0').Substring(0, 16));

            return Decrypt(bytes, rgbKey, rgbIV);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="input">密文字串</param>
        /// <param name="key">金鑰</param>
        /// <returns>明文</returns>
        public static string Decrypt(string input, string key) {
            byte[] buffer = Convert.FromBase64String(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
            byte[] rgbIV = Encoding.UTF8.GetBytes(key.PadRight(16, '0').Substring(0, 16));

            return Encoding.UTF8.GetString(Decrypt(buffer, rgbKey, rgbIV));
        }

        /// <summary>
        /// AES 解密, 如果解密失敗,返回 defaultValue
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string Decrypt(string input, string key, string defaultValue) {
            try {
                return Decrypt(key);
            } catch {
                return defaultValue;
            }
        }

        private static byte[] Decrypt(byte[] buffer, byte[] rgbKey, byte[] rgbIV) {
            //string decrypt = null;
            using (var mStream = new MemoryStream()) {
                using (var aes = Rijndael.Create()) {
                    using (var cStream = new CryptoStream(mStream, aes.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write)) {
                        cStream.Write(buffer, 0, buffer.Length);
                        cStream.FlushFinalBlock();
                    }
                    aes.Clear();
                }
                //decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                return mStream.ToArray();
            }

            //return decrypt;
        }

        #endregion

    }
}
