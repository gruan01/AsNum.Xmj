using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace AsNum.Common.Extends {
    public static class CollectionHelper {

        /// <summary>
        /// 如果沒有找到，返回預設值, 而不是返回null
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string Get(this NameValueCollection coll, string key, string defaultValue) {
            if (null == coll)
                throw new ArgumentNullException("coll");
            if (coll[key] == null)
                return defaultValue;
            else
                return coll[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue) {
            if (null == dic)
                throw new ArgumentNullException("dic");
            if (dic.ContainsKey(key))
                return dic[key];
            else
                return defaultValue;
        }

        /// <summary>
        /// 如果没有找到，抛出带键值的异常.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key) {
            if (null == dic)
                throw new ArgumentNullException("dic");
            if (dic.ContainsKey(key))
                return dic[key];
            else
                throw new ArgumentOutOfRangeException("key", key, "关键字不存在于给定的字典中");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value) {
            if (null == dic)
                throw new ArgumentNullException("dic");

            if (dic.ContainsKey(key))
                dic[key] = value;
            else
                dic.Add(key, value);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pager"></param>
        /// <returns></returns>
        public static IQueryable<T> DoPage<T>(this IQueryable<T> source, Pager pager) {
            pager.Count = source.Count();
            if (pager.AllowPage) {
                return source
                    .Skip((pager.Page ?? 0) * pager.PageSize)
                    .Take(pager.PageSize);
            } else
                return source;
        }

    }
}
