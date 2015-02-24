using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AsNum.Common {
    public class DisconnectedMetadataTypeDescriptionProvider : TypeDescriptionProvider {
        public DisconnectedMetadataTypeDescriptor Descriptor { get; private set; }
        public DisconnectedMetadataTypeDescriptionProvider(Type type)
            : base(TypeDescriptor.GetProvider(type)) {
            this.Descriptor =
              new DisconnectedMetadataTypeDescriptor(
                base.GetTypeDescriptor(type , null) ,
                type);
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType , object instance) {
            return this.Descriptor;
        }
    }
}
