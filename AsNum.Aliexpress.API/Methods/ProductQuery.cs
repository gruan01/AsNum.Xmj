using AsNum.Xmj.API.Attributes;
using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Methods {
    public class ProductQuery : MethodBase<Paged<SuccinctProduct>> {
        protected override string APIName {
            get {
                return "api.findProductInfoListQuery";
            }
        }

        [EnumParam("productStatusType", EnumUseNameOrValue.Name, Required = true)]
        public ProductStatus Status {
            get;
            set;
        }

        [Param("subject")]
        public string Subject {
            get;
            set;
        }

        [Param("groupId")]
        public int? GroupID {
            get;
            set;
        }

        [EnumParam("wsDisplay", EnumUseNameOrValue.Name)]
        public ProductOfflineReasons? OfflineReason {
            get;
            set;
        }

        [Param("offLineTime")]
        public int? ExpireDays {
            get;
            set;
        }

        [Param("productId")]
        public long? ProductID {
            get;
            set;
        }

        [Param("pageSize")]
        public int? PageSize {
            get;
            set;
        }

        [Param("currentPage")]
        public int? CurrPage {
            get;
            set;
        }

        [Param("ownerMemberId")]
        public string OwnerID {
            get;
            set;
        }

        [NeedAuth]
        public override Paged<SuccinctProduct> Execute(Auth auth) {
            this.ResultString = this.GetResult(auth);
            var o = new {
                productCount = 0,
                currPage = 1,
                aeopAEProductDisplayDTOList = new List<SuccinctProduct>()
            };
            o = JsonConvert.DeserializeAnonymousType(this.ResultString, o);
            
            if (o.aeopAEProductDisplayDTOList != null)
                o.aeopAEProductDisplayDTOList.ForEach(a => {
                    a.Account = auth.User;
                });

            return new Paged<SuccinctProduct>() {
                CurrPage = o.currPage,
                Results = o.aeopAEProductDisplayDTOList ?? new List<SuccinctProduct>(),
                Total = o.productCount
            };
        }
    }
}
