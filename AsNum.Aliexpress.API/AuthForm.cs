using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsNum.Xmj.API {
    public partial class AuthForm : Form {

        public string Code { get; private set; }

        public AuthForm(string url) {
            InitializeComponent();

            this.wb1.Navigate(url, false);
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Code = this.txtCode.Text.Trim();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
