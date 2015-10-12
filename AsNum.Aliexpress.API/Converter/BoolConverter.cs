using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Converter {
    public class BoolConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var v = reader.Value.ToString();
            if (v.Equals("0"))
                return false;
            else if (v.Equals("1"))
                return true;
            else if (v.Equals("true", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (v.Equals("false", StringComparison.OrdinalIgnoreCase))
                return false;
            else
                throw new NotSupportedException(string.Format("can't convert value : {0} to bool", v));

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
