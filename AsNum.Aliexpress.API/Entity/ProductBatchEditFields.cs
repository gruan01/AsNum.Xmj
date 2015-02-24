using AsNum.Xmj.API.Attributes;
using System.ComponentModel;

namespace AsNum.Xmj.API.Entity {
    /// <summary>
    /// 批量修可修改的字段
    /// <remarks>
    /// api.editSimpleProductFiled
    /// </remarks>
    /// </summary>
    public enum ProductBatchEditFields {

        [Description("备货期")]
        [SpecifyValue("deliveryTime")]
        DeliveryTime,

        [Description("一口价"), SpecifyValue("productPrice")]
        ProductPrice,

        [Description("运费模板"), SpecifyValue("freightTemplateId")]
        FreightTemplateID,

        [Description("包裹长度"), SpecifyValue("packageLength")]
        PackageLength,

        [Description("包裹宽度"), SpecifyValue("packageWidth")]
        PackageWidth,

        [Description("包裹高度"), SpecifyValue("packageHeight")]
        PackageHeight,

        [Description("毛重"), SpecifyValue("grossWeight")]
        GrossWeight,

        [Description("商品有效天数"), SpecifyValue("wsValidNum")]
        ValidDay,

        [Description("批发最小数量"), SpecifyValue("bulkOrder")]
        MOQ,

        [Description("批发折扣"), SpecifyValue("bulkDiscount")]
        BulkDiscount,

        [Description("产品标题"), SpecifyValue("subject")]
        Subject,

        [Description("详情"), SpecifyValue("detail")]
        Detail
    }
}
