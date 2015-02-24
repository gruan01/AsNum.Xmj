using System;
using System.Linq.Expressions;

namespace AsNum.Common.Extends {
    public static class FuncHelper {

        public static Expression<Func<T, TResult>> ToExpression<T, TResult>(this Func<T, TResult> call) {
            return x => call(x);
        }

    }
}
