using System.Windows.Forms;

namespace AsNum.Common.PropertyEditor {
    public partial class HtmlEditor : Form {

        public string Ctx {
            get {
                return this.wb1.DocumentText;
            }
            set {
                this.wb1.DocumentText = value;
            }
        }

        public HtmlEditor(string ctx) {
            InitializeComponent();

            this.wb1.DocumentCompleted += wb1_DocumentCompleted;
            this.Ctx = string.Format("{0}<script>function designOn(){{document.designMode='on';}}</script>", ctx);
        }

        void wb1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            //this.wb1.Document
            //HtmlElement ele = this.wb1.Document.CreateElement("script");
            //ele.SetAttribute("type", "text/javascript");
            //ele.SetAttribute("text", "document.designMode='on'");
            //this.wb1.Document.Body.AppendChild(ele);
            this.wb1.Document.InvokeScript("designOn");
        }
    }
}
