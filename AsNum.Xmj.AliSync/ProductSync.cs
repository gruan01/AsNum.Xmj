using AsNum.Xmj.API;
using AsNum.Xmj.API.Entity;
using AsNum.Xmj.API.Methods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsNum.Xmj.AliSync {
    public static class ProductSync {

        public static List<SuccinctProduct> Query(string subject, ProductStatus status = ProductStatus.OnSelling, string account = "", int? group = null, int? expiryDays = 3) {
            var products = new List<SuccinctProduct>();

            if (string.IsNullOrWhiteSpace(account)) {
                var accs = AccountHelper.LoadAccounts();
                foreach (var acc in accs) {
                    products.AddRange(QueryByAccount(acc, subject, status, null, expiryDays));
                }
            } else {
                var acc = AccountHelper.LoadAccounts().FirstOrDefault(a => a.User.Equals(account, StringComparison.OrdinalIgnoreCase));
                if (acc != null)
                    products = QueryByAccount(acc, subject, status, group, expiryDays);
            }
            return products;
        }

        private static List<SuccinctProduct> QueryByAccount(Account acc, string subject, ProductStatus status = ProductStatus.OnSelling, int? group = null, int? expiryDays = 3) {
            var method = new ProductQuery() {
                Subject = subject,
                Status = status,
                GroupID = group,
                ExpireDays = expiryDays
            };
            var api = new APIClient(acc.User, acc.Pwd);
            var ps = api.Execute(method);
            return ps.Results;
        }

        public static List<ProductGroup2> QueryGroups(string account = "") {
            var groups = new List<ProductGroup2>();
            if (string.IsNullOrWhiteSpace(account)) {
                var accs = AccountHelper.LoadAccounts();
                foreach (var acc in accs) {
                    groups.AddRange(QueryGroupsByAccount(acc));
                }
            } else {
                var acc = AccountHelper.LoadAccounts().FirstOrDefault(a => a.User.Equals(account, StringComparison.OrdinalIgnoreCase));
                if (acc != null)
                    groups = QueryGroupsByAccount(acc);
            }

            return groups;
        }

        private static List<ProductGroup2> QueryGroupsByAccount(Account account) {
            var api = new APIClient(account.User, account.Pwd);
            var method = new ProductGroupList();
            return api.Execute(method);
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

        public static bool ExtendExpiryDate(string account, string productID) {
            var acc = AccountHelper.GetAccount(account);
            if (acc != null) {
                var api = new APIClient(acc.User, acc.Pwd);
                var method = new ProductEditField() {
                    ProductID = productID,
                    Field = ProductBatchEditFields.ValidDay,
                    Value = "30"
                };

                return api.Execute(method).Success;
            }
            return false;
        }
    }
}
