using System;
using System.ComponentModel;
using System.Resources;

namespace AsNum.Common.TypeDescriptors {
    /// <summary>
    /// 将复杂类型的属性的属性映射为类型的一个属性
    /// <remarks>
    /// 用于解决 DataGridView 在绑定复杂属性的时候，不能显示的问题
    /// </remarks>
    /// </summary>
    public class DisplayPropertyTypeDescriptionProvider : TypeDescriptionProvider {
        private ICustomTypeDescriptor td;

        private ResourceManager resManager;

        //public MyTypeDescriptionProvider()
        //    : this(TypeDescriptor.GetProvider(typeof(Order))) {
        //}

        public DisplayPropertyTypeDescriptionProvider(Type type, ResourceManager resManager)
            : this(TypeDescriptor.GetProvider(type) , resManager) { 
        }

        public DisplayPropertyTypeDescriptionProvider(TypeDescriptionProvider parent, ResourceManager resManager)
            : base(parent) {

                this.resManager = resManager;

        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType , object instance) {
            if(td == null) {
                td = base.GetTypeDescriptor(objectType , instance);
                td = new DisplayPropertyCustomTypeDescriptor(td, resManager);
            }
            return td;
        }
    }
}
