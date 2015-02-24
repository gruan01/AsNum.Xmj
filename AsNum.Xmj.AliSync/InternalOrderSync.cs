using AsNum.Common.Extends;
using AsNum.Xmj.AliSync.Settings;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using AsNum.Xmj.Common;
using AsNum.Xmj.IBiz;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AE = AsNum.Xmj.Entity;

namespace AsNum.Xmj.AliSync {

    internal class OrderDealedEventArgs : EventArgs {

        public string Account {
            get;
            set;
        }

        public OrderStatus Status {
            get;
            set;
        }

        public string OrderNO {
            get;
            set;
        }

        public bool IsSuccess {
            get;
            set;
        }

    }

    internal class OrderListEventArgs : EventArgs {

        public string Account {
            get;
            set;
        }

        public List<SuccinctOrder> Orders {
            get;
            set;
        }
        public int Page {
            get;
            set;
        }

        public int Total {
            get;
            set;
        }

        public OrderStatus Status {
            get;
            set;
        }

        public bool Smart {
            get;
            set;
        }
    }

    internal class InternalOrderSync {

        private API.APIClient Api;

        public event EventHandler<OrderListEventArgs> OrderListReturned;
        public event EventHandler<OrderDealedEventArgs> OrderDealed;

        [Import]
        private Lazy<LogObserverable> LogObserverable = new Lazy<LogObserverable>(LazyThreadSafetyMode.ExecutionAndPublication);

        //[Import]
        //public IOrder OrderBiz { get; set; }

        public InternalOrderSync(string user, string pwd) {
            //
            GlobalData.MefContainer.ComposeParts(this);

            this.Api = new API.APIClient(user, pwd);
            this.OrderListReturned += OrderSync_OrderListReturned;
        }

