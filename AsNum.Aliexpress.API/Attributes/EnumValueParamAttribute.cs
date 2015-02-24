using System.Collections.Generic;

namespace AsNum.Xmj.API.Attributes {
    public class EnumValueParamAttribute : ParamAttribute {

        public EnumValueParamAttribute(string name)
            : base(name) {
        }

        public override Dictionary<string, string> GetParams(object obj, System.Reflection.PropertyInfo p) {

            var value = p.GetValue(obj, null);
            if(value == null && this.Required)
                return new Dictionary<string, string>(){
                    {this.Name, ""}
                };
            else if(value == null && !this.Required)
                return null;
            else
                return new Dictionary<string, string>(){
                    {this.Name, ((int)p.GetValue(obj, null)).ToString()}
                };
        }
    }
}
