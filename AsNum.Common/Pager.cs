
namespace AsNum.Common {
    /// <summary>
    /// 分页
    /// </summary>
    public class Pager {

        #region 分页

        private int pageSize = 20;

        /// <summary>
        /// 每页数据大小,默认20
        /// </summary>
        public int PageSize {
            get {
                return this.pageSize;
            }
            set {
                this.pageSize = value <= 0 ? 20 : value;
            }
        }


        private int page = 0;
        /// <summary>
        /// 第几页
        /// </summary>
        public int? Page {
            get {
                return this.page;
            }
            set {
                //this.page = value < 0 ? 0 : value;
                this.page = value == null ? 0 : (value.Value < 0 ? 0 : value.Value);
            }
        }

        /// <summary>
        /// 查询结果条数
        /// </summary>
        public int Count {
            get;
            set;
        }
        #endregion


        private bool allowPage = true;
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool AllowPage {
            get {
                return this.allowPage;
            }
            set {
                this.allowPage = value;
            }
        }
    }
}
