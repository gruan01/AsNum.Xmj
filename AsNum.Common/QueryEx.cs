using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AsNum.Common {

    public enum SortDirection { 
        Asc,
        Desc
    }

    public class QueryEx<T> where T : class {

        public Expression<Func<T , bool>> Where { get; set; }

        private List<Expression<Func<T , bool>>> wheres = null;
        public List<Expression<Func<T , bool>>> Wheres {
            get {
                if(this.wheres == null)
                    this.wheres = new List<Expression<Func<T , bool>>>();
                return this.wheres;
            }
            set {
                this.wheres = value;
            }
        }

        private Dictionary<Expression<Func<T, object>>, SortDirection> orderBys = null;
        public Dictionary<Expression<Func<T, object>>, SortDirection> OrderBys {
            get {
                if(this.orderBys == null)
                    this.orderBys = new Dictionary<Expression<Func<T, object>>, SortDirection>();
                return this.orderBys;
            }
            set {
                this.orderBys = value;
            }
        }

        private List<Expression<Func<T , object>>> includes = null;
        public List<Expression<Func<T , object>>> Includes {
            get {
                if(this.includes == null)
                    this.includes = new List<Expression<Func<T , object>>>();
                return this.includes;
            }
            set {
                this.includes = value;
            }
        }

        /// <summary>
        /// 从第多少条开始
        /// </summary>
        public int? From { get; set; }

        /// <summary>
        /// 取多少条
        /// </summary>
        public int? Count { get; set; }
    }
}
