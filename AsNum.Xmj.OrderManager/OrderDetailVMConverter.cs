using AsNum.Aliexpress.Entity;
using AsNum.Xmj.OrderManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AsNum.Xmj.OrderManager {
    public class OrderDetailVMConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value.GetType().BaseType == typeof(Order))
                return new OrderDetailCellTemplateVewModel();
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return null;
        }
    }
}
