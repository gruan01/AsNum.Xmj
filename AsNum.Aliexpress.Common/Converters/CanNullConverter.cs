using System;
using System.ComponentModel;
using System.Windows.Data;

namespace AsNum.Xmj.Common.Converters {
    public class CanNullConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            NullableConverter nullableConvert;
            var toType = targetType;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
                nullableConvert = new NullableConverter(targetType);
                toType = nullableConvert.UnderlyingType;
            }

            return value.GetType().Equals(toType) ? value : null;
        }
    }
}
