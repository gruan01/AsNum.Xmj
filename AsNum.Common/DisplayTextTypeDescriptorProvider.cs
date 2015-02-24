using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AsNum.Common {
    public class DisplayTextTypeDescriptorProvider : TypeDescriptionProvider {

        // AssociatedMetadataTypeTypeDescriptor 是 internal 的

        public DisplayTextTypeDescriptorProvider(Type type)
            : base(TypeDescriptor.GetProvider(type)) {

        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType , object instance) {
            var td = base.GetTypeDescriptor(objectType , instance);
            var attr = td.GetAttributes();
            return new DisplayTextTypeDescriptor(td);
        }

    }
}
