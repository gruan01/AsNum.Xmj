using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Attributes {
    public class JsonParam : ParamAttribute {

        public JsonParam(string name) : base(name) { }

        public override Dictionary<string, string> GetParams(object obj, System.Reflection.PropertyInfo p) {

            var value = p.GetValue(obj, null);
            if(value == null && this.Required)
                return new Dictionary<string, string>(){
                    {this.Name, ""}
                };
            else if(value == null && !this.Required)
                return null;
            else {
                var settig = new JsonSerializerSettings();
                return new Dictionary<string, string>(){
                    {this.Name, JsonConvert.SerializeObject( p.GetValue(obj, null))}
                };
            }
        }

    }
}
