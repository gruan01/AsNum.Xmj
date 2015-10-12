using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsNum.Xmj.AliSync {
    public static class ProductSync {

        public async static Task<List<SuccinctProduct>> Query(string subject, ProductStatus status = ProductStatus.OnSelling, string account = "", int? group = null, int? expiryDays = 3) {
            var products = new List<SuccinctProduct>();

            if (string.IsNullOrWhiteSpace(account)) {
                var accs = AccountHelper.LoadAccounts();
                foreach (var acc in accs) {
                    var datas = await QueryByAccount(acc, subject, status, null, expiryDays);
                    products.AddRange(datas);
                }
            } else {
                var acc = AccountHelper.LoadAccounts().FirstOrDefault(a => a.User.Equals(account, StringComparison.OrdinalIgnoreCase));
                if (acc != null)
                    products = await QueryByAccount(acc, subject, status, group, expiryDays);
            }
            return products;
        }

        private async static Task<List<SuccinctProduct>> QueryByAccount(Account acc, string subject, ProductStatus status = ProductStatus.OnSelling, int? group = null, int? expiryDays = 3) {
            var method = new ProductQuery() {
                Subject = subject,
                Status = status,
                GroupID = group,
                ExpireDays = expiryDays
            };
            var api = new APIClient(acc.User, acc.Pwd);
            var ps = await api.Execute(method);
            return ps.Results;
        }

        public async static Task<List<ProductGroup2>> QueryGroups(string account = "") {
            var groups = new List<ProductGroup2>();
            if (string.IsNullOrWhiteSpace(account)) {
                var accs = AccountHelper.LoadAccounts();
                foreach (var acc in accs) {
                    var datas = await QueryGroupsByAccount(acc);
                    groups.AddRange(datas);
                }
            } else {
                var acc = AccountHelper.LoadAccounts().FirstOrDefault(a => a.User.Equals(account, StringComparison.OrdinalIgnoreCase));
                if (acc != null)
                    groups = await QueryGroupsByAccount(acc);
            }

            return groups;
        }

        private async static Task<List<ProductGroup2>> QueryGroupsByAccount(Account account) {
            var api = new APIClient(account.User, account.Pwd);
            var method = new ProductGroupList();
            return await api.Execute(method);
        }

        public static void OfflineProducts(string account, List<string> productIDs) {
            var acc = AccountHelper.GetAccount(account);
            if (acc != null) {
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new ProductOffline() {
                    IDs = productIDs
                };
                api.Execute(method);
            }
        }

        public async static Task<bool> ExtendExpiryDate(string account, string productID) {
            var acc = AccountHelper.GetAccount(account);
            if (acc != null) {
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new ProductEditField() {
                    ProductID = productID,
                    Field = ProductBatchEditFields.ValidDay,
                    Value = "30"
                };

                var result = await api.Execute(method);
                return result.Success;
            }
            return false;
        }
    }
}
