using System.Xml.Linq;

namespace AsNum.Common.Extends {
    public static class XElementHelper {

        public static string GetNamedAttribute(this XElement el, string attrName, string defaultValue = null) {
            var attr = el.Attribute(attrName);
            if (attr != null)
                return attr.Value;
            else
                return defaultValue;
        }

    }
}
