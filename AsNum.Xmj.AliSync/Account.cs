using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AsNum.Xmj.AliSync {
    [Serializable]
    public class Account : IDataErrorInfo {

        [MinLength(2)]
        [Display(Name = "账户", Order = 1)]
        public string User { get; set; }

        private string pwd = "";
        [StringLength(20, MinimumLength = 5), Required(AllowEmptyStrings = false)]
        [Display(Name = "密码", Order = 0)]
        public string Pwd {
            get {
                return this.pwd;
            }
            set {
                this.pwd = value;
            }
        }

        [Browsable(false)]
        public string Error {
            get {
                var errors = TypeDescriptor.GetProperties(this).Cast<PropertyDescriptor>()
                    .Select(p => this[p.Name])
                    .Where(s => !string.IsNullOrEmpty(s));

                return string.Join(Environment.NewLine, errors);
            }
        }

        public string this[string columnName] {
            get {
                var pd = TypeDescriptor.GetProperties(this).Find(columnName, false);
                var vs = pd.Attributes.OfType<ValidationAttribute>();
                if (vs.Count() > 0) {
                    var value = pd.GetValue(this);

                    var msgs = vs.Where(v => !v.IsValid(value))
                        .Select(v => string.IsNullOrWhiteSpace(v.ErrorMessage) ? v.FormatErrorMessage(columnName) : v.ErrorMessage);

                    return string.Join(Environment.NewLine, msgs);
                }
                return null;
            }
        }
    }
}
