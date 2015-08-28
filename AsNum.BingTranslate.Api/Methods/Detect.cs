using AsNum.BingTranslate.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.BingTranslate.Api.Methods {

    /// <summary>
    /// 检查语种
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/ff512411.aspx
    /// </remarks>
    /// </summary>
    public class Detect : MethodBase<string> {
        public override string MethodName {
            get {
                return "Detect";
            }
        }

        [Param("text", Required = true)]
        public string Text {
            get;
            set;
        }
    }
}
