using System;
using System.Resources;

namespace AsNum.Common.Extends {
    public static class ResourceManagerHelper {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(this ResourceManager mgr , object key) {
            if(key == null || mgr == null)
                throw new ArgumentNullException("key");

            string type = key.GetType().Name;
            string key_ = string.Format("{0}_{1}" , type , key.ToString());
            string value = mgr.GetString(key_);
            if(string.IsNullOrEmpty(value)) {
                return mgr.GetString(key.ToString());
            } else
                return value;
        }
    }

}