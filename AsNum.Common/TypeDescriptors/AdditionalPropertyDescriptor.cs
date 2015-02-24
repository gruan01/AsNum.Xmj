using System;
using System.ComponentModel;

namespace AsNum.Common.TypeDescriptors {
    public class AdditionalPropertyDescriptor<TComponent, TProperty> : PropertyDescriptor where TComponent : class {

        private Func<TComponent, TProperty> fun;

        //private object Value = null;

        public AdditionalPropertyDescriptor(string propertyName, Func<TComponent, TProperty> func)
            : base(propertyName, null) {
            this.fun = func;
        }

        public override bool CanResetValue(object component) {
            return false;
        }

        public override Type ComponentType {
            get { return typeof(TComponent); }
        }

        public override object GetValue(object component) {
            return this.fun(component as TComponent);
        }

        public override bool IsReadOnly {
            get { return true; }
        }

        public override Type PropertyType {
            get { return typeof(TProperty); }
        }

        public override void ResetValue(object component) {

        }

        public override void SetValue(object component, object value) {
            
        }

        public override bool ShouldSerializeValue(object component) {
            return false;
        }
    }
}
