using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AsNum.Common.TypeDescriptors {
    public class RuntimeAttributeTypeDescriptor : CustomTypeDescriptor {

        private Dictionary<string , List<Attribute>> PropertyAttrsMap;

        public RuntimeAttributeTypeDescriptor(ICustomTypeDescriptor parent , Dictionary<string , List<Attribute>> propAttrsMap)
            : base(parent) {

            if(propAttrsMap == null)
                throw new ArgumentException("propAttrsMap");

            this.PropertyAttrsMap = propAttrsMap;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes) {

            //base.GetProperties() 和 base.GetProperties(attributes) 返回的结果不一样
            //var props = base.GetProperties().Cast<PropertyDescriptor>();
            var props = base.GetProperties(attributes).Cast<PropertyDescriptor>();
            List<PropertyDescriptor> dpps = new List<PropertyDescriptor>();
            foreach(var prop in props) {
                if(this.PropertyAttrsMap.ContainsKey(prop.Name)) {
                    var newAttrs = this.PropertyAttrsMap[prop.Name];
                    if(newAttrs == null) {
                        dpps.Add(prop);
                        continue;
                    } else {
                        var attrs = prop.Attributes.Cast<Attribute>().ToList();
                        attrs.AddRange(newAttrs);

                        dpps.Add(TypeDescriptor.CreateProperty(prop.ComponentType , prop , attrs.ToArray()));
                    }
                } else {
                    dpps.Add(prop);
                }
            }

            return new PropertyDescriptorCollection(dpps.ToArray());
        }

    }
}
