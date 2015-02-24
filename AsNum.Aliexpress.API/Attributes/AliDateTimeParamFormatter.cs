using System;

namespace AsNum.Xmj.API.Attributes {
    internal class AliDateTimeParamFormatter : ParamFormaterAttribute {

        public string FormatString { get; set; }
        public string WhenNull { get; set; }

        public AliDateTimeParamFormatter(string format, string whenNull = "")
            : base(typeof(DateTime)) {
            this.FormatString = format;
            this.WhenNull = whenNull;
        }

        public override string Format(object obj) {
            var o = obj as DateTime?;
            if(!o.HasValue) {
                return this.WhenNull;
            } else {
                return o.Value.ToString(this.FormatString);
            }

        }
    }
}
