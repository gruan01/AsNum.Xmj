using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsNum.Xmj.Entity {
    public class FreightPartition {

        public int ID {
            get;
            set;
        }

        public LogisticsTypes LogisticTypes {
            get;
            set;
        }

        [StringLength(20)]
        public string Name {
            get;
            set;
        }

        public decimal Price {
            get;
            set;
        }

        public virtual ICollection<FreightPartitionCountry> Countries {
            get;
            set;
        }
    }
}
