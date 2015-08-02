using AsNum.Xmj.BizEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.Common.Interfaces {
    public interface IBatchShipment {

        void SendShipment(IEnumerable<ShipmentItem> items);

        void Show();
    }
}
