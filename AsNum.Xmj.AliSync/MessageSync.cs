using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Xmj.AliSync {
    public static class MessageSync {

        public static List<Message> SyncByOrderNO(string orderNO, string account) {
            var ast = new AccountSetting();
            var ac = ast.Value.FirstOrDefault(a => a.User == account);
            if (ac != null) {
                var api = new API.APIClient(ac.User, ac.Pwd);
                //var method = new OrderMsgListByOrderId() {
                //    OrderID = orderNO
                //};
                var method = new OrderMsgList() {
                    OrderID = orderNO
                };
                return api.Execute(method).Results;
            } else {
                return null;
            }
        }

        public static void WriteOrderMessage(string acc, string orderNO, string ctx) {
            var ast = new AccountSetting();
            var ac = ast.Value.FirstOrDefault(a => a.User == acc);
            if (ac != null) {
                var api = new APIClient(ac.User, ac.Pwd);
                var method = new OrderNewMsg() {
                    OrderID = orderNO,
                    Content = ctx
                };
                var a = api.Execute(method);
            }
        }

        public static void SendMessage(string acc, string buyerID, string ctx) {
            var ast = new AccountSetting();
            var ac = ast.Value.FirstOrDefault(a => a.User == acc);
            if (ac != null) {
                var api = new APIClient(ac.User, ac.Pwd);
                var method = new MessageSend() {
                    BuyerID = buyerID,
                    Ctx = ctx
                };
                var a = api.Execute(method);
            }
        }

        public static List<Message> QueryOrderMsg(DateTime? begin, DateTime? end, string orderNO) {
            List<Message> msgs = new List<Message>();
            var s = new AccountSetting();
            foreach (var acc in s.Value) {
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new OrderMsgList() {
                    StartTime = begin,
                    EndTime = end,
                    OrderID = orderNO
                };
                var result = api.Execute(method);
                msgs.AddRange(result.Results);
            }
            return msgs;
        }

        public static List<Message2> QueryMsg(DateTime? begin, DateTime? end, string buyerID) {
            List<Message2> msgs = new List<Message2>();
            var s = new AccountSetting();
            foreach (var acc in s.Value) {
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new MessageQuery() {
                    StartTime = begin,
                    EndTime = end,
                    BuyerID = buyerID
                };
                var result = api.Execute(method);
                msgs.AddRange(result.Results);
            }
            return msgs;
        }
    }
}
