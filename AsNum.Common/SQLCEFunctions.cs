using System;
using System.Data.Objects.DataClasses;

namespace AsNum.Common {
    public static class SQLCEFunctions {
        [EdmFunction("SqlServerCe", "DATEADD")]
        public static DateTime DateAdd(string datePart, double amount, DateTime date) {
            throw new InvalidOperationException("Not to be called from client code");
        }

        [EdmFunction("SqlServerCe", "DATEDIFF")]
        public static int? DateDiff(string datePart, DateTime startDate, DateTime endDate) {
            throw new InvalidOperationException("Not to be called from client code");
        }

        [EdmFunction("SqlServerCe", "DatePart")]
        public static string DatePart(string datePart, DateTime? date) {
            throw new InvalidOperationException("Not to be called from client code");
        }
    }
}
