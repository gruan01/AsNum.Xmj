using System.ComponentModel.DataAnnotations;

namespace AsNum.Xmj.Entity {
    public class LogisticFee {

        [Key, StringLength(20)]
        public string TrackNO { get; set; }

        public decimal Weight { get; set; }

        public decimal Fee { get; set; }
    }
}
