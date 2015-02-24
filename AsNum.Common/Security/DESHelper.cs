using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AsNum.Common.Security {
    public static class DESHelper {
        /// <summary>
        /// 獲取金鑰
        /// </summary>
        private static string Key {
            get {
                return @"P@+#wG+Z";
            }
        }

        /// <summary>
        /// 獲取向量
        /// </summary>
        private static string IV {
            get {
                return @"L%n67}G\Mk@k%:~Y";
            }
        }


        /// <summary>
        /// DES加密
        /// 使用此類裡硬編碼的Key及IV
        /// </summary>
        /// <param name="input">明文字串</param>
        /// <returns>密文</returns>
        public static string Encrypt(string input) {
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(Key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(IV);

            return Encrypt(buffer, rgbKey, rgbIV);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="input">明文字串</param>
        /// <param name="key">金鑰</param>
        /// <returns>密文</returns>
        public static string Encrypt(string input, string key) {
            byte[] buffer = Encoding.UTF8.GetBytes(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.ToMD5());
            byte[] rgbIV = Encoding.UTF8.GetBytes(key.To16bitMD5());

            return Encrypt(buffer, rgbKey, rgbIV);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key">金鑰</param>
        /// <param name="iv">向量</param>
        /// <returns>密文</returns>
        public static string Encrypt(string input, string key, string iv) {
            byte[] buffer = Convert.FromBase64String(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(Key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(IV);
            return Encrypt(buffer, rgbKey, rgbIV);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="rgbKey">金鑰</param>
        /// <param name="rgbIV">向量</param>
        /// <returns>密文</returns>
        public static string Encrypt(string input, byte[] rgbKey, byte[] rgbIV) {
            byte[] buffer = Convert.FromBase64String(input);
            return Encrypt(buffer, rgbKey, rgbIV);
        }

        private static string Encrypt(byte[] buffer, byte[] rgbKey, byte[] rgbIV) {
            string encrypt = null;
            using (var des = new DESCryptoServiceProvider()) {
                try {
                    using (MemoryStream mStream = new MemoryStream())
                    using (CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write)) {
                        cStream.Write(buffer, 0, buffer.Length);
                        cStream.FlushFinalBlock();
                        encrypt = Convert.ToBase64String(mStream.ToArray());
                    }
                } catch {
                }
                des.Clear();
            }
            return encrypt;
        }

        /// <summary>
        /// DES解密
        /// 使用此類裡硬編碼的Key及IV
        /// </summary>
        /// <param name="input">密文字串</param>
        /// <returns>明文</returns>
        public static string Decrypt(string input) {
            byte[] buffer = Convert.FromBase64String(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(Key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(IV);

            return Decrypt(buffer, rgbKey, rgbIV);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="input">密文字串</param>
        /// <param name="key">金鑰</param>
        /// <returns>明文</returns>
        public static string Decrypt(string input, string key) {
            byte[] buffer = Convert.FromBase64String(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(key.ToMD5());
            byte[] rgbIV = Encoding.UTF8.GetBytes(key.To16bitMD5());

            return Decrypt(buffer, rgbKey, rgbIV);
        }


        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key">金鑰</param>
        /// <param name="iv">向量</param>
        /// <returns>明文</returns>
        public static string Decrypt(string input, string key, string iv) {
            byte[] buffer = Convert.FromBase64String(input);
            byte[] rgbKey = Encoding.UTF8.GetBytes(Key);
            byte[] rgbIV = Encoding.UTF8.GetBytes(IV);
            return Decrypt(buffer, rgbKey, rgbIV);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="rgbKey">金鑰</param>
        /// <param name="rgbIV">向量</param>
        /// <returns>明文</returns>
        public static string Decrypt(string input, byte[] rgbKey, byte[] rgbIV) {
            byte[] buffer = Convert.FromBase64String(input);
            return Decrypt(buffer, rgbKey, rgbIV);
        }

        private static string Decrypt(byte[] buffer, byte[] rgbKey, byte[] rgbIV) {
            string decrypt = null;
            using (var des = new DESCryptoServiceProvider()) {
                try {
                    using (MemoryStream mStream = new MemoryStream()) {
                        using (CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write)) {
                            cStream.Write(buffer, 0, buffer.Length);
                            cStream.FlushFinalBlock();
                            decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                        }
                    }
                } catch {
                }
                des.Clear();
            }

            return decrypt;
        }
    }
}
