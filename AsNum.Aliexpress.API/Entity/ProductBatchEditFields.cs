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
        [SpecifyNameValue(Name = "deliveryTime")]
        DeliveryTime,

        [Description("一口价"), SpecifyNameValue(Name = "productPrice")]
        ProductPrice,

        [Description("运费模板"), SpecifyNameValue(Name = "freightTemplateId")]
        FreightTemplateID,

        [Description("包裹长度"), SpecifyNameValue(Name = "packageLength")]
        PackageLength,

        [Description("包裹宽度"), SpecifyNameValue(Name = "packageWidth")]
        PackageWidth,

        [Description("包裹高度"), SpecifyNameValue(Name = "packageHeight")]
        PackageHeight,

        [Description("毛重"), SpecifyNameValue(Name = "grossWeight")]
        GrossWeight,

        [Description("商品有效天数"), SpecifyNameValue(Name = "wsValidNum")]
        ValidDay,

        [Description("批发最小数量"), SpecifyNameValue(Name = "bulkOrder")]
        MOQ,

        [Description("批发折扣"), SpecifyNameValue(Name = "bulkDiscount")]
        BulkDiscount,

        [Description("产品标题"), SpecifyNameValue(Name = "subject")]
        Subject,

        [Description("详情"), SpecifyNameValue(Name = "detail")]
        Detail
    }
}
