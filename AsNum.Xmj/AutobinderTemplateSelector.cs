using System.Windows;
using System.Windows.Controls;

namespace AsNum.Xmj {
    public class AutobinderTemplateSelector : DataTemplateSelector {
        public DataTemplate Template { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {

            //var t = item.GetType();
            //if (t.GetInterface("IScreen") == typeof(IScreen) || t.GetInterface("IViewAware") == typeof(IViewAware)) {
            //    return Template;
            //} else
            //    return null;
            return Template;
        }
    }
}
