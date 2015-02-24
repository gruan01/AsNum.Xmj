using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AsNum.Common {
    public class DisplayTextTypeDescriptor : CustomTypeDescriptor {

        public DisplayTextTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent) {
           
        }

        public override PropertyDescriptorCollection GetProperties() {
            return base.GetProperties();
        }

        public override AttributeCollection GetAttributes() {
            var attrs = base.GetAttributes();

            return attrs;
        }
    }
}
