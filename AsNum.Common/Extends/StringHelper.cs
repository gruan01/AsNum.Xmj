using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace AsNum.Common.Extends {
    /// <summary>
    /// 字串擴展類
    /// </summary>
    public static class StringHelper {

        #region To int
        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(this string str, int defaultValue) {
            int v;
            if(int.TryParse(str, out v)) {
                return v;
            } else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str) {
            return str.ToInt(0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string str, int? defaultValue) {
            int v;
            if(int.TryParse(str, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int? ToIntOrNull(this string str) {
            return str.ToIntOrNull(null);
        }

        /// <summary>
        /// 智慧轉換為 Int ，取字串中的第一個數位串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int SmartToInt(this string str, int defaultValue) {
            if(string.IsNullOrEmpty(str))
                return defaultValue;

            //Match ma = Regex.Match(str, @"(\d+)");
            Match ma = Regex.Match(str, @"((-\s*)?\d+)");
            if(ma.Success) {
                return ma.Groups[1].Value.Replace(" ", "").ToInt(defaultValue);
            } else {
                return defaultValue;
            }
        }
        #endregion

        #region To Float
        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string str, float defaultValue) {
            float v;
            if(float.TryParse(str, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToFloat(this string str) {
            return str.ToFloat(0f);
        }

        /// <summary>
        /// 智慧轉換為 float ，取字串中的第一個數位串
        /// 可轉換 a1.2, 0.12 1.2aa , 1.2e+7
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float SmartToFloat(this string str, float defaultValue) {
            if(string.IsNullOrEmpty(str))
                return defaultValue;

            //Regex rx = new Regex(@"((\d+)(\.?((?=\d)\d+))?(e\+\d)*)");
            Regex rx = new Regex(@"((-\s*)?(\d+)(\.?((?=\d)\d+))?(e[\+-]?\d+)*)");
            Match ma = rx.Match(str);
            if(ma.Success) {
                return ma.Groups[1].Value.Replace(" ", "").ToFloat(defaultValue);
            } else {
                return defaultValue;
            }
        }
        #endregion

        #region To Decimal

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defaultValue) {
            decimal v;
            if(decimal.TryParse(str, NumberStyles.Any, null, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str) {
            return str.ToDecimal(0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal? ToDecimalOrNull(this string str) {
            decimal temp;
            if(decimal.TryParse(str, out temp))
                return temp;
            else
                return null;
        }

        /// <summary>
        /// 智慧轉換為 float ，取字串中的第一個數位串
        /// 可轉換 a1.2, 0.12 1.2aa , 1.2e+7
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal SmartToDecimal(this string str, decimal defaultValue) {
            if(string.IsNullOrEmpty(str))
                return defaultValue;

            //Regex rx = new Regex(@"((\d+)(\.?((?=\d)\d+))?(e\+\d)*)");
            //Regex rx = new Regex(@"((-\s*)?(\d+)(\.?((?=\d)\d+))?(e[\+-]?\d+)*)");
            Regex rx = new Regex(@"((-\s*)?(\d+(,\d+)*)(\.?((?=\d)\d+))?(e[\+-]?\d+)*)");
            Match ma = rx.Match(str);
            if(ma.Success) {
                return ma.Groups[1].Value.Replace(" ", "").Replace(",", "").ToDecimal(defaultValue);
            } else {
                return defaultValue;
            }
        }

        #endregion

        #region To byte
        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defaultValue) {
            byte v;
            if(byte.TryParse(str, out v)) {
                return v;
            } else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte ToByte(this string str) {
            return str.ToByte(0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte? ToByteOrNull(this string str, byte? defaultValue) {
            byte v;
            if(byte.TryParse(str, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte? ToByteOrNull(this string str) {
            return str.ToByteOrNull(null);
        }
        #endregion

        #region To long
        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(this string str, long defaultValue) {
            long v;
            if(long.TryParse(str, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this string str) {
            return str.ToLong(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long? ToLongOrNull(this string str) {
            long temp;
            if(long.TryParse(str, out temp))
                return temp;
            else
                return null;
        }

        #endregion

        #region To ulong
        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ulong ToUlong(this string str, ulong defaultValue) {
            ulong v;
            if(ulong.TryParse(str, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ulong ToUlong(this string str) {
            return str.ToUlong(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static ulong? ToUlongOrNull(this string str) {
            ulong temp;
            if(ulong.TryParse(str, out temp))
                return temp;
            else
                return null;
        }

        #endregion

        #region ToBool

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBool(this string str, bool defaultValue) {
            bool b;
            if(bool.TryParse(str, out b)) {
                return b;
            } else {
                return defaultValue;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(this string str) {
            return str.ToBool(false);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool? ToBoolOrNull(this string str) {
            bool temp;
            if(bool.TryParse(str, out temp))
                return temp;
            else
                return null;
        }


        #endregion

        #region ToDouble
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue) {
            double v;
            if(double.TryParse(str, NumberStyles.Any, null, out v))
                return v;
            else
                return defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ToDouble(this string str) {
            return str.ToDouble(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double? ToDoubleOrNull(this string str) {
            double temp;
            if(double.TryParse(str, out temp))
                return temp;
            else
                return null;
        }

        #endregion

        #region Enum

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public static T ToEnum<T>(this int value, T defaultValue) where T : struct, IComparable, IConvertible, IFormattable {
            var type = typeof(T);
            if(!type.IsEnum)
                throw new Exception("T 必须是枚举类型");

            if(Enum.IsDefined(type, value)) {
                return (T)Enum.ToObject(type, value);
            } else {
                return defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this int value) where T : struct, IComparable, IConvertible, IFormattable {
            return value.ToEnum<T>(default(T));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        public static T ToEnum<T>(this byte value, T defaultValue) where T : struct, IComparable, IConvertible, IFormattable {
            var type = typeof(T);
            if(!type.IsEnum)
                throw new Exception("T 必须是枚举类型");

            if(Enum.IsDefined(type, value)) {
                return (T)Enum.ToObject(type, value);
            } else {
                return defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this byte value) where T : struct, IComparable, IConvertible, IFormattable {
            return value.ToEnum<T>(default(T));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue, bool ignoreCase) where T : struct, IComparable, IConvertible, IFormattable {

            T o;
            bool flag = Enum.TryParse<T>(value, ignoreCase, out o);
            if(flag && Enum.IsDefined(typeof(T), o))
                return o;
            else
                return defaultValue;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct, IComparable, IConvertible, IFormattable {
            return value.ToEnum<T>(defaultValue, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value) where T : struct, IComparable, IConvertible, IFormattable {
            return value.ToEnum<T>(default(T));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignoreCase) where T : struct, IComparable, IConvertible, IFormattable {
            return value.ToEnum<T>(default(T), ignoreCase);
        }


        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this string value, int defaultValue, bool ignoreCase) where T : struct, IComparable, IConvertible, IFormattable {

            T o;
            bool flag = Enum.TryParse<T>(value, ignoreCase, out o);
            if(flag)

                return System.Convert.ToInt32(o);
            else
                return defaultValue;

        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this string value, int defaultValue) where T : struct, IComparable, IConvertible, IFormattable {
            return value.GetEnumValue<T>(defaultValue, true);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this T value, int defaultValue) where T : struct, IComparable, IConvertible, IFormattable {
            if(!typeof(T).IsEnum) {
                return defaultValue;
            } else {
                return Convert.ToInt32(value);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this T value) where T : struct, IComparable, IConvertible, IFormattable {
            return value.GetEnumValue<T>(0);
        }

        #endregion

        #region IPAddress
        /// <summary>
        /// 如果轉換失敗，返回 IPAddress.None
        /// </summary>
        /// <param name="ipstring"></param>
        /// <returns></returns>
        public static IPAddress ToIPAddress(this string ipstring) {
            IPAddress ip;
            if(IPAddress.TryParse(ipstring, out ip))
                return ip;
            else
                return IPAddress.None;
        }
        #endregion

        #region DateTime
        /// <summary>
        /// 轉換為日期，如果轉換失敗，返回預設值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(this string str, DateTime? defaultValue = null) {
            DateTime d;
            if(DateTime.TryParse(str, out d))
                return d;
            else {
                if(DateTime.TryParseExact(str, new string[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyyMMdd", "yyyyMMdd HH:mm:ss", "yyyy/MM/dd", "yyyy'/'MM'/'dd HH:mm:ss", "MM'/'dd'/'yyyy HH:mm:ss", "yyyy-M-d", "yyy-M-d hh:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
                    return d;
                else
                    return defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dateFmt"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(this string str, string dateFmt, DateTime? defaultValue = null) {
            DateTime d;
            //if (DateTime.TryParse(str, out d))
            //    return d;
            //else {
            if(DateTime.TryParseExact(str, dateFmt, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
                return d;
            else
                return defaultValue;
            //}        
        }

        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);

        /// <summary>
        /// 轉換日期，轉換失敗時，返回 defaultValue
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, DateTime defaultValue = default(DateTime)) {
            DateTime d;
            if(DateTime.TryParse(str, out d))
                return d;
            else {
                if(DateTime.TryParseExact(str, new string[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyyMMdd", "yyyyMMdd HH:mm:ss", "yyyy/MM/dd", "yyyy/MM/dd HH:mm:ss", "MM/dd/yyyy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
                    return d;
                else
                    if(default(DateTime) == defaultValue)
                        return MinDate;
                    else
                        return defaultValue;
            }
        }

        /// <summary>
        /// 按給定日期格式進行日期轉換
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dateFmt"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, string dateFmt, DateTime defaultValue) {
            DateTime d;
            //if (DateTime.TryParse(str, out d))
            //    return d;
            //else {
            if(DateTime.TryParseExact(str, dateFmt, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
                return d;
            else
                return defaultValue;
            //}            
        }

        /// <summary>
        /// 轉換為日期，轉換失敗時，返回null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeOrNull(this string str) {
            return str.ToDateTimeOrNull(null);
        }

        /// <summary>
        /// 轉換日期，轉換失敗時，返回當前時間
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str) {
            return str.ToDateTime(DateTime.Now);
        }

        /// <summary>
        /// 是否為日期型字串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string str) {
            //return Regex.IsMatch(str, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
            DateTime d;
            if(DateTime.TryParseExact(str, new string[] { "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyyMMdd", "yyyyMMdd HH:mm:ss", "yyyy/MM/dd", "yyyy/MM/dd HH:mm:ss", "MM/dd/yyyy", "MM/dd/yyyy HH:mm:ss" }, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out d))
                return true;
            else
                return false;
        }
        #endregion

        #region ToTimeSpan
        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string str, TimeSpan defaultValue) {
            TimeSpan t;
            if(TimeSpan.TryParse(str, out t)) {
                return t;
            } else {
                return defaultValue;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string str) {
            return str.ToTimeSpan(new TimeSpan());
        }
        #endregion

        #region Guid
        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string str) {
            Guid g;
            if(Guid.TryParse(str, out g))
                return g;
            else
                return Guid.Empty;
        }
        #endregion

        #region Url

        /// <summary>
        /// 從 URL 中取出鍵的值, 如果不存在,返回空
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string ParseString(this string s, string key, bool ignoreCase) {
            if(s == null)
                return ""; //必須這樣,請不要修改

            if(String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            Dictionary<string, string> kvs = s.ParseString(ignoreCase);
            key = ignoreCase ? key.ToLower() : key;
            if(kvs.ContainsKey(key)) {
                return kvs[key];
            }
            return "";
        }

        /// <summary>
        /// 從URL中取 Key / Value
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseString(this string s, bool ignoreCase) {
            //必須這樣,請不要修改
            if(string.IsNullOrEmpty(s)) {
                return new Dictionary<string, string>();
            }

            if(s.IndexOf('?') != -1) {
                s = s.Remove(0, s.IndexOf('?'));
            }

            Dictionary<string, string> kvs = new Dictionary<string, string>();
            Regex reg = new Regex(@"[\?&]?(?<key>[^=]+)=(?<value>[^\&]*)", RegexOptions.Compiled | RegexOptions.Multiline);
            MatchCollection ms = reg.Matches(s);
            string key;
            foreach(Match ma in ms) {
                key = ignoreCase ? ma.Groups["key"].Value.ToLower() : ma.Groups["key"].Value;
                if(kvs.ContainsKey(key)) {
                    kvs[key] += "," + ma.Groups["value"].Value;
                } else {
                    kvs[key] = ma.Groups["value"].Value;
                }
            }

            return kvs;
        }

        /// <summary>
        /// 設置 URL中的 key
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string SetUrlKeyValue(this string url, string key, string value, Encoding encode = null) {
            if(url == null)
                url = "";
            if(String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if(value == null)
                value = "";
            if(null == encode)
                encode = Encoding.UTF8;
            //if (!string.IsNullOrEmpty(url.ParseString(key, true).Trim())) {
            if(url.ParseString(true).ContainsKey(key.ToLower())) {
                Regex reg = new Regex(@"([\?\&])(" + key + @"=)([^\&]*)(\&?)", RegexOptions.IgnoreCase);
                //　如果 value 前几个字符是数字，有BUG
                //return reg.Replace(url, "$1$2" + HttpUtility.UrlEncode(value, encode) + "$4");

                return reg.Replace(url, (ma) => {
                    if(ma.Success) {
                        //var a = string.Format("{0}{1}{2}{3}", ma.Groups[1].Value, ma.Groups[2].Value, HttpUtility.UrlEncode(value, encode), ma.Groups[4].Value);
                        return string.Format("{0}{1}{2}{3}", ma.Groups[1].Value, ma.Groups[2].Value, HttpUtility.UrlEncode(value, encode), ma.Groups[4].Value);
                    }
                    return "";

                });

            } else {
                return url + (url.IndexOf('?') > -1 ? "&" : "?") + key + "=" + HttpUtility.UrlEncode(value, encode);
            }
        }


        /// <summary>
        /// 修正URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FixUrl(this string url) {
            return url.FixUrl("");
        }

        /// <summary>
        /// 修正URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaultPrefix"></param>
        /// <returns></returns>
        public static string FixUrl(this string url, string defaultPrefix) {
            // 必須這樣,請不要修改
            if(url == null)
                url = "";

            if(defaultPrefix == null)
                defaultPrefix = "";
            string tmp = url.Trim();
            if(!Regex.Match(tmp, "^(http|https):").Success) {
                tmp = string.Format("{0}/{1}", defaultPrefix, tmp);
            }
            tmp = Regex.Replace(tmp, @"(?<!(http|https):)[\\/]+", "/").Trim();
            return tmp;
        }
        #endregion

        #region mix
        /// <summary>
        /// 獲取用於 Javascript 的安全字串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string JsSafeString(this string str) {
            if(String.IsNullOrEmpty(str))
                return "";//必須這樣,請不要修改

            return str.ToString().Replace("'", "\\'").Replace("\"", "&quot;");
        }

        /// <summary>
        /// 清除两个以上的空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ClearupSpace(this string str) {
            return Regex.Replace(str.Replace("&nbsp;", " "), @"\s{2,}", " ").Trim();
        }


        #endregion
    }
}
