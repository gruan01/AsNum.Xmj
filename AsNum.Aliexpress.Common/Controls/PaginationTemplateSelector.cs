using System.Windows;
using System.Windows.Controls;

namespace AsNum.Xmj.Common.Controls {
    public class PaginationTemplateSelector : DataTemplateSelector {

        public DataTemplate CurrIndexTemplate {
            get;
            set;
        }

        public DataTemplate ClickableTemplate {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var o = (PageItem)item;
            if (o.IsCurrPage)
                return this.CurrIndexTemplate;
            else
                return this.ClickableTemplate;
        }
    }
}
