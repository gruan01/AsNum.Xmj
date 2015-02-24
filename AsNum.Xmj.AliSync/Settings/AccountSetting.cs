using AsNum.Common.Extends;
using AsNum.Xmj.AliSync.Views;
using AsNum.Xmj.Common.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AsNum.Xmj.AliSync.Settings {
    [DisplayName("Aliexpress 账户设置")]
    public class AccountSetting : PropertyChangedBase, ISettingItem<ObservableCollection<Account>> {

        private ObservableCollection<Account> accounts = null;

        public ObservableCollection<Account> DefaultValue {
            get { return new ObservableCollection<Account>(); }
        }

        public ObservableCollection<Account> Value {
            get {
                if (this.accounts == null)
                    this.accounts = new ObservableCollection<Account>();
                return this.accounts;
            }
            set {
                this.accounts = value;
                this.NotifyOfPropertyChange("Value");
            }
        }

        public string Key {
            get { return ""; }
        }

        public Type ValueType {
            get { return typeof(List<Account>); }
        }

        public void Save() {
            PersistFileHelper.Save(this.Value, "accounts");
        }

        public AccountSetting() {
            this.Value = PersistFileHelper.Load<ObservableCollection<Account>>("accounts");
        }

        public Type EditorType {
            get { return typeof(AccountEditorView); }
        }

        public void Delete(Account acc) {
            this.Value.Remove(acc);
        }
    }
}
