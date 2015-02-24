using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    public class FreightPartitionCountry {

        [Key, Column(Order = 0)]
        public int PartitionID {
            get;
            set;
        }

        [StringLength(5), Required]
        [Key, Column(Order = 1)]
        public string CountryCode {
            get;
            set;
        }

        [ForeignKey("CountryCode")]
        public virtual Country Country {
            get;
            set;
        }

        [ForeignKey("PartitionID")]
        public virtual FreightPartition Partition {
            get;
            set;
        }

    }
}
