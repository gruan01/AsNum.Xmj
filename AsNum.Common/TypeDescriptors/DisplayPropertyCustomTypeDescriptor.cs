using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Resources;

namespace AsNum.Common.TypeDescriptors {


    /// <summary>
    /// 将复杂类型的属性的属性映射为类型的一个属性
    /// <remarks>
    /// 用于解决 DataGridView 在绑定复杂属性的时候，不能显示的问题
    /// </remarks>
    /// </summary>
    public class DisplayPropertyCustomTypeDescriptor : CustomTypeDescriptor {

        public ResourceManager ResManager { get; set; }

        public DisplayPropertyCustomTypeDescriptor(ICustomTypeDescriptor parent, ResourceManager resManager)
            : base(parent) {

            this.ResManager = resManager;
        }

        //public override PropertyDescriptorCollection GetProperties() {
        //    var props = base.GetProperties().Cast<PropertyDescriptor>();
        //    List<PropertyDescriptor> dpps = new List<PropertyDescriptor>();
        //    var ns = base.GetClassName().Replace("." , "");
        //    foreach(var prop in props) {

        //        //dpps.Add(prop);
        //        //dpps.Add(new DisplayPropertyPropertyDescriptor(prop));

        //        var attrs = prop.Attributes.Cast<Attribute>().ToList();
        //        //var dKey = string.Format("{0}_{1}_DisplayName" , ns , prop.Name);
        //        //var displayName = this.ResManager.GetString(dKey);
        //        //if(!string.IsNullOrWhiteSpace(displayName)) {
        //        //    attrs.Add(new DisplayNameAttribute(displayName));
        //        //}
        //        //var descKey = string.Format("{0}_{1}_Description" , ns , prop.Name);
        //        //var desc = this.ResManager.GetString(descKey);
        //        //if(!string.IsNullOrWhiteSpace(descKey)) {
        //        //    attrs.Add(new DescriptionAttribute(desc));
        //        //}

        //        var attr = attrs.OfType<DisplayNameAttribute>().FirstOrDefault();
        //        if(attr != null) {
        //            attrs.RemoveAll(a => a.GetType().Equals(typeof(DisplayNameAttribute)));
        //            attrs.Add(new DisplayNameAttribute("AA"));
        //        }

        //        dpps.Add(TypeDescriptor.CreateProperty(prop.ComponentType , prop , attrs.ToArray()));
        //    }
        //    return new PropertyDescriptorCollection(dpps.ToArray());
        //}

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
            var props = base.GetProperties().Cast<PropertyDescriptor>();
            List<PropertyDescriptor> dpps = new List<PropertyDescriptor>();
            var ns = base.GetClassName().Replace(".", "");
            foreach(var prop in props) {
                var attrs = prop.Attributes.Cast<Attribute>().ToList();
                var dKey = string.Format("{0}_{1}_DisplayName", ns, prop.Name);
                var displayName = this.ResManager.GetString(dKey);
                if(!string.IsNullOrWhiteSpace(displayName)) {
                    attrs.Add(new DisplayNameAttribute(displayName));
                }
                var descKey = string.Format("{0}_{1}_Description", ns, prop.Name);
                var desc = this.ResManager.GetString(descKey);
                if(!string.IsNullOrWhiteSpace(descKey)) {
                    attrs.Add(new DescriptionAttribute(desc));
                }

                dpps.Add(TypeDescriptor.CreateProperty(prop.ComponentType, prop, attrs.ToArray()));
            }
            return new PropertyDescriptorCollection(dpps.ToArray());
        }
    }
}
