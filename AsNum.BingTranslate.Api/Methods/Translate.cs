using AsNum.BingTranslate.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.BingTranslate.Api.Methods {
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/ff512421.aspx
    /// </summary>
    public class Translate : MethodBase<string> {
        public override string MethodName {
            get {
                return "Translate";
            }
        }

        [Param("text", Required = true)]
        public string Text {
            get;
            set;
        }

        [Param("from")]
        public string From {
            get;
            set;
        }


        [Param("to", Required = true)]
        public string To {
            get;
            set;
        }


        [EnumParam("contentType", EnumUseNameOrValue.Name)]
        public ContentTypes ContentType {
            get;
            set;
        }


        public enum ContentTypes {
            [SpecifyNameValue(Name = "text/plain")]
            Plan,

            [SpecifyNameValue(Name = "text/html")]
            Html
        }
    }
}
