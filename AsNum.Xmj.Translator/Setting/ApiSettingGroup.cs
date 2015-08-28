using AsNum.Xmj.Common;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Common.SettingEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.Translator.Setting {
    [Export(typeof(ISettingGroup))]
    public class AccountSettingGroup : ISettingGroup {
        public SettingCategories Category {
            get { return SettingCategories.Normal; }
        }

        public string GroupName {
            get { return "Bing 翻译设定"; }
        }

        public string Desc {
            get { return ""; }
        }

        private ICollection<ISettingEditor> items = new List<ISettingEditor>() {
            new StringEditorViewModel(new ClientID()),
            new StringEditorViewModel(new SecretCode())
        };

        public ICollection<Common.Interfaces.ISettingEditor> Items {
            get {
                return items;
            }
            set {
                items = value;
            }
        }
    }

    [Description("Client ID"), DisplayName("Client ID")]
    public class ClientID : AppSettingBase<string> {
        public override string Key {
            get {
                return ConstValue.ClientIDAppSettingKey;
            }
        }
    }


    [Description("SecretCode"), DisplayName("Secret Code")]
    public class SecretCode : AppSettingBase<string> {
        public override string Key {
            get {
                return ConstValue.SECKeyAppSettingKey;
            }
        }
    }
}
