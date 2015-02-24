using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsNum.Xmj.Entity {
    public class OrderDetail {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int DetailID {
        //    get;
        //    set;
        //}

        [ForeignKey("OrderOf")]
        [Key, StringLength(20, MinimumLength = 10), Required, Column(Order = 0)]
        public string OrderNO {
            get;
            set;
        }

        //子单号
        [Key, StringLength(20), Required, Column(Order = 1)]
        public string SubOrderNO {
            get;
            set;
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        [StringLength(20, MinimumLength = 5), Required]
        public string ProductID {
            get;
            set;
        }

        [StringLength(200), Required]
        public string ProductName {
            get;
            set;
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal ProductPrice {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// <remarks>
        /// 如果 Unit 是 lot ，那这里的 Qty 即指 几 lot
        /// </remarks>
        /// </summary>
        public int UnitQty {
            get;
            set;
        }

        [StringLength(20), Required]
        public string Unit {
            get;
            set;
        }

        /// <summary>
        /// 打包数量
        /// <remarks>
        /// 该值从OrderFindById 接口中取
        /// </remarks>
        /// </summary>
        public int LotNum {
            get;
            set;
        }

        /// <summary>
        /// 数量
        /// <remarks>
        /// 如果1包有10个， 2包， 这里的值就是20
        /// </remarks>
        /// </summary>
        public int Qty {
            get {
                return this.LotNum * this.UnitQty;
            }
        }

        /// <summary>
        /// 产品信息镜像链接
        /// </summary>
        [StringLength(200), Required]
        public string SnapURL {
            get;
            set;
        }

        /// <summary>
        /// 产品图片
        /// <remarks>
        /// 如果所选的颜色有图片，这里即是所选颜色的图片
        /// </remarks>
        /// </summary>
        [StringLength(200)]
        public string Image {
            get;
            set;
        }

        /// <summary>
        /// SKU码, 对应商品编码
        /// </summary>
        [StringLength(20)]
        public string SKUCode {
            get;
            set;
        }

        /// <summary>
        /// 备货期，单位 天
        /// </summary>
        public int PrepareDays {
            get;
            set;
        }

        /// <summary>
        /// 运达时间,
        /// <remarks>
        /// 值如： 15-60
        /// </remarks>
        /// </summary>
        [StringLength(20)]
        public string DeliveryTime {
            get;
            set;
        }

        /// <summary>
        /// 订单备注（客人留）
        /// </summary>
        [StringLength(1000)]
        public string Remark {
            get;
            set;
        }

        public virtual Order OrderOf {
            get;
            set;
        }

        public virtual List<OrderDetailAttribute> Attrs {
            get;
            set;
        }

        /// <summary>
        /// 运送方式
        /// </summary>
        [StringLength(20)]
        public string LogisticsType {
            get;
            set;
        }

        /// <summary>
        /// 运费
        /// </summary>
        public decimal LogisticAmount {
            get;
            set;
        }
    }
}
