using AsNum.Xmj.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace AsNum.Xmj.OnlineLogistics {
    public class ExistsLogisticInfoConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value != null) {
                var infos = (IEnumerable<OnlineLogisticsInfo>)value;
                return string.Join(",", infos.Select(s => s.TrackNO));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
