using AsNum.Xmj.API;
using AsNum.Xmj.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using AsNum.Common.Extends;
using Caliburn.Micro;
using System.Windows;

namespace AsNum.Xmj.ManualAuth {
    public class AuthViewModel : VMScreenBase {
        public override string Title
        {
            get
            {
                return "手动认证";
            }
        }

        private string displayName = "手动认证";
        new public string DisplayName
        {
            get { return this.displayName; }
            set { this.displayName = value; }
        }

        public string Url
        {
            get
            {
                return Auth.AuthUrl;
            }
        }

        public string Code { get; set; }
        public string Account { get; set; }

        public void Navigating(NavigatingCancelEventArgs e) {
            var code = e.Uri.AbsoluteUri.ParseString("code", true);
            if (!string.IsNullOrEmpty(code)) {
                this.Code = code;
                this.NotifyOfPropertyChange(() => this.Code);
            }
        }

        public void SureAuth() {
            if (!string.IsNullOrWhiteSpace(this.Account) && !string.IsNullOrWhiteSpace(this.Code)) {
                var client = new APIClient(this.Account, "", this.Code);
                MessageBox.Show("授权成功");
            }
        }
    }
}
