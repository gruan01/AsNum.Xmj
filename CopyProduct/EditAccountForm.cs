using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CopyProduct {
    public partial class EditAccountForm : Form {

        public DestAccount Account;

        private List<ProductGroup2> Groups;
        private List<FreightTemplate> Templates;

        public EditAccountForm() {
            InitializeComponent();
        }

        public EditAccountForm(DestAccount account)
            : this() {

            this.Account = account;

            this.txtUser.Text = account.User;
            this.txtPwd.Text = account.Pwd;

            this.drpFrightTemplate.DataSource = account.FreightTemplates;
            this.drpFrightTemplate.DisplayMember = "Name";
            this.drpFrightTemplate.ValueMember = "ID";

            this.drpProductGroup.DataSource = account.ProductGroups;
            this.drpProductGroup.DisplayMember = "Name";
            this.drpProductGroup.ValueMember = "ID";

            this.Groups = account.ProductGroups;
            this.Templates = account.FreightTemplates;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            this.Account = new DestAccount() {
                User = this.txtUser.Text.Trim(),
                Pwd = this.txtPwd.Text.Trim(),
                ProductGroup = (ProductGroup2)this.drpProductGroup.SelectedItem,
                FrightTemplate = (FreightTemplate)this.drpFrightTemplate.SelectedItem,
                ProductGroups = this.Groups,
                FreightTemplates = this.Templates
            };
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            var api = new APIClient(this.txtUser.Text, this.txtPwd.Text);
            var method = new FreightTemplateList();
            var result = api.Execute(method);
            this.Templates = result.List.OrderByDescending(l => l.IsDefault).ToList();
            this.drpFrightTemplate.DataSource = this.Templates;
            this.drpFrightTemplate.DisplayMember = "Name";
            this.drpFrightTemplate.ValueMember = "ID";

            //var result2 = api.Execute(new ProductGetProductGroup());

            var gs = api.Execute(new ProductGroupList());
            var subs = gs.Where(g => g.Children != null).SelectMany(g => g.Children).ToList();
            gs.AddRange(subs);
            gs.RemoveAll(g => !g.CanChoice);

            this.Groups = gs;

            this.drpProductGroup.DataSource = Groups;
            this.drpProductGroup.DisplayMember = "NamePath";
            this.drpProductGroup.ValueMember = "ID";
        }
    }
}
