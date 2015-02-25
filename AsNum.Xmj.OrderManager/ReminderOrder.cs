using AsNum.Xmj.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.OrderManager {
    public class ReminderOrder : Order {

        public bool IsRemindered {
            get {
                return this.Messages != null && this.Messages.Any(m => !m.Sender.Equals(this.Buyer.Name));
            }
        }

        public string Msg { get; set; }

    }
}
