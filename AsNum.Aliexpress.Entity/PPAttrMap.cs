using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Aliexpress.Entity {
    public class PPAttrMap {

        public int ID {
            get;
            set;
        }

        public int PCMID {
            get;
            set;
        }

        [ForeignKey("PCMID")]
        public virtual PProductCatelogMap PCM {
            get;
            set;
        }

        [StringLength(50), Required]
        public string Attrs {
            get;
            set;
        }

        [StringLength(20), Required]
        public string Name {
            get;
            set;
        }

        public int Version {
            get;
            set;
        }
    }
}
