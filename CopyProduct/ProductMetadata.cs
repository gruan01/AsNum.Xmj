using AsNum.Common.PropertyEditor;
using System.ComponentModel;

namespace CopyProduct {
    public class ProductMetadata {

        [Editor(typeof(PropertyGridHtmlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public object Detail { get; set; }

    }
}
