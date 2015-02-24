using System;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Attributes {
    /// <summary>
    /// 复杂参数扁平化
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false)] 

    internal class FlattenParamAttribute : ParamAttribute {

        public FlattenParamAttribute() : base("") { }

        public override Dictionary<string, string> GetParams(object obj, System.Reflection.PropertyInfo p) {
            var value = p.GetValue(obj, null);
            return ParamHelper.GetParams(value);
        }
    }
}
