using System.Windows;
using System.Windows.Controls;

namespace AsNum.Xmj.OnlineLogistics {
    public class LocSelector : DataTemplateSelector {

        public DataTemplate NearLocTemplate { get; set; }

        public DataTemplate DefaultTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var str = (string)item;
            if (str.Contains("深圳"))
                return this.NearLocTemplate;
            else
                return this.DefaultTemplate;
        }

    }
}
