using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.Entity {
    public class BuyerLevel {

        [ForeignKey("BuyerOf")]
        [StringLength(20), Required, Key]
        public string BuyerID { get; set; }


        [StringLength(10)]
        public string Level { get; set; }

        public DateTime UpdateOn { get; set; }

        public virtual Buyer BuyerOf { get; set; }
    }
}
