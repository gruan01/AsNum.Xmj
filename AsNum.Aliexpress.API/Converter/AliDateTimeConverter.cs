using Newtonsoft.Json.Converters;
using System.Globalization;

namespace AsNum.Xmj.API.Converter {
    public class AliDateTimeConverter : IsoDateTimeConverter {

        public AliDateTimeConverter()
            : base() {
            base.DateTimeFormat = "yyyyMMddHHmmssFFFzzzz";
            base.Culture = CultureInfo.InvariantCulture;
            base.DateTimeStyles = System.Globalization.DateTimeStyles.AdjustToUniversal;
        }

    }
}
