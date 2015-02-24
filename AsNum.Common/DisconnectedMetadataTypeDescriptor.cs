using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AsNum.Common {
    public class DisconnectedMetadataTypeDescriptor : CustomTypeDescriptor {
        private static Type AssociatedMetadataTypeTypeDescriptor =
          typeof(AssociatedMetadataTypeTypeDescriptionProvider)
          .Assembly
          .GetType("System.ComponentModel.DataAnnotations.AssociatedMetadataTypeTypeDescriptor" , true);

        public Type Type { get; private set; }
        public Type AssociatedMetadataType { get; private set; }
        private object _associatedMetadataTypeTypeDescriptor;

        public DisconnectedMetadataTypeDescriptor(ICustomTypeDescriptor parent , Type type)
            : this(parent , type , GetAssociatedMetadataType(type)) {
        }

        public DisconnectedMetadataTypeDescriptor(ICustomTypeDescriptor parent , Type type , Type associatedMetadataType)
            : base(parent) {
            this._associatedMetadataTypeTypeDescriptor = Activator.CreateInstance(AssociatedMetadataTypeTypeDescriptor , parent , type , associatedMetadataType);
            this.Type = type;
            this.AssociatedMetadataType = associatedMetadataType;
        }

        public override AttributeCollection GetAttributes() {
            return AssociatedMetadataTypeTypeDescriptor.InvokeMember(
              "GetAttributes" ,
              BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod ,
              null ,
              this._associatedMetadataTypeTypeDescriptor ,
              new object[] { }) as AttributeCollection;
        }

        public override PropertyDescriptorCollection GetProperties() {
            return AssociatedMetadataTypeTypeDescriptor.InvokeMember(
              "GetProperties" ,
              BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod ,
              null ,
              this._associatedMetadataTypeTypeDescriptor ,
              new object[] { }) as PropertyDescriptorCollection;
        }

        public static Type GetAssociatedMetadataType(Type type) {
            if(type == null) {
                throw new ArgumentNullException("type");
            }

            // Try the map first...
            if(DisconnectedMetadata.Map.ContainsKey(type)) {
                return DisconnectedMetadata.Map[type];
            }

            // ...and fall back to the standard mechanism.
            MetadataTypeAttribute[] customAttributes = (MetadataTypeAttribute[])type.GetCustomAttributes(typeof(MetadataTypeAttribute) , true);
            if(customAttributes != null && customAttributes.Length > 0) {
                return customAttributes[0].MetadataClassType;
            }
            return null;
        }
    }

    public static class DisconnectedMetadata {
        public static Dictionary<Type , Type> Map { get; private set; }

        static DisconnectedMetadata() {
            Map = new Dictionary<Type , Type>();
        }
    }
}
