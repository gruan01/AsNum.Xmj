using AsNum.Xmj.Common.Interfaces;
using System.Windows;
using System.Windows.Controls;

namespace AsNum.Xmj {
    internal class MenuItemTemplateSelector : DataTemplateSelector {

        public DataTemplate Separator {
            get;
            set;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container) {
            var obj = item as IMenuItem;
            if (obj != null && string.IsNullOrWhiteSpace(obj.Header))
                return this.Separator;

            return base.SelectTemplate(item, container);
        }

    }
}
