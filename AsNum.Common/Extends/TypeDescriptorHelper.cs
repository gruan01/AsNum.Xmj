using AsNum.Common.TypeDescriptors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;

namespace AsNum.Common.Extends {
    public class TypeDescriptorHelper {

        public static void SetDisplayAttributFromResource<T>(ResourceManager resManager) where T : class {
            TypeDescriptor.AddProvider(new DisplayPropertyTypeDescriptionProvider(typeof(T), resManager), typeof(T));
        }

        public static void AutoSetDisplayAttributeFromResource(string assemblyName, ResourceManager resManager) {
            var asms = Assembly.GetCallingAssembly()
                .GetReferencedAssemblies()
                .Where(a => a.Name.Equals(assemblyName));

            foreach (var a in asms) {
                try {
                    var asm = Assembly.Load(a);
                    var types = asm.GetTypes().Where(t => t.IsClass && t.IsPublic && !t.IsAbstract);
                    foreach (var t in types) {
                        TypeDescriptor.AddProvider(new DisplayPropertyTypeDescriptionProvider(t, resManager), t);
                    }
                } catch (ReflectionTypeLoadException) {
                }
            }
        }

        ///// <summary>
        ///// 将类的复杂属性映射类上，用于 DataGridView 显示子级属性
        ///// <remarks>如果同时使用了 SetDisplayAttributFromResource 方法，请确保该方法在 SetDisplayAttributFromResource 后调用</remarks>
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        //public static void MappingSubLevelProperty<T>() where T : class {
        //    TypeDescriptor.AddProvider(new SubLevelTypeDescriptionProvider(typeof(T)) , typeof(T));
        //}

        /// <summary>
        /// 将类的复杂属性映射类上，用于 DataGridView 显示子级属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="properties"></param>
        public static void MappingSubLevelProperty<T>(params Expression<Func<T, object>>[] properties) where T : class {
            var pps = properties.Select(p => GetPropertyName(p)).ToList();
            TypeDescriptor.AddProvider(new SubLevelTypeDescriptionProvider(typeof(T), pps), typeof(T));
        }

        private static string GetPropertyName(LambdaExpression property) {
            var member = property.Body as MemberExpression;

            if (member == null) {
                var unaryExpression = (UnaryExpression)property.Body;
                member = (MemberExpression)unaryExpression.Operand;
            }

            if (member.ToString().Split('.').Length < 3)
                throw new ArgumentException("只支持属性的属性,不支持少于两级的属性映射.", member.ToString());

            //return string.Format("{0}.{1}" , (member.Expression as MemberExpression).Member.Name , member.Member.Name);
            return string.Join(".", member.ToString().Split('.').Skip(1));
        }

        /// <summary>
        /// 给实体类注册伴随类
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TMetadata"></typeparam>
        public static void RegistMetadata<TEntity, TMetadata>()
            where TEntity : class
            where TMetadata : class {

            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(TEntity), typeof(TMetadata)), typeof(TEntity));
        }

        /// <summary>
        /// 给非基元属性(不包括 string 类型)加上 Browserable = false
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public static void SetNotBrowserableForComplexProperty<TEntity>() {
            var pts = TypeDescriptor.GetProperties(typeof(TEntity));//.Cast<PropertyDescriptor>();
            var builder = new RuntimeAttributeBuilder<TEntity>();
            foreach (var pt in pts) {
                var opt = (PropertyDescriptor)pt;
                if (!(opt.PropertyType.IsPrimitive || opt.PropertyType == typeof(string)) && opt.IsBrowsable) {
                    builder.Add(opt.Name, new BrowsableAttribute(false));
                }
            }
            TypeDescriptorHelper.AddRuntimeAttribute<TEntity>(builder);
        }

        public static void AddRuntimeAttribute<T>(RuntimeAttributeBuilder<T> builder) {
            TypeDescriptor.AddProvider(new RuntimeAttributeTypeDescriptorProvider(typeof(T), builder.PropAttrsMap), typeof(T));
        }

        public static void AddAttionalProperty<T, TProperty>(string propName, Func<T, TProperty> func) where T : class {
            var dic = new Dictionary<string, Func<T, TProperty>>();
            dic.Add(propName, func);
            TypeDescriptor.AddProvider(new AdditionPropertyTypeDescriptorProvider<T, TProperty>(dic), typeof(T));
        }
    }

    public class RuntimeAttributeBuilder<T> {

        internal Dictionary<string, List<Attribute>> PropAttrsMap = new Dictionary<string, List<Attribute>>();

        public void Add(Expression<Func<T, object>> prop, Attribute attr) {
            var key = ((MemberExpression)prop.Body).Member.Name;
            this.Add(key, attr);
        }

        public void Add(string prop, Attribute attr) {
            if (this.PropAttrsMap.ContainsKey(prop))
                this.PropAttrsMap[prop].Add(attr);
            else {
                this.PropAttrsMap[prop] = new List<Attribute>() { 
                    attr
                };
            }
        }
    }
}
