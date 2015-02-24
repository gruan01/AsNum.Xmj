using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace AsNum.Common {
    public static class PortHelper {

        private static Lazy<List<int>> UsedPorts {
            get {
                return new Lazy<List<int>>(() => {
                    var ipps = IPGlobalProperties.GetIPGlobalProperties();
                    var ports = ipps.GetActiveTcpConnections().Select(tc => tc.LocalEndPoint.Port).ToList();
                    ports.AddRange(ipps.GetActiveTcpListeners().Select(tl => tl.Port));
                    ports.AddRange(ipps.GetActiveUdpListeners().Select(ul => ul.Port));
                    return ports;
                });
            }
        }

        /// <summary>
        /// 返回一个可用的端口，如果指定的端口不可用，返回下一个可用的端口。范围从 8081 到 65535
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static int GetAvailablePort(int port) {
            if(UsedPorts.Value.Contains(port)) {
                return Enumerable.Range(8081, 65535 - 8081).Except(UsedPorts.Value).First();
            } else {
                return port;
            }
        }

    }
}
