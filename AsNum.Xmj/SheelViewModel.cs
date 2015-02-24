using AsNum.Common.Annoations;
using AsNum.Common.Extends;
using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Xceed.Wpf.AvalonDock;

namespace AsNum.Xmj {
    [Export(typeof(ISheel))]
    public class SheelViewModel : Conductor<IScreen>.Collection.AllActive, ISheel {

        [ImportMany]
        private IEnumerable<Lazy<IMenuItem, IMenuItemMetadata>> MenuItems {
            get;
            set;
        }

        [ImportMany]
        public IEnumerable<Lazy<AsNum.Xmj.Common.Interfaces.ILog>> Loggers {
            get;
            set;
        }

        [Import]
        private LogObserverable LogObserverable {
            get;
            set;
        }

        public List<AsNum.Xmj.Common.Interfaces.ILog> Loggers2 {
            get {
                return this.Loggers.Select(l => l.Value).ToList();
            }
        }

        public List<ViewModes> ViewModes {
            get {
                return Enum.GetValues(typeof(AsNum.Xmj.Common.ViewModes))
                    .Cast<AsNum.Xmj.Common.ViewModes>().ToList();
            }
        }

        private BindableCollection<IMenuItem> menu = null;
        public BindableCollection<IMenuItem> Menu {
            get {
                if (menu == null) {
                    var gms = this.MenuItems
                        .Where(m => m.Metadata.TopMenuTag != TopMenuTags.None)
                        .GroupBy(m => m.Metadata.TopMenuTag)
                        .ToDictionary(g => g.Key, g => g.ToList())
                        .Select(g => {
                            var order = EnumHelper.GetAttribute<TopMenuTags, OrderAttribute>(g.Key);
                            return new TopMenuItem(EnumHelper.GetDescription<TopMenuTags>(g.Key), g.Value.Select(gg => gg.Value).ToList()) {
                                Order = order != null ? order.Order : 100
                            };
                        })
                        .OrderBy(o => o.Order)
                        .ToList();

                    var gms2 = this.MenuItems.Where(m => m.Metadata.TopMenuTag == TopMenuTags.None)
                        .Select(m => new TopMenuItem(m.Value.Header, m.Value.SubItems));

                    menu = new BindableCollection<IMenuItem>();
                    menu.AddRange(gms);
                    menu.AddRange(gms2);
                }

                return menu;

            }
        }

        public IScreen ActiveItem {
            get;
            set;
        }

        //public ViewModes ViewMode {
        //    get {
        //        return GlobalData.ViewMode;
        //    }
        //    set {
        //        GlobalData.ViewMode = value;
        //    }
        //}

        public void Show(IScreen vm, bool once = false) {
            this.ActivateItem(vm);
            this.ActiveItem = vm;
            this.NotifyOfPropertyChange("Items");
            this.NotifyOfPropertyChange("ActiveItem");
        }

        public void ScreenClosing(DocumentClosingEventArgs e) {
            var screen = e.Document.Content as VMScreenBase;
            if (screen != null && !screen.CloseAble) {
                //e.Document.CanClose = screen.CloseAble;//F
                e.Cancel = true;
                MessageBox.Show("亲，暂时不能关闭！");
            }
        }

        public SheelViewModel() {
            var vm = new StartUpViewModel();
            this.Show(vm);
        }
    }
}
