using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.BingTranslate.Api.Methods {
    public class GetLanguagesForSpeak : MethodBase<List<string>> {
        public override string MethodName {
            get {
                return "GetLanguagesForSpeak";
            }
        }
    }
}
