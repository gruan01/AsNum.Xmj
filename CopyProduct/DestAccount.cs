using AsNum.Xmj.API.Entity;
using System.Collections.Generic;
using System.ComponentModel;

namespace CopyProduct {
    public class DestAccount {

        [DisplayName("账户")]
        public string User { get; set; }
        [Browsable(false)]
        public string Pwd { get; set; }

        public FreightTemplate FrightTemplate { get; set; }

        public ProductGroup2 ProductGroup { get; set; }

        [Browsable(false)]
        public List<FreightTemplate> FreightTemplates { get; set; }

        [Browsable(false)]
        public List<ProductGroup2> ProductGroups { get; set; }

        [DisplayName("是否选中")]
        public bool IsChecked { get; set; }
    }
}
