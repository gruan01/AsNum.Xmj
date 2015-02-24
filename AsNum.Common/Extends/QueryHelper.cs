using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace AsNum.Common.Extends {
    public static class QueryHelper {

        //public static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(
        //    Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values) {
        //    if(null == valueSelector) { throw new ArgumentNullException("valueSelector"); }
        //    if(null == values) { throw new ArgumentNullException("values"); }
        //    ParameterExpression p = valueSelector.Parameters.Single();

        //    // p => valueSelector(p) == values[0] || valueSelector(p) == ...
        //    if(!values.Any()) {
        //        return e => false;
        //    }

        //    var equals = values.Select(
        //             value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));

        //    var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

        //    return Expression.Lambda<Func<TElement, bool>>(body, p);
        //}

        public static IQueryable<TEntity> WhereIn<TEntity, TValue>
          (
            this IQueryable<TEntity> query,
            Expression<Func<TEntity, TValue>> selector,
            IEnumerable<TValue> collection
          ) {
            if(selector == null)
                throw new ArgumentNullException("selector");
            if(collection == null)
                throw new ArgumentNullException("collection");
            if(!collection.Any())
                return query.Where(t => false);

            ParameterExpression p = selector.Parameters.Single();

            IEnumerable<Expression> equals = collection.Select(value =>
               (Expression)Expression.Equal(selector.Body,
                    Expression.Constant(value, typeof(TValue))));

            Expression body = equals.Aggregate((accumulate, equal) =>
                Expression.Or(accumulate, equal));

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
        }

        //Optional - to allow static collection:
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>
          (
            this ObjectQuery<TEntity> query,
            Expression<Func<TEntity, TValue>> selector,
            params TValue[] collection
          ) {
            return WhereIn(query, selector, (IEnumerable<TValue>)collection);
        }

    }
}
