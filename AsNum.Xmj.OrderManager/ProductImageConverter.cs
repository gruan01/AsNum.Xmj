using System;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace AsNum.Xmj.OrderManager {
    public class ProductImageConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            //throw new NotImplementedException();
            var ma = Regex.Match((string)value, @"(?<org>[\s\S]*?\.jpg)_");
            if (ma.Success) {
                return string.Format("{0}_120x120.jpg", ma.Groups["org"].Value);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
