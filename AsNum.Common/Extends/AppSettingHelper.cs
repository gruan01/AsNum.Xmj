using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AsNum.Common.Extends {
    public class AppSettingHelper {

        private static List<AppSettingItemBase> Items = new List<AppSettingItemBase>();

        public static T GetItem<T>() where T : AppSettingItemBase, new() {
            T t;
            if(Items.OfType<T>().Count() == 0) {
                t = new T();
                Items.Add(t);
            } else
                t = Items.OfType<T>().First();

            return t;
        }

        public static void Save(Configuration cfg) {
            Items.ForEach(i => {
                i.Save(cfg);
            });
            cfg.Save(ConfigurationSaveMode.Modified, false);
        }
    }
}
