using AsNum.BingTranslate.Api;
using AsNum.BingTranslate.Api.Methods;
using AsNum.Xmj.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.Translator.ViewModels {
    public class TranslatorViewModel : VMScreenBase {
        public override string Title {
            get { return "翻译"; }
        }

        public string Source { get; set; }

        public string Result { get; set; }

        public async void Translate() {
            var method = new Translate() {
                Text = this.Source.Trim(),
                To = "en"
            };
            this.Result = await ApiClient.ExecuteWrap(method);
            this.NotifyOfPropertyChange(() => this.Result);
        }
    }
}
