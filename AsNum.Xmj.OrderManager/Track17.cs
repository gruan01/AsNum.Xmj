using AsNum.Common.Extends;
using AsNum.Xmj.Common.Interfaces;
using AsNum.Xmj.Entity;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;

namespace AsNum.Xmj.OrderManager {
    [Export(typeof(IQuickTrackButton)), PartCreationPolicy(CreationPolicy.NonShared)]
    [ExportMetadata("Support",

        LogisticsTypes.CHP | LogisticsTypes.CPAM | LogisticsTypes.CPAM_HRB |
        LogisticsTypes.HKPAM | LogisticsTypes.SEP | LogisticsTypes.SGP |
        LogisticsTypes.EMS_ZX_ZX_US |
        LogisticsTypes.DHL | LogisticsTypes.EMS |
        LogisticsTypes.ARAMEX | LogisticsTypes.FEDEX | LogisticsTypes.TNT)]
    public class Track17 : IQuickTrackButton {

        private void Deal(string url, IEnumerable<string> nos, Dictionary<string, List<string>> dic) {
            var lst = dic.Get(url, new List<string>());
            lst.AddRange(nos.Distinct());
            dic.Set(url, lst);
        }

        public void Track(List<OrdeLogistic> logistics) {
            //http://www.17track.net/cn/result/post.shtml?nums=RC698629338CN%2CRC698629386CN

            var groups = logistics.GroupBy(l => l.LogisticsType);

            var dic = new Dictionary<string, List<string>>();

            foreach (var g in groups) {
                switch (g.Key) {
                    case LogisticsTypes.CPAM:
                    case LogisticsTypes.CPAM_HRB:
                    case LogisticsTypes.EMS:
                    case LogisticsTypes.EMS_ZX_ZX_US:
                    case LogisticsTypes.SEP:
                    case LogisticsTypes.SGP:
                    case LogisticsTypes.CHP:
                    case LogisticsTypes.HKPAM:
                        this.Deal("http://www.17track.net/en/result/post.shtml", g.Select(l => l.TrackNO), dic);
                        break;
                    case LogisticsTypes.DHL:
                        this.Deal("http://www.17track.net/en/result/express-100001.shtml", g.Select(l => l.TrackNO), dic);
                        break;
                    case LogisticsTypes.ARAMEX:
                        this.Deal("http://www.17track.net/en/result/express-100006.shtml", g.Select(l => l.TrackNO), dic);
                        break;
                    case LogisticsTypes.TNT:
                        this.Deal("http://www.17track.net/en/result/express-100004.shtml", g.Select(l => l.TrackNO), dic);
                        break;
                    case LogisticsTypes.FEDEX:
                        this.Deal("http://www.17track.net/en/result/express-100003.shtml", g.Select(l => l.TrackNO), dic);
                        break;
                    case LogisticsTypes.UPS:
                    case LogisticsTypes.UPSE:
                        this.Deal("http://www.17track.net/en/result/express-100002.shtml", g.Select(l => l.TrackNO), dic);
                        break;
                }
            }

            foreach (var d in dic) {
                var url = d.Key.SetUrlKeyValue("nums", string.Join(",", d.Value));
                Process.Start(url);
            }
        }

        public string Title {
            get {
                return "17 Track";
            }
        }
    }
}
