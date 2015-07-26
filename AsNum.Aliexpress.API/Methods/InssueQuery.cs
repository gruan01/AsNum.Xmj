using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsNum.Xmj.API.Methods {
    public class InssueQuery : MethodBase<object> {
        protected override string APIName {
            get {
                return "api.queryIssueList";
            }
        }

        [Param("orderNo")]
        public string OrderNO { get; set; }

        [Param("buyerName")]
        public string BuyerName { get; set; }

        [EnumNameParam("issueStatus", Required = true)]
        public IssueStatus? Status { get; set; }

        [Param("currentPage", Required = true)]
        public int Page { get; set; }

        [Param("pageSize")]
        public int PageSize { get; set; }
    }
}
