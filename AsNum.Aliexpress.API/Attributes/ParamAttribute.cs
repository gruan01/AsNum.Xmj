using System;
using System.Collections.Generic;
using System.Reflection;

namespace AsNum.Xmj.API.Attributes {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ParamAttribute : Attribute {

        public string Name { get; set; }

        public bool Required { get; set; }

        public ParamAttribute(string name) {
            this.Name = name;
        }

        public virtual Dictionary<string, string> GetParams(object obj, PropertyInfo p) {
            var value = p.GetValue(obj, null);
            if(value == null && this.Required)
                return new Dictionary<string, string>(){
                    {this.Name, ""}
                };
            else if(value == null && !this.Required)
                return null;
            else
                return new Dictionary<string, string>(){
                    {this.Name, p.GetValue(obj, null).ToString()}
                };
        }
    }
}
