using AsNum.Common.Extends;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace AsNum.Xmj.Common.Converters {
    //[ValueConversion(typeof(int), typeof(string))]
    public class EnumDescriptionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null) {
                return null;
            }

            if (!value.GetType().IsEnum) {

                var t = (Type)parameter;
                if (t != null) {
                    var v = Enum.Parse(t, value.ToString());
                    return EnumHelper.GetDescription(v);
                }

                return value;
            } else {
                return EnumHelper.GetDescription(value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            NullableConverter nullableConvert;
            var toType = targetType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
                nullableConvert = new NullableConverter(targetType);
                toType = nullableConvert.UnderlyingType;
            }

            var dic = EnumHelper.GetDescriptions(toType);
            var item = dic.FirstOrDefault(d => d.Value.Equals(value));
            return item.Key;
        }
    }
}
