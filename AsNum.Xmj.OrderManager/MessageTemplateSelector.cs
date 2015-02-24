using System.Windows;
using System.Windows.Controls;

namespace AsNum.Xmj.OrderManager {
    public class MessageTemplateSelector : DataTemplateSelector {

        public DataTemplate LeftTemplate {
            get;
            set;
        }

        public DataTemplate RightTemplate {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var ome = (OrderMessageEx)item;
            if (ome == null)
                return null;

            if (ome.Left)
                return LeftTemplate;
            else
                return RightTemplate;
        }

    }
}
