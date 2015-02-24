using AsNum.Xmj.API.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsNum.Xmj.API.Methods {
    public class ProductGroupList : MethodBase<List<ProductGroup2>> {
        protected override string APIName {
            get {
                return "api.getProductGroupList";
            }
        }

        public override List<ProductGroup2> Execute(Auth auth) {
            var str = this.GetResult(auth);
            var o = new {
                target = new List<ProductGroup2>()
            };
            o = JsonConvert.DeserializeAnonymousType(str, o);
            if (o.target != null) {
                o.target.ForEach(g => {
                    //g.Account = auth.User;
                    Deal(g, auth.User);
                });


            }

            return o.target ?? new List<ProductGroup2>();
        }

        private void Deal(ProductGroup2 group, string account, string parentNamePath = null) {
            group.Account = account;

            if (parentNamePath != null)
                group.NamePath = string.Format("{0} > {1}", parentNamePath, group.Name);// string.Join(" > ", new string[] { parentNamePath, group.Name });
            else
                group.NamePath = group.Name;

            if (group.Children != null)
                foreach (var c in group.Children) {
                    Deal(c, account, group.NamePath);
                }
        }
    }
}
