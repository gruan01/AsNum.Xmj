using System;
using System.Linq;
using System.ComponentModel;

namespace AsNum.Common {
    /// <summary>
    /// 将复杂类型的属性的属性映射为类型的一个属性
    /// <remarks>
    /// 用于解决 DataGridView 在绑定复杂属性的时候，不能显示的问题
    /// </remarks>
    /// </summary>
    public class DisplayPropertyPropertyDescriptor : PropertyDescriptor {
        private PropertyDescriptor descriptor;

        public DisplayPropertyPropertyDescriptor(PropertyDescriptor descriptor)
            : base(descriptor.Name , null) {
            this.descriptor = descriptor;
        }

        public override bool IsReadOnly {
            get {
                return false;
            }
        }
        public override void ResetValue(object component) {
        }

        public override bool CanResetValue(object component) {
            return false;
        }

        public override bool ShouldSerializeValue(object component) {
            return true;
        }

        public override Type ComponentType {
            get { return this.descriptor.ComponentType; }
        }

        public override Type PropertyType {
            get {
                return this.descriptor.PropertyType;
            }
        }

        public override object GetValue(object component) {
            return descriptor.GetValue(component);
        }

        public override void SetValue(object component , object value) {
            descriptor.SetValue(component , value);
            OnValueChanged(component , EventArgs.Empty);
        }

        protected override AttributeCollection CreateAttributeCollection() {
            var attrs = this.descriptor.Attributes.Cast<Attribute>().ToList();

            if(attrs.OfType<DisplayNameAttribute>().Count() > 0)
                attrs.RemoveAll(a => a.GetType().Equals(typeof(DisplayNameAttribute)));

            //attrs.Add(new DisplayNameAttribute("AA"));

            return new AttributeCollection(attrs.ToArray());
        }

        public override string DisplayName {
            get {
                return "AA";
            }
        }
    }
}
