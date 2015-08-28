using AsNum.BingTranslate.Api;
using AsNum.BingTranslate.Api.Methods;
using AsNum.Xmj.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AsNum.Common.Extends;

namespace AsNum.Xmj.Translator.ViewModels {
    public class TranslatorViewModel : VMScreenBase {
        public override string Title {
            get { return "翻译"; }
        }

        public string Source { get; set; }

        public string Result { get; set; }

        public Dictionary<string, string> Langs { get; set; }

        public string Target { get; set; }

        public TranslatorViewModel() {
            this.Target = "en";
            Task.Run(() => this.LoadLans());
        }

        private async void LoadLans() {
            Dictionary<string, string> dic = null;
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AsNum.Xmj"); ;
            var path = Path.Combine(dir, "BingTranslateLang");
            if (File.Exists(path)) {
                var bf = new BinaryFormatter();
                using (var stm = File.OpenRead(path)) {
                    dic = (Dictionary<string, string>)bf.Deserialize(stm);
                }
            }

            if (dic == null) {
                dic = new Dictionary<string, string>();
                var method = new GetLanguagesForTranslate();
                var lans = await ApiClient.ExecuteWrap(method);

                lans.ForEach(c => {
                    try {
                        var culture = CultureInfo.GetCultureInfo(c);
                        dic.Set(c, string.Format("{0}\r\n{1}", culture.EnglishName, culture.DisplayName));
                    } catch {
                        dic.Set(c, c);
                    }
                });

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var bf = new BinaryFormatter();
                using (var stm = File.Open(path, FileMode.OpenOrCreate)) {
                    bf.Serialize(stm, dic);
                }
            }

            this.Langs = dic;
            this.NotifyOfPropertyChange(() => this.Langs);
        }

        public async void Translate() {
            if (string.IsNullOrWhiteSpace(this.Source))
                return;

            var method = new Translate() {
                Text = this.Source.Trim(),
                To = this.Target
            };
            this.Result = await ApiClient.ExecuteWrap(method);
            this.NotifyOfPropertyChange(() => this.Result);
        }
    }
}
