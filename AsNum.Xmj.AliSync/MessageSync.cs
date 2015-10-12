using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.AliSync {
    public static class MessageSync {

        /// <summary>
        /// 同步订单留言
        /// </summary>
        /// <param name="orderNO"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public async static Task<List<MessageDetail>> SyncByOrderNO(string orderNO, string account) {
            var ast = new AccountSetting();
            var ac = ast.Value.FirstOrDefault(a => a.User == account);
            if (ac != null) {
                var api = new API.APIClient(ac.User, ac.Pwd);
                var method = new MessageDetailList() {
                    ChannelID = orderNO,
                    Type = MessageTypes.Order
                };
                var result = await api.Execute(method);
                return result.ToList();
            } else {
                return null;
            }
        }


        /// <summary>
        /// 发送订单留言
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="orderNO"></param>
        /// <param name="ctx"></param>
        public async static Task WriteOrderMessage(string acc, string buyerID, string orderNO, string ctx) {
            var ast = new AccountSetting();
            var ac = ast.Value.FirstOrDefault(a => a.User == acc);
            if (ac != null) {
                var api = new APIClient(ac.User, ac.Pwd);
                //var method = new OrderNewMsg() {
                //    OrderID = orderNO,
                //    Content = ctx
                //};
                var method = new MessageAdd() {
                    ChannelID = orderNO,
                    Ctx = ctx,
                    Type = MessageTypes.Order,
                    BuyerID = buyerID
                };
                var a = await api.Execute(method);
            }
        }

        /// <summary>
        /// 发送站内信
        /// </summary>
        /// <param name="acc"></param>
        /// <param name="buyerID"></param>
        /// <param name="ctx"></param>
        public async static Task SendMessage(string acc, string buyerID, string ctx) {
            var ast = new AccountSetting();
            var ac = ast.Value.FirstOrDefault(a => a.User == acc);
            if (ac != null) {
                var api = new APIClient(ac.User, ac.Pwd);
                //var method = new MessageSend() {
                //    BuyerID = buyerID,
                //    Ctx = ctx
                //};
                var method = new MessageAdd() {
                    BuyerID = buyerID,
                    Ctx = ctx,
                    Type = MessageTypes.MessageCenter
                };
                var a = await api.Execute(method);
            }
        }

        /// <summary>
        /// 订单留言列表
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="orderNO"></param>
        /// <returns></returns>
        [Obsolete]
        public async static Task<List<Message>> QueryOrderMsg(DateTime? begin, DateTime? end, string orderNO) {
            List<Message> msgs = new List<Message>();
            var s = new AccountSetting();
            foreach (var acc in s.Value) {
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new OrderMsgList() {
                    StartTime = begin,
                    EndTime = end,
                    OrderID = orderNO
                };
                var result = await api.Execute(method);
                if (result.Results != null)
                    msgs.AddRange(result.Results);
            }
            return msgs;
        }




        /// <summary>
        /// 站点内信列表
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="buyerID"></param>
        /// <returns></returns>
        [Obsolete]
        public async static Task<List<Message2>> QueryMsg(DateTime? begin, DateTime? end, string buyerID) {
            List<Message2> msgs = new List<Message2>();
            var s = new AccountSetting();
            foreach (var acc in s.Value) {
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new MessageQuery() {
                    StartTime = begin,
                    EndTime = end,
                    BuyerID = buyerID
                };
                var result = await api.Execute(method);
                msgs.AddRange(result.Results);
            }
            return msgs;
        }

        public static async Task<List<MessageRelation>> QueryRelations(MessageTypes msgType, int perCount = 10, bool unReaded = false) {
            List<MessageRelation> msgs = new List<MessageRelation>();
            var s = new AccountSetting();
            foreach (var acc in s.Value) {
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new MessageRelationList() {
                    PageSize = perCount,
                    Type = msgType,
                    UnReaded = unReaded
                };
                var result = await api.Execute(method);
                msgs.AddRange(result);
            }
            return msgs;
        }
    }
}
