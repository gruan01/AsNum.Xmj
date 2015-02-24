using System;
using System.Configuration;

namespace AsNum.Common {

    public abstract class AppSettingItemBase {
        public abstract string Key { get; }
        protected string StringValue { get; set; }

        public virtual void Save(Configuration cfg) {
            if(cfg.AppSettings.Settings[this.Key] != null) {
                cfg.AppSettings.Settings[this.Key].Value = this.StringValue;
            } else {
                cfg.AppSettings.Settings.Add(this.Key, this.StringValue);
            }

            cfg.Save(ConfigurationSaveMode.Modified);
        }
    }

    public abstract class AppSettingItemBase<T> : AppSettingItemBase {

        protected virtual string KeyPrefix {
            get {
                return this.GetType().Namespace;
            }
        }

        public abstract string SubKey { get; }

        public override string Key {
            get {
                return string.Format("{0}.{1}", this.KeyPrefix, this.SubKey);
            }
        }

        private object settingValue = null;
        public virtual T Value {
            get {
                if(this.settingValue == null && ConfigurationManager.AppSettings[this.Key] != null) {
                    try {
                        this.settingValue = (T)Convert.ChangeType(ConfigurationManager.AppSettings[Key], typeof(T));
                    } catch {
                        this.settingValue = this.DefaultValue;
                    }
                } else if(this.settingValue == null) {
                    this.settingValue = this.DefaultValue;
                }
                return (T)this.settingValue;
            }
            set {
                base.StringValue = value.ToString();
                this.settingValue = value;
            }
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public virtual T DefaultValue { get; set; }
    }
}
