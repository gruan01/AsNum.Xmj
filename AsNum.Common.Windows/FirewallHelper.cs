using NetFwTypeLib;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AsNum.Common {
    public static class FirewallHelper {

        public enum ProtocolType {
            Tcp,
            Udp
        }

        public static void AddPortException(string name, int port, ProtocolType protocol) {
            //创建firewall管理类的实例
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));

            INetFwOpenPort objPort = (INetFwOpenPort)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwOpenPort"));

            objPort.Name = name;
            objPort.Port = port;
            switch(protocol) {
                case ProtocolType.Tcp:
                    objPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                    break;
                case ProtocolType.Udp:
                    objPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                    break;
            }
            objPort.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
            objPort.Enabled = true;

            bool exist = netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Cast<INetFwOpenPort>()
                .Any(p => p.Equals(objPort));

            if(!exist)
                netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(objPort);
        }

        public static void AddApplicationToException(string name, string path) {
            //创建firewall管理类的实例
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));

            INetFwAuthorizedApplication app = (INetFwAuthorizedApplication)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwAuthorizedApplication"));

            //在例外列表里，程序显示的名称
            app.Name = name;

            //程序的路径及文件名
            app.ProcessImageFileName = path;

            //是否启用该规则
            app.Enabled = true;

            ////加入到防火墙的管理策略
            //netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(app);

            bool exist = netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications
                .Cast<INetFwAuthorizedApplication>()
                .Any(a => a.ProcessImageFileName.Equals(app.ProcessImageFileName, StringComparison.OrdinalIgnoreCase));

            if(!exist)
                netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Add(app);
        }

        public static void AddApplicationToException() {
            var name = Application.ProductName;
            var path = Application.ExecutablePath;
            AddApplicationToException(name, path);
        }

        public static void RemovePortException(int port, ProtocolType protocol) {
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));
            switch(protocol) {
                case ProtocolType.Tcp:
                    netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Remove(port, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP);
                    break;
                case ProtocolType.Udp:
                    netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Remove(port, NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP);
                    break;
            }
        }

        public static void RemoveAppException(string path) {
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));
            netFwMgr.LocalPolicy.CurrentProfile.AuthorizedApplications.Remove(path);
        }
    }
}
