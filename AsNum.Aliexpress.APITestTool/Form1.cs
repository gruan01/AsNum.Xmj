using AsNum.Common.Extends;
using AsNum.Xmj.API.Methods;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AsNum.Xmj.APITestTool {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            TypeDescriptorHelper.RegistMetadata<ProductNewProduct, ProductNewProductMetadata>();

            this.LoadMethods();
            this.cmbMethods.SelectedIndexChanged += cmbMethods_SelectedIndexChanged;
        }

        void cmbMethods_SelectedIndexChanged(object sender, EventArgs e) {
            this.LoadProperties();
        }

        private void LoadProperties() {
            var type = (Type)this.cmbMethods.SelectedValue;
            var method = Activator.CreateInstance(type);
            this.propGrid.SelectedObject = method;

            this.propGrid.ExpandAllGridItems();
        }

        private void LoadMethods() {
            //先使用其中的一个类，后面才找到
            var method = new AsNum.Xmj.API.Methods.OrderFindByID();

            var b = AppDomain.CurrentDomain.GetAssemblies();
            var ms = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(AsNum.Xmj.API.MethodBase))))
                .Select(t => new { K = t.Name, V = t })
                .OrderBy(t => t.K)
                .ToList();

            this.cmbMethods.DataSource = ms;
            this.cmbMethods.DisplayMember = "K";
            this.cmbMethods.ValueMember = "V";

            this.LoadProperties();
        }

        private void btnExecute_Click(object sender, EventArgs e) {
            var api = new API.APIClient(this.txtUser.Text, this.txtPwd.Text);
            var type = this.cmbMethods.SelectedValue as Type;
            var method = this.propGrid.SelectedObject as AsNum.Xmj.API.MethodBase;
            //this.txtResult.Text = api.GetResult(method);
            this.jsonViewer1.Json = api.GetResult(method).Result;
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e) {
            var form = new SettingForm();
            form.ShowDialog();
        }
    }
}
