using System;

namespace AsNum.Xmj.API.Attributes {
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
