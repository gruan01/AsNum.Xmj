using AsNum.Xmj.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace AsNum.Xmj.OrderManager {

    internal class AttrConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var attrs = (List<OrderDetailAttribute>)value;
            if (value == null)
                return null;

            return string.Join(" ", attrs.Select(a => a.AttrStr));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
