using AsNum.Common.Attributes;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace AsNum.Common {
    [Serializable]
    public enum OrderDirection {
        Asc,
        Desc
    }

    [Serializable]
    public class BaseQuery {

        private Pager pager = null;
        public Pager Pager {
            get {
                if (this.pager == null)
                    this.pager = new Pager();
                return this.pager;
            }
            set {
                this.pager = value;
            }
        }
    }

    [Serializable]
    public class BaseQuery<T> : BaseQuery where T : class {
        public IQueryable<T> Filter(IQueryable<T> source) {
            var ps = this.GetType()
                .GetProperties()
                .ToList();

            ps.ForEach(p => {
                var mapTo = p.GetCustomAttribute<MapToAttribute>();
                if (mapTo != null) {
                    var st = typeof(T).GetProperty(mapTo.Field);
                    if (st != null) {
                        var opt = string.Empty;
                        switch (mapTo.Opt) {
                            case MapToOpts.Equal:
                                opt = "==";
                                break;
                            case MapToOpts.NotEqual:
                                opt = "!=";
                                break;
                            case MapToOpts.Gt:
                                opt = ">";
                                break;
                            case MapToOpts.Lt:
                                opt = "<";
                                break;
                            case MapToOpts.GtOrEqual:
                                opt = ">=";
                                break;
                            case MapToOpts.LtOrEqual:
                                opt = "<=";
                                break;
                        }

                        if (!string.IsNullOrEmpty(opt)) {
                            var v = p.GetValue(this);
                            var isString = p.PropertyType == typeof(string);
                            if ((v != null && !isString) || (isString && !string.IsNullOrWhiteSpace((string)v))) {
                                string cond = "";
                                if (v.GetType() == typeof(string) && mapTo.Opt == MapToOpts.Equal && mapTo.IgnoreCase) {
                                    cond = string.Format("{0}.ToUpper() {1} @0", st.Name, opt);
                                    source = source.Where(cond, ((string)v).ToUpper());
                                } else {
                                    cond = string.Format("{0} {1} @0", st.Name, opt);
                                    source = source.Where(cond, v);
                                }


                            }
                        } else {
                            var cond = string.Empty;

                            var v = (string)p.GetValue(this);
                            if (v == null || string.IsNullOrEmpty((string)v))
                                return;

                            v = v.Replace("\"", "");

                            if (string.IsNullOrEmpty(v))
                                return;

                            var ignoreCaseStr = mapTo.IgnoreCase ? ".ToUpper()" : "";
                            if (mapTo.IgnoreCase)
                                v = v.ToUpper();

                            switch (mapTo.Opt) {
                                case MapToOpts.Include:
                                    cond = string.Format("{0}{1}.IndexOf(\"{2}\") != -1", st.Name, ignoreCaseStr, v);
                                    break;
                                case MapToOpts.StartWith:
                                    cond = string.Format("{0}{1}.StartsWith(\"{2}\")", st.Name, ignoreCaseStr, v);
                                    break;
                                case MapToOpts.EndWith:
                                    cond = string.Format("{0}{1}.EndsWith(\"{2}\")", st.Name, ignoreCaseStr, v);
                                    break;
                            }

                            if (!string.IsNullOrEmpty(cond)) {
                                source = source.Where(cond);
                            }

                        }
                    }
                }
            });
            return source;
        }


    }
}
