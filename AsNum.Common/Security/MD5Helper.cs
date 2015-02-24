using System;
using System.Security.Cryptography;
using System.Text;

namespace AsNum.Common.Security {
    public static class MD5Helper {
        ///// <summary>
        ///// MD5 16位元加密 加密後密碼為小寫
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public static string Md5(this string input) {
        //    using(var md5 = new MD5CryptoServiceProvider()) {
        //        string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(input)), 4, 8);
        //        t2 = t2.Replace("-", "");
        //        t2 = t2.ToLower();
        //        return t2;
        //    }
        //}

        /// <summary>
        /// MD5 32位元加密 加密後密碼為小寫
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToMD5(this string input) {
            using(var md5Hasher = MD5.Create()) {
                byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for(int i = 0; i < data.Length; i++) {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        public static string To16bitMD5(this string input) {
            using(var md5 = new MD5CryptoServiceProvider()) {
                string result = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(input)), 4, 8);
                return result.Replace("-", "");
            }
        }
    }
}
