using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AsNum.Common.TypeDescriptors {
    public class AdditionalPropertyTypeDescriptor<TComponent, TProperty> : CustomTypeDescriptor where TComponent : class {

        private Dictionary<string, Func<TComponent, TProperty>> additionals = new Dictionary<string, Func<TComponent, TProperty>>();

        public AdditionalPropertyTypeDescriptor(ICustomTypeDescriptor parent, Dictionary<string, Func<TComponent, TProperty>> additionals)
            : base(parent) {
            this.additionals = additionals;
        }

        //这里居然会影响EF
        //public override PropertyDescriptorCollection GetProperties() {
        //    return this.GetProperties(null);
        //}

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes) {

            var props = base.GetProperties(attributes).Cast<PropertyDescriptor>().ToList();

            if(this.additionals != null) {
                foreach(var a in this.additionals) {
                    //var p = TypeDescriptor.CreateProperty(typeof(TComponent), a.Key, typeof(TProperty));
                    var p = new AdditionalPropertyDescriptor<TComponent, TProperty>(a.Key, a.Value);
                    props.Add(p);
                }
            }

            return new PropertyDescriptorCollection(props.ToArray());
        }

    }
}
