using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AsNum.Common.Extends {
    /// <summary>
    /// 枚举扩展方法类
    /// </summary>
    public static class EnumHelper {

        public static Dictionary<T, string> GetDescriptions<T>() where T : struct, IComparable, IConvertible, IFormattable {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("不是一个有效的枚举类型");
            var ms = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            return ms.Select(m => {
                var desc = ((DescriptionAttribute[])m.GetCustomAttributes(typeof(DescriptionAttribute), false)).FirstOrDefault();
                return new {
                    K = m.Name.ToEnum<T>(),
                    V = desc != null ? desc.Description : m.Name
                };
            }).ToDictionary(a => a.K, a => a.V);
        }

        public static Dictionary<string, string> GetDescriptions(Type type) {
            if (!type.IsEnum)
                throw new ArgumentException("不是一个有效的枚举类型");
            var ms = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            return ms.Select(m => {
                var desc = ((DescriptionAttribute[])m.GetCustomAttributes(typeof(DescriptionAttribute), false)).FirstOrDefault();
                return new {
                    K = m.Name,
                    V = desc != null ? desc.Description : m.Name
                };
            }).ToDictionary(a => a.K, a => a.V);
        }
        public static Dictionary<string, string> GetDescriptions(object o) {
            var type = o.GetType();
            return GetDescriptions(type);
            //if (!type.IsEnum)
            //    throw new ArgumentException("不是一个有效的枚举类型");
            //var ms = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            //return ms.Select(m => {
            //    var desc = ((DescriptionAttribute[])m.GetCustomAttributes(typeof(DescriptionAttribute), false)).FirstOrDefault();
            //    return new {
            //        K = m.Name,
            //        V = desc != null ? desc.Description : m.Name
            //    };
            //}).ToDictionary(a => a.K, a => a.V);
        }

        public static string GetDescription<T>(T value) where T : struct, IComparable, IConvertible, IFormattable {
            var dic = GetDescriptions<T>();
            return dic.Get(value, value.ToString());
        }

        public static string GetDescription(object value) {
            var dic = GetDescriptions(value);
            return dic.Get(value.ToString(), value.ToString());
        }

        public static TA GetAttribute<T, TA>(T value)
            where T : struct, IComparable, IConvertible, IFormattable
            where TA : Attribute {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException("不是一个有效的枚举类型");

            var field = type.GetField(value.ToString());
            return field.GetCustomAttributes(false).OfType<TA>().FirstOrDefault();
        }
    }
}