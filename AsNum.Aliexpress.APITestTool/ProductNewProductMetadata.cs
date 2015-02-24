using System.ComponentModel;

namespace AsNum.Xmj.APITestTool {
    public class ProductNewProductMetadata {

        [DisplayName("产品详情")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public object ProductDetail { get; set; }

    }
}
