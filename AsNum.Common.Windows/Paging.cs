using AsNum.Common.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AsNum.Common {

    public class PagingEventArgs : EventArgs {
        public int Page { get; set; }
    }

    public partial class Paging : UserControl {

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecord { get; private set; }

        /// <summary>
        /// 每页多少记录
        /// </summary>
        public int PerPage { get; private set; }

        private int currPage = 1;
        /// <summary>
        /// 当前页, 从1开始
        /// </summary>
        public int CurrPage {
            get {
                return this.currPage;
            }
            set {
                this.currPage = value < 1 ? 1 : value;
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; private set; }

        public Paging() {
            InitializeComponent();
        }

        public event EventHandler<PagingEventArgs> OnPage;

        public void Init(int totalRecord , int perPage) {
            this.TotalRecord = totalRecord < 0 ? 0 : totalRecord;
            this.PerPage = perPage < 0 ? 1 : perPage;
            this.TotalPage = (int)Math.Ceiling((double)this.TotalRecord / (double)this.PerPage);

            this.ResetButtons();
        }

        public void ResetButtons() {
            var btnCount = 5;

            var start = this.CurrPage - btnCount / 2 < 1 ? 1 : this.CurrPage - btnCount / 2;
            var end = start + btnCount - 1 > this.TotalPage ? this.TotalPage : start + btnCount - 1;
            if(end - start + 1 < btnCount)
                start = end - btnCount < 1 ? 1 : end - btnCount;

            var btns = Enumerable.Range(start , end - start + 1).Select(i => {
                var btn = new Button() {
                    Text = i.ToString() ,
                    Width = 25 ,
                    Padding = new Padding(2 , 0 , 2 , 0) ,
                    Enabled = i != this.CurrPage ,
                    Tag = i ,
                    AutoSize = true ,
                    Margin = new Padding(0)
                };
                btn.Click += new EventHandler(btn_Click);
                return btn;
            });

            this.flPanel.Controls.Clear();

            this.flPanel.Controls.Add(new Label() {
                Text = string.Format("{0}条数据 {1} / {2} 页" , this.TotalRecord , this.CurrPage , this.TotalPage) ,
                AutoSize = true ,
                TextAlign = ContentAlignment.MiddleCenter
            });

            if(this.CurrPage > 1) {
                var firstBtn = new Button() {
                    Image = Resources.First ,
                    Width = 25 ,
                    Tag = 0,
                    Margin = new Padding(0)
                };
                this.flPanel.Controls.Add(firstBtn);
                var preBtn = new Button() {
                    Image = Resources.Pre ,
                    Width = 25 ,
                    Tag = this.CurrPage - 1,
                    Margin = new Padding(0)
                };
                this.flPanel.Controls.Add(preBtn);
                preBtn.Click += new EventHandler(btn_Click);
                firstBtn.Click += new EventHandler(btn_Click);
            }

            this.flPanel.Controls.AddRange(btns.ToArray());

            if(this.CurrPage < this.TotalPage) {
                var nextBtn = new Button() {
                    Image = Resources.Next ,
                    Width = 25 ,
                    Tag = this.CurrPage + 1,
                    Margin = new Padding(0)
                };
                this.flPanel.Controls.Add(nextBtn);

                var lastBtn = new Button() {
                    Image = Resources.Last ,
                    Width = 25 ,
                    Tag = this.TotalPage ,
                    Margin = new Padding(0)
                };
                this.flPanel.Controls.Add(lastBtn);
                nextBtn.Click += new EventHandler(btn_Click);
                lastBtn.Click += new EventHandler(btn_Click);
            }
        }

        void btn_Click(object sender , EventArgs e) {
            var page = (int)((Button)sender).Tag;
            this.CurrPage = page;
            if(this.OnPage != null)
                OnPage(this , new PagingEventArgs() {
                    Page = page
                });

            this.ResetButtons();
        }
    }
}
