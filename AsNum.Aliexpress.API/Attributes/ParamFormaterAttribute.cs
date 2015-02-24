using System;

namespace AsNum.Xmj.API.Attributes {

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public abstract class ParamFormaterAttribute : Attribute {

        private Type ObjectType { get; set; }

        public ParamFormaterAttribute(Type type) {
            this.ObjectType = type;
        }

        public abstract string Format(object obj);

        protected virtual bool CheckType(object obj) {
            return obj.GetType().IsSubclassOf(this.ObjectType);
        }
    }
}
