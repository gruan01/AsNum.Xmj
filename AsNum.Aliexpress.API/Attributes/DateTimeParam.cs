using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Attributes {
    public class DateTimeParam : ParamAttribute {

        public string Format { get; set; }

        public DateTimeParam(string name, string fmt)
            : base(name) {

                this.Format = fmt;
        }

        public override Dictionary<string, string> GetParams(object obj, System.Reflection.PropertyInfo p) {
            //return base.GetParams(obj, p);

            var value = p.GetValue(obj, null);
            if(value == null && this.Required)
                return new Dictionary<string, string>(){
                    {this.Name, ""}
                };
            else if(value == null && !this.Required)
                return null;
            else
                return new Dictionary<string, string>(){
                    {this.Name, ((DateTime)p.GetValue(obj, null)).ToString(this.Format)}
                };

        }
    }
}
