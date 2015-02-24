using System;
using System.Windows.Data;

namespace AsNum.Xmj.Common.Converters {
    [ValueConversion(typeof(DateTime), typeof(TimeSpan))]
    public class OffTimeConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var offTime = (DateTime)value;
            var now = DateTime.Now;

            return offTime > now ? offTime - now : TimeSpan.Zero;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