        private void OrderSync_OrderListReturned(object sender, OrderListEventArgs e) {
            e.Orders.ForEach(o => {
                Task.Factory.StartNew(oo => {
                    var order = (SuccinctOrder)oo;
                    order.Account = e.Account;
                    this.DealOrder(order, e.Smart);

                    if (this.OrderDealed != null)
                        this.OrderDealed.Invoke(this, new OrderDealedEventArgs() {
                            Account = order.Account,
                            OrderNO = order.OrderID,
                            Status = order.OrderStatus,
                            IsSuccess = true
                        });

                }, o, TaskCreationOptions.AttachedToParent)
                .ContinueWith(t => {
                    var order = (SuccinctOrder)t.AsyncState;
                    //TASK Exception 未抛出
                    this.LogObserverable.Value.Notify(string.Format("处理订单 ： {0} ({1}) 时发生错误！", order.OrderID, order.Account), false);
                    this.LogObserverable.Value.Notify(t.Exception);

                    if (this.OrderDealed != null)
                        this.OrderDealed.Invoke(this, new OrderDealedEventArgs() {
                            Account = order.Account,
                            OrderNO = order.OrderID,
                            Status = order.OrderStatus,
                            IsSuccess = false
                        });

                }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.AttachedToParent);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="smart"></param>
        private void DealOrder(SuccinctOrder order, bool smart) {
            var biz = GlobalData.MefContainer.GetExportedValue<IOrder>();
            var eo = biz.GetOrder(order.OrderID, false);
            if (!smart || (eo == null || (
                (byte)eo.Status != (byte)order.OrderStatus)
                || eo.InIssue != order.InIssue
                )) {
                this.LogObserverable.Value.Notify(string.Format("更新订单 {0}", order.OrderID), true);
                var od = this.GetOrderDetail(order.OrderID);
                //转换为数据库实体
                var o = this.CombineOrderData(od, order);
                biz.AddOrEdit(o);
                if (biz.HasError) {
                    var str = string.Join(";", biz.Errors.Select(kv => string.Join("{0} : {1}", kv.Key, kv.Value)));
                    this.LogObserverable.Value.Notify(string.Format("订单 {0} : {1}", o.OrderNO, str), false);
                }
            } else {
                //this.LogObserverable.Value.Notify(string.Format("订单 {0} 未发生变化，跳过", order.OrderID), true);
            }
        }

        /// <summary>
        /// 合并两个接口返回的订单数据
        /// </summary>
        /// <param name="dOrder"></param>
        /// <param name="sOrder"></param>
        /// <returns></returns>
        private AE.Order CombineOrderData(DetailOrder dOrder, SuccinctOrder sOrder) {
            var ao = new AE.Order() {
                InIssue = sOrder.InIssue,
                //dOrder 的 Amount 不是调整后的价格.
                Amount = sOrder.Amount.Total,// dOrder.OrderAmount.Total;
                CreteOn = dOrder.CreateOn,
                Currency = dOrder.OrderAmount.Currency.CurrencyCode,

                OrderNO = dOrder.OrderNO,
                PaymentOn = dOrder.PaymentOn,
                PaymentType = sOrder.PaymentType,
                Status = ((byte)dOrder.Status).ToEnum<AE.OrderStatus>(),
                OffTime = DateTime.Now.Add(sOrder.OutLeftTime)
            };

            #region details
            ao.Details = dOrder.ChildOrders.Select(d => {
                var sDetail = sOrder.Products.Find(so => so.ChildID.Equals(d.ID));
                return new AE.OrderDetail() {
                    ProductPrice = sDetail.UnitPrice.Total,
                    DeliveryTime = sDetail.DeliveryTime,
                    Image = sDetail.Image,//dOrder 里没有这个数据
                    LotNum = d.LotNum,
                    OrderNO = sOrder.OrderID,
                    PrepareDays = sDetail.PrepareDays,
                    ProductID = d.ProductID,
                    ProductName = d.ProductName,
                    Remark = sDetail.OrderMessage,
                    SKUCode = d.SKUCode,
                    SnapURL = sDetail.SnapURL,
                    SubOrderNO = d.ID,
                    Unit = d.Unit,
                    UnitQty = d.Count,
                    LogisticsType = sDetail.LogisticsType,
                    //老订单返回的没有 LogisticsAmount
                    LogisticAmount = sDetail.LogisticsAmount == null ? 0 : sDetail.LogisticsAmount.Total,
                    Attrs = d.Attributes.SKU.Select(s => new AE.OrderDetailAttribute() {
                        Order = s.Order,
                        OrderNO = dOrder.OrderNO,
                        SubOrderNO = d.ID,
                        AttrStr = string.IsNullOrWhiteSpace(s.CustomValue) ? string.Format("{0}: {1}", s.Name, s.Value) : s.CustomValue,
                        CompatibleStr = s.CompatibleStr
                    }).ToList()
                };
            }
            ).ToList();
            #endregion;

            #region OrgReceiver
            ao.OrgReceiver = new AE.Receiver() {
                OrderNO = dOrder.OrderNO,
                Address = string.IsNullOrWhiteSpace(dOrder.Receiver.Address2) ? dOrder.Receiver.Address : string.Format("{0} , {1}", dOrder.Receiver.Address, dOrder.Receiver.Address2),
                City = dOrder.Receiver.City,
                CountryCode = dOrder.Receiver.CountryCode,
                Mobi = dOrder.Receiver.Mobi,
                Name = dOrder.Receiver.ContactPerson,
                Phone = string.Join("-", new string[] { dOrder.Receiver.PhoneCountry, dOrder.Receiver.PhoneArea, dOrder.Receiver.Phone }.Where(s => !string.IsNullOrWhiteSpace(s))),
                PhoneArea = dOrder.Receiver.PhoneArea,
                Province = dOrder.Receiver.Province,
                ZipCode = dOrder.Receiver.ZipCode,
            };
            #endregion

            #region Logistics
            ao.Logistics = dOrder.LogisticInfos.Where(l => l.SendOn.HasValue).Select(l => new AE.OrdeLogistic() {
                SendOn = l.SendOn,
                TrackNO = l.TrackingNo,
                LogisticsType = l.TypeCode.ToEnum<AE.LogisticsTypes>(),
                OrderNO = sOrder.OrderID
            }).ToList();
            #endregion

            #region Account
            ao.Account = sOrder.Account;
            ao.AccountOf = new AE.Owner() {
                Account = sOrder.Account,
                AccountType = AE.AccountTypes.Ali
            };
            #endregion

            #region buyer
            //某些订单，在订单列表里，是没有BuyerID 的，导至保存失败
            ao.BuyerID = dOrder.Customer.LoginID;
            ao.Buyer = new AE.Buyer() {
                BuyerID = dOrder.Customer.LoginID,
                CountryCode = dOrder.Customer.CountryCode,
                Email = dOrder.Customer.Email,
                Name = string.Format("{0} {1}", dOrder.Customer.FirstName, dOrder.Customer.LastName)
            };
            #endregion

            return ao;
        }

        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        private DetailOrder GetOrderDetail(string orderID) {
            var method = new OrderFindByID();
            method.OrderID = orderID;
            return this.Api.Execute(method);
        }

        /// <summary>
        /// 由于API白限制,该方法只更新已存在的订单:
        /// </summary>
        /// <param name="orderID"></param>
        public void Sync(AE.Order order) {
            DateTime time = DateTime.Now;
            if (order == null)
                return;
            //太平洋时间,-8区,不直接减8是因为有夏令时
            //订单数据里的时间以经在同步的时候转换为了UTC时间
            time = TimeZoneInfo.ConvertTimeFromUtc(order.CreteOn, TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time"));

            Task.Factory.StartNew(() => {
                this.SyncByStatus(OrderStatus.UNKNOW, false, time, time);
            }, TaskCreationOptions.AttachedToParent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="smart"></param>
        public void Sync(OrderStatus status = OrderStatus.UNKNOW, bool smart = true) {

            var days = new SmartSyncDays().Value;
            DateTime? start = null;
            DateTime? end = null;
            if (days > 0) {
                start = DateTime.Now.AddDays(-days);
                end = DateTime.Now.AddDays(1);
            }

            if (status == OrderStatus.UNKNOW) {
                //按状态更新,否则不知道各状态下面有多少条数据
                Enum.GetNames(typeof(OrderStatus))
                    .Select(s => s.ToEnum<OrderStatus>())
                    .Where(s => s != OrderStatus.UNKNOW)
                    .ToList()
                    .ForEach(s => {
                        Task.Factory.StartNew(() => {
                            this.SyncByStatus(s, smart, start, end);
                        }, TaskCreationOptions.AttachedToParent);
                    });
            } else {
                Task.Factory.StartNew(() => {
                    this.SyncByStatus(status, smart, start, end);
                }, TaskCreationOptions.AttachedToParent)
                .ContinueWith((t, o) => {
                    this.LogObserverable.Value.Notify(string.Format("状态为 ： {0} 的订单同步完成！", EnumHelper.GetDescription(status)), true);
                }, TaskContinuationOptions.AttachedToParent);
            }
        }

        private void SyncByStatus(OrderStatus status, bool smart, DateTime? start, DateTime? end, int page = 1, int? total = null) {

            var method = new OrderQueryList();
            method.PageSize = 50;
            method.Page = page;
            //UNKNOW不是具体的状态状态,只是一个辅助
            if (status != OrderStatus.UNKNOW)
                method.Status = status;

            if (start.HasValue)
                method.CreateBegin = start;
            if (end.HasValue)
                method.CreateEnd = end;

            var orderList = this.Api.Execute(method);

            if (this.OrderListReturned != null)
                this.OrderListReturned.Invoke(this, new OrderListEventArgs() {
                    Account = this.Api.AuthUser,
                    Orders = orderList.Orders,
                    Page = page,
                    Total = orderList.Count,
                    Status = status,
                    Smart = smart
                });

            total = total.HasValue ? total : orderList.Count;

            if (total > page * method.PageSize && page == 1) {
                var totalPage = (int)Math.Ceiling((double)total / method.PageSize);

                for (var i = 2; i <= totalPage; i++)
                    Task.Factory.StartNew((p) => {
                        this.SyncByStatus(status, smart, start, end, (int)p, orderList.Count);
                    }, i, TaskCreationOptions.AttachedToParent);
            }
        }
    }
}
