using System.ComponentModel.DataAnnotations;

namespace AsNum.Xmj.Entity {
    /// <summary>
    /// 产品属性分类
    /// </summary>
    public class PPCatelog {

        public int ID {
            get;
            set;
        }

        public int ParentID {
            get;
            set;
        }

        [StringLength(20), Required]
        public string Name {
            get;
            set;
        }

        public bool IsLeaf {
            get;
            set;
        }

        //public virtual ICollection<PProductCatelogMap> Products {
        //    get;
        //    set;
        //}

    }
}
