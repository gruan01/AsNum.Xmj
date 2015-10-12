using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsNum.Common.Extends;

namespace AsNum.Xmj.API.Converter {
    public class UnixTimeStampToDateTimeConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var ticket = reader.Value.ToString().ToLong();
            return this.FromUnixTimestamp(ticket);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public DateTime FromUnixTimestamp(double timestamp) {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddMilliseconds(timestamp);
        }
    }
}
