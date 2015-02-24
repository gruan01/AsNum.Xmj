using AsNum.Common;
using AsNum.Common.Extends;
using AsNum.Common.Security;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.Common.Interfaces;
using Fizzler.Systems.HtmlAgilityPack;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyProduct {
    public partial class Form1 : Form {

        private SortableBindingList<DestAccount> DestAccounts = new SortableBindingList<DestAccount>();

        [Dependency]
        public ILog Logger {
            get;
            set;
        }

        public Form1() {
            InitializeComponent();
            this.dgDestAccounts.DataSource = this.DestAccounts;
            var logger = this.Logger;
            TypeDescriptorHelper.SetNotBrowserableForComplexProperty<DestAccount>();
            TypeDescriptorHelper.MappingSubLevelProperty<DestAccount>(a => a.ProductGroup.Name, a => a.FrightTemplate.Name);
            TypeDescriptorHelper.RegistMetadata<Product, ProductMetadata>();
        }

        private void button1_Click(object sender, EventArgs e) {
            var form = new EditAccountForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                this.DealAccount(form.Account);
            }
        }

        private void aPI设置ToolStripMenuItem_Click(object sender, EventArgs e) {
            var form = new APISettingForm();
            form.ShowDialog();
        }

        private void dgDestAccounts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e) {
            var destAccount = (DestAccount)this.dgDestAccounts.Rows[e.RowIndex].DataBoundItem;
            if (destAccount != null) {
                var form = new EditAccountForm(destAccount);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    this.DealAccount(form.Account);
                }
            }
        }

        private void DealAccount(DestAccount account) {
            var da = this.DestAccounts.FirstOrDefault(a => a.User.Equals(account.User));
            if (da != null)
                this.DestAccounts.Remove(da);
            this.DestAccounts.Add(account);
        }

        private void button2_Click(object sender, EventArgs e) {
            var api = new APIClient(this.txtSourceAccountUser.Text.Trim(), this.txtSourceAccountPwd.Text);
            var method = new ProductFindById() {
                ProductID = this.txtSourceProductID.Text.Trim()
            };
            var result = api.Execute(method);
            this.ppgProduct.SelectedObject = result;
        }

        private string GetSavePath(string url) {
            return string.Format("{0}.jpg", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TmpPic", url.To16bitMD5()));
        }

        private void button3_Click(object sender, EventArgs e) {

            //this.btnAddAccount.Enabled = false;
            //this.btnGetDetail.Enabled = false;
            //this.btnPublish.Enabled = false;

            this.Controls.Cast<Control>().ToList().ForEach((c) => {
                c.InvokeIfNeed(() => {
                    c.Enabled = false;
                });
            });

            var product = (Product)this.ppgProduct.SelectedObject;
            var imgs = product.ImageUrls.Split(new char[] { ';' });

            Task.Factory.StartNew(() => {
                foreach (var img in imgs) {
                    Task.Factory.StartNew((url) => {
                        this.DownloadPic((string)url);
                    }, img, TaskCreationOptions.AttachedToParent);
                }
            })
            .ContinueWith((task) => {
                foreach (var a in this.DestAccounts.Where(d => d.IsChecked)) {
                    Task.Factory.StartNew((account) => {
                        Publish((DestAccount)account);
                    }, a, TaskCreationOptions.AttachedToParent);
                }
            })
            .ContinueWith((task) => {
                var msg = task.Exception.GetBaseException().StackTrace;
                this.Log(msg);
                MessageBox.Show(task.Exception.GetBaseException().Message);
            }, TaskContinuationOptions.OnlyOnFaulted)
            .ContinueWith((task) => {
                //this.btnAddAccount.InvokeIfNeed(() => {
                //    this.btnAddAccount.Enabled = true;
                //    this.btnGetDetail.Enabled = true;
                //    this.btnPublish.Enabled = true;
                //});
                this.Controls.Cast<Control>().ToList().ForEach((c) => {
                    c.InvokeIfNeed(() => {
                        c.Enabled = true;
                    });
                });
            });
        }

        private void DownloadPic(string url) {
            this.Log("开始下载图片{0}", url);
            var savePath = this.GetSavePath(url);
            var dir = Directory.GetParent(savePath).FullName;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (!File.Exists(savePath)) {
                var client = new WebClient();
                client.DownloadFile(url, savePath);
            }
        }

        private string UploadImage(APIClient api, string file) {
            this.Log("上传图片 {0} 到账户 {1}", file, api.AuthUser);
            var method = new PhotoBankUploadImage() {
                FilePath = file
            };
            var result = api.Execute(method);
            if (result.Success) {
                return result.Url;
            } else
                return null;
        }

        private string UploadImage2(APIClient api, string file) {
            this.Log("上传临时图片 {0} 到账户 {1}", file, api.AuthUser);
            var method = new PhotoBankUploadTempImage() {
                FilePath = file
            };
            var result = api.Execute(method);
            if (result.Success)
                return result.Url;
            else
                return null;
        }

        private void ReplaceImage(APIClient api, Product product) {
            List<string> uimgs = new List<string>();
            //产品图片
            var imgs = ((Product)this.ppgProduct.SelectedObject).ImageUrls.Split(';');
            foreach (var img in imgs) {
                var file = this.GetSavePath(img);
                var t = this.UploadImage(api, file);
                if (t != null) {
                    uimgs.Add(t);
                }
            }
            product.ImageUrls = string.Join(";", uimgs);

            //自定义颜色里的图片
            //只能用api.uploadTempImage 接口返回的图片,造
            foreach (var s in product.SKUs) {
                s.Property.Where(p => !string.IsNullOrEmpty(p.SkuImage)).ToList()
                    .ForEach(p => {
                        this.DownloadPic(p.SkuImage);
                        var result = this.UploadImage2(api, this.GetSavePath(p.SkuImage));
                        p.SkuImage = result;
                    });
            }

            //说明里的图片
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(product.Detail);
            var imgs2 = doc.DocumentNode
                .QuerySelectorAll("img")
                .Select(i => i.Attributes["src"].Value)
                .Where(i => !string.IsNullOrEmpty(i))
                .Distinct();

            var dic = new Dictionary<string, string>();
            foreach (var img in imgs2) {
                this.DownloadPic(img);
                var newUrl = this.UploadImage(api, this.GetSavePath(img));
                dic[img] = newUrl;
            }

            foreach (var img in dic) {
                product.Detail = product.Detail.Replace(img.Key, img.Value);
            }
        }

        private void Publish(DestAccount account) {
            this.Log("偿试发布到账户:{0}", account.User);
            var api = new APIClient(account.User, account.Pwd);
            var product = (this.ppgProduct.SelectedObject as Product).SerializeCopy();
            this.ReplaceImage(api, product);

            //替换产品组和运费模板
            product.ProductGroup = account.ProductGroup.ID;
            product.FreightTemplateID = account.FrightTemplate.ID;

            var method = new ProductNewProduct() {
                ProductDetail = product
            };

            var result = api.Execute(method);
            if (result.Success) {
                MessageBox.Show(string.Format("Copy 到 {0} 成功", account.User));
            } else {
                MessageBox.Show(method.ResultString, string.Format("Copy 到 {0} 失败", account.User));
            }

            //Thread.CurrentThread.Interrupt();
        }

        private void Log(string format, params object[] args) {
            var m = string.Format(format, args);
            if (this.Logger != null) {
                this.Logger.Log(m);
            }
        }
    }
}
