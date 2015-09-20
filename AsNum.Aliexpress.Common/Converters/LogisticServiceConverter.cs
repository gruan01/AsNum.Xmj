using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AsNum.Xmj.Common.Converters {
    public class LogisticServiceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var codes = ((string)value).Split(',').Where(c => !string.IsNullOrWhiteSpace(c)).ToList();
            codes = codes.Where(c => !string.IsNullOrWhiteSpace(c))
                .Select(c => {
                    var lo = GlobalData.LogisticService.FirstOrDefault(l => l.Code.Equals(c));
                    if (lo == null)
                        return c;
                    else
                        return lo.Display;
                }).ToList();
            return string.Join(", ", codes);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
