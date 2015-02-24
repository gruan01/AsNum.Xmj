using AsNum.Common.Extends;
using AsNum.Xmj.API;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace AsNum.Xmj.APITestTool {
    public partial class SettingForm : Form {
        public SettingForm() {
            InitializeComponent();
            this.LoadConfig();
        }

        private void LoadConfig() {
            this.txtAppKey.Text = ConfigurationManager.AppSettings.Get(ConstValue.AppKeyAppSettingKey, "");
            this.txtSecretKey.Text = ConfigurationManager.AppSettings.Get(ConstValue.AppKeyAppSettingKey, "");
        }

        private void button1_Click(object sender, EventArgs e) {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var appKey = new APPKey();
            appKey.Value = this.txtAppKey.Text;
            appKey.Save();

            var secKey = new APPSecretCode();
            secKey.Value = this.txtSecretKey.Text;
            secKey.Save();

            //AppSettingHelper.GetItem<AppKeySettingItem>().Value = this.txtAppKey.Text;
            //AppSettingHelper.GetItem<SecretKeySettingItem>().Value = this.txtSecretKey.Text;
            //AppSettingHelper.Save(cfg);
            Application.Restart();
        }
    }
}
