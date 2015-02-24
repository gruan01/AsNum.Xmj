using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

namespace AsNum.Xmj.Common {
    public abstract class AppSettingBase<T> : PropertyChangedBase, ISettingItem<T> {

        public virtual string Key {
            get {
                return this.GetType().FullName;
            }
        }

        public virtual T DefaultValue {
            get {
                return default(T);
            }
        }

        private T value;

        public T Value {
            get {
                if (this.value == null)
                    this.value = (T)this.DefaultValue;
                return this.value;
            }
            set {
                this.value = (T)value;
            }
        }

        public AppSettingBase() {
            var v = ConfigurationManager.AppSettings[this.Key];
            if (v != null)
                this.Value = (T)Convert.ChangeType(v, typeof(T));
            else
                this.Value = (T)this.DefaultValue;
        }

        public virtual void Save() {

            if (this.Value == null)
                return;


            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (cfg.AppSettings.Settings[this.Key] != null) {
                cfg.AppSettings.Settings[this.Key].Value = this.Value.ToString();
            } else {
                cfg.AppSettings.Settings.Add(this.Key, this.Value.ToString());
            }

            cfg.Save(ConfigurationSaveMode.Modified, false);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public string Description {
            get {
                var desc = (DescriptionAttribute)this.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
                return desc != null ? desc.Description : "";
            }
        }

        public string DisplayName {
            get {
                var dn = (DisplayNameAttribute)this.GetType().GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault();
                return dn != null ? dn.DisplayName : this.GetType().Name;
            }
        }



        public Type ValueType {
            get {
                return typeof(T);
            }
        }

        public Type EditorType {
            get {
                return null;
            }
        }
    }
}
