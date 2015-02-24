using AsNum.Common.Extends;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;

namespace AsNum.Xmj.OrderManager {
    [Export(typeof(IQuickTrackButton)), PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMetadata("Support", LogisticsTypes.EMS_ZX_ZX_US)]
    public class USPS : IQuickTrackButton {
        public void Track(List<OrdeLogistic> logistics) {
            var url = "https://tools.usps.com/go/TrackConfirmAction!input.action?tRef=qt&tLc=0&tLabels=";
            url = url.SetUrlKeyValue("tLabels", string.Join(",", logistics.Where(l => l.LogisticsType == LogisticsTypes.EMS_ZX_ZX_US).Select(l => l.TrackNO)));

            Process.Start(url);
        }

        public string Title {
            get {
                return "USPS";
            }
        }
    }
}
