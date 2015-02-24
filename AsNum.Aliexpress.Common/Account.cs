using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Aliexpress.Common {
    [Serializable]
    public class Account {

        [MinLength(2)]
        [Display(Name = "账户", Order = 1)]
        public string User {
            get;
            set;
        }

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
    }
}
