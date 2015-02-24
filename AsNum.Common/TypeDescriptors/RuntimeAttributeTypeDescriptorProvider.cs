using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AsNum.Common.TypeDescriptors {
    public class RuntimeAttributeTypeDescriptorProvider : TypeDescriptionProvider {

        private ICustomTypeDescriptor td = null;

        private Dictionary<string , List<Attribute>> PropAttrsMap;

        public RuntimeAttributeTypeDescriptorProvider(Type type , Dictionary<string , List<Attribute>> propAttrsMap)
            : this(TypeDescriptor.GetProvider(type) , propAttrsMap) {

        }

        public RuntimeAttributeTypeDescriptorProvider(TypeDescriptionProvider pd , Dictionary<string , List<Attribute>> propAttrsMap)
            : base(pd) {
            if(propAttrsMap == null)
                throw new ArgumentNullException("propAttrsMap");

            this.PropAttrsMap = propAttrsMap;
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType , object instance) {
            if(td == null) {
                td = base.GetTypeDescriptor(objectType , instance);
                td = new RuntimeAttributeTypeDescriptor(td , this.PropAttrsMap);
            }

            return td;
        }

    }
}
