using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using System;

namespace AsNum.Xmj.API.Methods {
    public class OrderQueryList : MethodBase<OrderList> {

        protected override string APIName {
            get { return "api.findOrderListQuery"; }
        }

        private int pageSize = 50;

        [Param("pageSize", Required = true)]
        public int PageSize {
            get {
                return pageSize;
            }
            set {
                this.pageSize = value < 1 ? 1 : (value > 50 ? 50 : value);
            }
        }

        private int page = 1;

        [Param("page", Required = true)]
        public int Page {
            get {
                return this.page;
            }
            set {
                this.page = value < 1 ? 1 : value;
            }
        }

        //[Param("createDateStart")]
        //[AliDateTimeParamFormatter("MM/dd/yyyy")]
        [DateTimeParam("createDateStart", "MM/dd/yyyy HH:mm:ss")]
        public DateTime? CreateBegin { get; set; }

        //[Param("createDateEnd")]
        //[AliDateTimeParamFormatter("MM/dd/yyyy")]
        [DateTimeParam("createDateEnd", "MM/dd/yyyy HH:mm:ss")]
        public DateTime? CreateEnd { get; set; }

        [Param("orderStatus")]
        public OrderStatus? Status { get; set; }
    }
}
