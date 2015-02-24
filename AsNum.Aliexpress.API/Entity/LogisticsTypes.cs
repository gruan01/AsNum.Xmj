using System;
using System.ComponentModel;

namespace AsNum.Xmj.API.Entity {

    [Flags]
    public enum LogisticsTypes {

        /// <summary>
        /// 中邮小包
        /// </summary>
        [Description("中邮小包")]
        CPAM = 0x1,// 1,

        /// <summary>
        /// e邮宝
        /// </summary>
        [Description("e邮宝")]
        EMS_ZX_ZX_US = 0x2,// 2,

        /// <summary>
        /// EMS
        /// </summary>
        [Description("EMS")]
        EMS = 0x4,// 4,

        /// <summary>
        /// DHL
        /// </summary>
        [Description("DHL")]
        DHL = 0x8,// 8,

        /// <summary>
        /// 新加坡小包
        /// </summary>
        [Description("新加坡小包")]
        SGP = 0x10,// 16,

        /// <summary>
        /// 瑞典小包
        /// </summary>
        [Description("瑞典小包")]
        SEP = 0x20,// 32,

        /// <summary>
        /// 瑞士小包
        /// </summary>
        [Description("瑞士小包")]
        CHP = 0x40,// 64,

        /// <summary>
        /// 中东专线
        /// </summary>
        [Description("中东专线")]
        ARAMEX = 0x80,// 128,

        /// <summary>
        /// 香港小包
        /// </summary>
        [Description("香港小包")]
        HKPAM = 0x100,// 256,

        /// <summary>
        /// 香港大包
        /// </summary>
        [Description("香港大包")]
        HKPAP = 0x200,// 512,

        /// <summary>
        /// 中邮大包
        /// </summary>
        [Description("中邮大包")]
        CPAP = 0x400,// 1024,

        /// <summary>
        /// Fedex IP
        /// </summary>
        [Description("Fedex IP")]
        FEDEX = 0x800,// 2048,

        /// <summary>
        /// FEDEX IE
        /// </summary>
        [Description("FEDEX IE")]
        FEDEX_IE = 0x1000,// 4096,

        [Description("SPSR")]
        SPSR = 0x2000,// 8192,

        [Description("UPS Expedited")]
        UPSE = 0x4000,// 16384,

        [Description("DHL Global Mail")]
        EMS_SH_ZX_US = 0x8000,// 32768,

        /// <summary>
        /// 顺风
        /// </summary>
        [Description("顺风")]
        SF = 0x10000,// 65536,

        /// <summary>
        /// 中俄专线
        /// </summary>
        [Description("中俄专线")]
        ZTORU = 0x20000,// 131072,

        /// <summary>
        /// TNT
        /// </summary>
        [Description("TNT")]
        TNT = 0x40000, // 262144,

        /// <summary>
        /// 经济包裹
        /// </summary>
        [Description("139经济包裹")]
        ECONOMIC139 = 0x80000,// 524288,

        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other = 0x100000, // 1048576,

        [Description("TOLL")]
        TOLL = 0x200000,// 2097152,

        [Description("Russian Air")]
        CPAM_HRB = 0x400000, // 4194304,

        [Description("e-EMS")]
        E_EMS = 0x800000, // 8388608,

        [Description("UPS Express Saver")]
        UPS = 0x1000000, //16777216

        [Description("燕文平邮")]
        YANWEN_JYT = 0x2000000,// 33554432,

        [Description("燕文航空专线")]
        YANWEN_AM = 0x4000000,// 67108864,

        [Description("速优宝-Itella")]
        ITELLA = 0x8000000 //134217728
    }

}
