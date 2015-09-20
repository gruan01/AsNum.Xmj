using AsNum.Xmj.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AsNum.Xmj {
    public class MenuTemplateSelector : StyleSelector {
        public Style Normal { get; set; }

        public Style Separator { get; set; }

        public override Style SelectStyle(object item, DependencyObject container) {
            if (typeof(IMenuItem).IsInstanceOfType(item)) {
                var menu = (IMenuItem)item;
                return menu.IsSeparator ? this.Separator : this.Normal;
            } else {
                return null;
            }
        }

        //public override DataTemplate SelectTemplate(object item, DependencyObject container) {
        //    if (typeof(IMenuItem).IsInstanceOfType(item)) {
        //        var menu = (IMenuItem)item;
        //        return menu.IsSeparator ? this.Separator : this.Normal;
        //    } else {
        //        return null;
        //    }
        //}

        //public override DataTemplate SelectTemplate(object item, ItemsControl parentItemsControl) {
        //    if (typeof(IMenuItem).IsInstanceOfType(item)) {
        //        var menu = (IMenuItem)item;
        //        return menu.IsSeparator ? this.Separator : this.Normal;
        //    } else {
        //        return null;
        //    }
        //}
    }
}
