using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AsNum.Common.TypeDescriptors {
    /// <summary>
    /// 将复杂类型的属性的属性映射为类型的一个属性
    /// <remarks>
    /// 用于解决 DataGridView 在绑定复杂属性的时候，不能显示的问题
    /// </remarks>
    /// </summary>
    public class SubLevelTypeDescriptionProvider : TypeDescriptionProvider {
        private ICustomTypeDescriptor typeDescriptor;

        // TypeDescriptionProvider 可以通过 TypeDescriptionProviderAttribute 直接加到实体类上，
        // 但是那样的话，就没有办法加所需要个性化的参数了。
        //public MyTypeDescriptionProvider()
        //    : this(TypeDescriptor.GetProvider(typeof(Order))) {
        //}

        /// <summary>
        /// 需要映射的子属性路径
        /// </summary>
        private List<string> SubProperties = null;

        public SubLevelTypeDescriptionProvider(Type type , List<string> subProperties = null)
            : this(TypeDescriptor.GetProvider(type) , subProperties) {
        }

        public SubLevelTypeDescriptionProvider(TypeDescriptionProvider parent , List<string> subProperties = null)
            : base(parent) {
            this.SubProperties = subProperties;
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType , object instance) {
            if(typeDescriptor == null) {
                typeDescriptor = base.GetTypeDescriptor(objectType , instance);
                typeDescriptor = new SubLevelCustomTypeDescriptor(typeDescriptor , this.SubProperties);
            }
            return typeDescriptor;
        }
    }
}
