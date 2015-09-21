using System;

namespace AsNum.Xmj.API.Attributes {

    [Obsolete("", true)]
    public class SpecifyValueAttribute : Attribute {

        public object Value {
            get;
            set;
        }

        public SpecifyValueAttribute(object value) {
            this.Value = value;
        }

    }
}
