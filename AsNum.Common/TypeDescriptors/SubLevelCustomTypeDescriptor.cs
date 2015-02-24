using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AsNum.Common.TypeDescriptors {


    /// <summary>
    /// 将复杂类型的属性的属性映射为类型的一个属性
    /// <remarks>
    /// 用于解决 DataGridView 在绑定复杂属性的时候，不能显示的问题
    /// </remarks>
    /// </summary>
    public class SubLevelCustomTypeDescriptor : CustomTypeDescriptor {

        /// <summary>
        /// 需要映射的子属性路径
        /// </summary>
        private List<string> SubProperties = null;

        public SubLevelCustomTypeDescriptor(ICustomTypeDescriptor parent, List<string> subProperties = null)
            : base(parent) {

            this.SubProperties = subProperties;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
            PropertyDescriptorCollection cols = base.GetProperties(attributes);

            var props = cols.Cast<PropertyDescriptor>();

            var newProps = new List<PropertyDescriptor>(props);

            //如果不指定 SubProperties ， 映射所有非基元子属性
            if(this.SubProperties == null || this.SubProperties.Count == 0) {
                foreach(var prop in props) {
                    var pt = prop.PropertyType;
                    //string 也是非基元类型
                    if(!pt.IsPrimitive) {
                        var subProps = prop.GetChildProperties().Cast<PropertyDescriptor>();
                        foreach(var subPt in subProps) {
                            var subDescriptor = new SubLevelPropertyDescriptor(prop, subPt, string.Format("{0}_{1}", prop.Name, subPt.Name));
                            newProps.Add(subDescriptor);
                        }
                    }
                }
            } else {
                this.SubProperties.ForEach(sbp => {
                    //var tmp = sbp.Split('.');
                    //// 只支持属性的属性
                    //if(tmp.Length < 2)
                    //    return;

                    //var prop = base.GetProperties(attributes).Find(tmp[0], false);
                    //if(prop == null)
                    //    return;
                    //else {
                    //    var subPt = prop.GetChildProperties().Find(tmp[1], false);
                    //    if(subPt == null)
                    //        return;
                    //    else {
                    //        var subDescriptor = new SubLevelPropertyDescriptor(prop, subPt, string.Format("{0}_{1}", prop.Name, subPt.Name));
                    //        newProps.Add(subDescriptor);
                    //    }
                    //}

                    var subDescriptor = this.FindSubProperty(sbp, base.GetProperties(attributes));
                    newProps.Add(subDescriptor);
                });
            }

            return new PropertyDescriptorCollection(newProps.ToArray(), true);
        }

        private PropertyDescriptor FindSubProperty(string subPath, PropertyDescriptorCollection pcs) {
            var tmp = subPath.Split('.');
            //支持孙子属性，孙子。。。属性
            if(tmp.Length < 2)
                return null;

            var prop = pcs.Find(tmp[0], false);
            if(prop == null)
                return null;
            else {
                var i = 1;
                while(i <= tmp.Length - 1) {
                    prop = FindSubProperty(tmp[i], prop);
                    i++;
                }

                return prop;
            }
        }

        private SubLevelPropertyDescriptor FindSubProperty(string sub, PropertyDescriptor prop) {
            var subPt = prop.GetChildProperties().Find(sub, false);
            if(subPt == null)
                return null;
            else {
                return new SubLevelPropertyDescriptor(prop, subPt, string.Format("{0}_{1}", prop.Name, subPt.Name));
            }
        }

        //public override PropertyDescriptorCollection GetProperties() {
        //    PropertyDescriptorCollection cols = base.GetProperties();

        //    PropertyDescriptor addressPD = cols["Customer"];
        //    PropertyDescriptorCollection homeAddr_child = addressPD.GetChildProperties();

        //    PropertyDescriptor[] array = new PropertyDescriptor[cols.Count + 2];
        //    cols.CopyTo(array , 0);
        //    array[cols.Count] = new SubPropertyDescriptor(addressPD , homeAddr_child["Name"] , "Customer_Name");
        //    array[cols.Count + 1] = new SubPropertyDescriptor(addressPD , homeAddr_child["Email"] , "HomeAddr_Email");

        //    PropertyDescriptorCollection newcols = new PropertyDescriptorCollection(array);
        //    return newcols;
        //}

    }
}
