using System;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace AsNum.Xmj.Common.Converters {
    public class SerialNumberConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return Regex.Replace(value.ToString(), @"(.{4})", "$1 ");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
