using Newtonsoft.Json;
using System;

namespace AsNum.Xmj.API.Converter {
    public class IssueConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            //JObject jObj = JObject.Load(reader);
            return reader.Value.Equals("IN_ISSUE");
            //return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
