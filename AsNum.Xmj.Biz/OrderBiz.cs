using AsNum.Common.Extends;
using AsNum.Xmj.BizEntity.Conditions;
using AsNum.Xmj.Data;
using AsNum.Xmj.Entity;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AsNum.Xmj.Biz {

    [Export(typeof(IOrder))]
    public class OrderBiz : BaseBiz, IOrder {

        private IQueryable<Order> Search(Entities db, OrderSearchCondition cond) {
            var datas = db.Orders.Include(p => p.AccountOf)
                .Include(p => p.AdjReceiver.Country)
                .Include(p => p.Buyer.Level)
                .Include(p => p.Details.Select(d => d.Attrs))
                .Include(p => p.Logistics)
                .Include(p => p.Messages)
                .Include(p => p.Note)
                .Include(p => p.OrgReceiver.Country)
                .Include(p => p.PurchasseDetail);

            datas = cond.Filter(datas);

            var sql = datas.ToString();

            if (cond.IncludeStatus != null) {
                datas = datas.Where(o => cond.IncludeStatus.Contains(o.Status));
            }

            if (cond.SpecifyOrders != null) {
                datas = datas.Where(o => cond.SpecifyOrders.Contains(o.OrderNO));
            }

            if (cond.ExcludeOrderNOs != null) {
                datas = datas.Where(o => !cond.ExcludeOrderNOs.Contains(o.OrderNO));
            }

            if (cond.SpecifyAccounts != null) {
                datas = datas.Where(o => cond.SpecifyAccounts.Contains(o.Account));
            }

            if (!string.IsNullOrWhiteSpace(cond.ReceiverName)) {
                datas = datas.Where(o => o.OrgReceiver.Name == cond.ReceiverName.Trim());
            }

            if (!string.IsNullOrWhiteSpace(cond.TrackNO)) {
                datas = datas.Where(o => o.Logistics.Any(l => l.TrackNO == cond.TrackNO.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(cond.ProductID)) {
                datas = datas.Where(o => o.Details.Any(p => p.ProductID == cond.ProductID.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(cond.CustomerName)) {
                datas = datas.Where(o => o.Buyer.Name == cond.CustomerName.Trim());
            }

            if (!string.IsNullOrWhiteSpace(cond.Note)) {
                datas = datas.Where(o => o.Note.Note.IndexOf(cond.Note) > -1);
            }

            if (!string.IsNullOrWhiteSpace(cond.LogisticsType)) {
                var t = cond.LogisticsType.ToString();
                //没有发货记录,取子订单里的客人选择的配送方式
                datas = datas.Where(o => (o.Logistics.Count == 0 && o.Details.Any(d => d.LogisticsType == t))
                //有发货记录,取实际的配送方式
                || (o.Logistics.Any(l => l.LogisticCode.Equals(cond.LogisticsType, StringComparison.OrdinalIgnoreCase)))
                );
            }

            if (!string.IsNullOrWhiteSpace(cond.ReceiverCountryCode)) {
                datas = datas.Where(o => o.AdjReceiver != null ? o.AdjReceiver.CountryCode == cond.ReceiverCountryCode.Trim() : o.OrgReceiver.CountryCode == cond.ReceiverCountryCode.Trim());
            }

            if (cond.TimesType.HasValue && (cond.BeginAt.HasValue || cond.EndAt.HasValue)) {

                var beginAt = cond.BeginAt ?? DateTime.Now.AddYears(-10);
                var endAt = cond.EndAt ?? DateTime.Now;

                switch (cond.TimesType.Value) {
                    case OrderSearchCondition.TimesTypes.ByCreateOn:
                        datas = datas.Where(o => o.CreteOn >= beginAt && o.CreteOn <= endAt);
                        break;
                    case OrderSearchCondition.TimesTypes.ByPayment:
                        datas = datas.Where(o => o.PaymentOn >= beginAt && o.PaymentOn <= endAt);
                        break;
                    case OrderSearchCondition.TimesTypes.BySendout:
                        var ons = db.OrderLogistics.Where(l => l.SendOn >= beginAt && l.SendOn <= endAt).Select(l => l.OrderNO);
                        datas = datas.Where(o => ons.Contains(o.OrderNO));
                        break;
                }
            }

            return datas;
        }

        public IEnumerable<Order> Search(OrderSearchCondition cond) {

            using (var db = new Entities()) {
                var datas = this.Search(db, cond);

                var t = datas
                    .OrderByDescending(o => o.CreteOn)
                    .DoPage(cond.Pager);

                //var sql = t.ToString();

                return t.ToList();

            }
        }

        public Order GetOrder(string orderNo, bool include = true) {
            using (var db = new Entities()) {
                var datas = db.Orders.AsQueryable();
                if (include)
                    datas = db.Orders.Include(p => p.AccountOf)
                        .Include(p => p.AdjReceiver.Country)
                        .Include(p => p.Buyer.Level)
                        .Include(p => p.Details)
                        .Include(p => p.Logistics)
                        .Include(p => p.Messages)
                        .Include(p => p.Note)
                        .Include(p => p.OrgReceiver.Country)
                        .Include(p => p.PurchasseDetail);

                return datas.FirstOrDefault(o => o.OrderNO == orderNo);
            }
        }

        public Order AddOrEdit(Order order) {
            using (var db = new Entities()) {

                var exb = db.Buyers.FirstOrDefault(b => b.BuyerID == order.BuyerID);
                if (exb != null)
                    order.Buyer.CopyToExcept(exb);
                else
                    db.Buyers.Add(order.Buyer);

                var exa = db.Owners.FirstOrDefault(a => a.Account == order.Account);
                if (exa == null)
                    db.Owners.Add(order.AccountOf);

                var isSamShipping = db.Orders.Where(o => o.OrderNO == order.OrderNO).Select(o => o.IsShamShipping).FirstOrDefault();

                order.AccountOf = null;
                order.Buyer = null;
                order.IsShamShipping = isSamShipping;
                db.Orders.AddOrUpdate(order);
                db.OrderLogistics.AddOrUpdate(order.Logistics.ToArray());

                this.Errors = db.GetErrors();
                if (!this.HasError) {
                    db.SaveChanges();
                }

                return order;
            }
        }


        public int Count(OrderSearchCondition cond) {
            using (var db = new Entities()) {
                var datas = this.Search(db, cond);
                var sql = datas.ToString();
                return datas.Count();
            }
        }


        public void UpdateShamShippingStatus(string orderNO, bool isSham) {
            using (var db = new Entities()) {
                db.Execute("UPDATE ORDERS SET IsShamShipping = @p0 WHERE ORDERNO = @p1", new { p0 = isSham, p1 = orderNO });
            }
        }


        public void SaveOrderMessage(IEnumerable<OrderMessage> messages) {
            using (var db = new Entities()) {
                var ids = messages.Select(m => m.ID);
                var willAdd = ids.Except(db.OrderMessages.Where(m => ids.Contains(m.ID)).Select(m => m.ID));
                foreach (var m in messages.Where(m => willAdd.Contains(m.ID))) {
                    db.OrderMessages.Add(m);
                }

                this.Errors = db.GetErrors();
                if (!this.HasError)
                    db.SaveChanges();
            }
        }


        public void SaveOrderNote(OrderNote note) {
            using (var db = new Entities()) {
                var ex = db.OrderNotes.FirstOrDefault(n => n.OrderNO == note.OrderNO);
                if (ex != null)
                    note.CopyToExcept(ex, n => n.OrderOf);
                else
                    db.OrderNotes.Add(note);

                this.Errors = db.GetErrors();
                if (!this.HasError)
                    db.SaveChanges();
            }
        }


        public void SavePurchaseDetail(PurchaseDetail detail) {
            using (var db = new Entities()) {
                var ex = db.PurchaseDetails.FirstOrDefault(p => p.OrderNO == detail.OrderNO);
                if (ex != null)
                    detail.CopyToExcept(ex, d => d.OrderOf);
                else
                    db.PurchaseDetails.Add(detail);

                this.Errors = db.GetErrors();
                if (!this.HasError)
                    db.SaveChanges();
            }
        }
    }
}
