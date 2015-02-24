using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Aliexpress.Entity {
    public class PProductCatelogMap {

        public int ID {
            get;
            set;
        }

        public int CatelogID {
            get;
            set;
        }

        [ForeignKey("CatelogID")]
        public virtual PProductCatelog Catelog {
            get;
            set;
        }

        [StringLength(20, MinimumLength = 5), Required]
        public string ProductID {
            get;
            set;
        }

        public virtual ICollection<PPAttrMap> AttrMaps {
            get;
            set;
        }

        public int Ver {
            get;
            set;
        }
    }
}
