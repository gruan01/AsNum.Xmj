using System;
using System.ComponentModel;

namespace AsNum.Common.TypeDescriptors {
    /// <summary>
    /// 将复杂类型的属性的属性映射为类型的一个属性
    /// <remarks>
    /// 用于解决 DataGridView 在绑定复杂属性的时候，不能显示的问题
    /// </remarks>
    /// </summary>
    public class SubLevelPropertyDescriptor : PropertyDescriptor {
        private PropertyDescriptor newPropertyDescriptor;
        private PropertyDescriptor orgPropertyDescriptor;

        public SubLevelPropertyDescriptor(PropertyDescriptor orgPD , PropertyDescriptor newPD , string newName)
            : base(newName , null) {
            newPropertyDescriptor = newPD;
            orgPropertyDescriptor = orgPD;
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
            get { return orgPropertyDescriptor.ComponentType; }
        }

        public override Type PropertyType {
            get {
                return newPropertyDescriptor.PropertyType;
            }
        }

        public override object GetValue(object component) {
            return newPropertyDescriptor.GetValue(orgPropertyDescriptor.GetValue(component));
        }

        public override void SetValue(object component , object value) {
            newPropertyDescriptor.SetValue(orgPropertyDescriptor.GetValue(component) , value);
            OnValueChanged(component , EventArgs.Empty);
        }

        public override AttributeCollection Attributes {
            get {
                return newPropertyDescriptor.Attributes;
            }
        }
    }
}
