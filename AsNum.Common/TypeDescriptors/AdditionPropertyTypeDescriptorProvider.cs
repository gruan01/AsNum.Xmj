using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AsNum.Common.TypeDescriptors {
    public class AdditionPropertyTypeDescriptorProvider<TComponent, TProperty> : TypeDescriptionProvider where TComponent : class {

        private ICustomTypeDescriptor td = null;

        private Dictionary<string, Func<TComponent, TProperty>> Adds = new Dictionary<string, Func<TComponent, TProperty>>();

        public AdditionPropertyTypeDescriptorProvider(Dictionary<string, Func<TComponent, TProperty>> additionals)
            : base(TypeDescriptor.GetProvider(typeof(TComponent))) {
            this.Adds = additionals;
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance) {

            if(td == null) {
                td = base.GetTypeDescriptor(objectType, instance);
                td = new AdditionalPropertyTypeDescriptor<TComponent, TProperty>(td, this.Adds);
            }

            return td;

        }

    }
}
