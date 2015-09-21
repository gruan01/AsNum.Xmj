using AsNum.Xmj.API.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AsNum.Xmj.OnlineLogistics {
    public class GroupSourceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return null;
            var lst = ((List<SupportOnlineLogisticsService>)value).OrderBy(l => l.DeliveryAddress.Contains("深圳") ? 0 : 1).ToList();
            var lsv = new ListCollectionView(lst);
            lsv.GroupDescriptions.Add(new PropertyGroupDescription("DeliveryAddress", new LocGroupConverter()));
            return lsv;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class LocGroupConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var str = ((string)value) ?? "";
            if (str.Contains("深圳"))
                return "深圳";
            else
                return "其它";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
