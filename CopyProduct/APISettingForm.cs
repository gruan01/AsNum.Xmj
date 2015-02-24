using AsNum.Common.Extends;
using AsNum.Xmj.API;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace CopyProduct {
    public partial class APISettingForm : Form {
        public APISettingForm() {
            InitializeComponent();
            this.LoadSetting();
        }

        private void LoadSetting() {
            this.txtAppKey.Text = ConfigurationManager.AppSettings.Get(ConstValue.AppKeyAppSettingKey, "");
            this.txtSecretKey.Text = ConfigurationManager.AppSettings.Get(ConstValue.SECKeyAppSettingKey, "");
        }

        private void button1_Click(object sender, EventArgs e) {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfg.AppSettings.Settings.Add(ConstValue.AppKeyAppSettingKey, this.txtAppKey.Text);
            cfg.AppSettings.Settings.Add(ConstValue.SECKeyAppSettingKey, this.txtSecretKey.Text);
            AppSettingHelper.Save(cfg);
            Application.Restart();
        }
    }
}
