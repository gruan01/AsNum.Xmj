using System;
using System.ComponentModel;

namespace AsNum.Xmj.API.Entity {
    [Serializable]
    public enum ProductUnit {

        [Description("袋  bag/bags")]
        Bag = 100000000,

        [Description("桶  barrel/barrels")]
        Barrel = 100000001,

        [Description("蒲式耳  bushel/bushels")]
        Bushel = 100000002,

        [Description("箱  carton")]
        Carton = 100078580,

        [Description("厘米  centimeter")]
        Centimeter = 100078581,

        [Description("立方米  cubic meter")]
        CubicMeter = 100000003,

        [Description("打  dozen")]
        Dozen = 100000004,

        [Description("英尺  feet")]
        Feet = 100078584,

        [Description("加仑  gallon")]
        Gallon = 100000005,

        [Description("克  gram")]
        Gram = 100000006,

        [Description("英寸  inch")]
        Inch = 100078587,

        [Description("千克  kilogram")]
        Kilogram = 100000007,

        [Description("千升  kiloliter")]
        Kiloliter = 100078589,

        [Description("千米  kilometer")]
        kilometer = 100000008,
        [Description("升  liter/liters")]
        Liter = 100078559,

        [Description("英吨  long ton")]
        LongTon = 100000009,

        [Description("米  meter")]
        Meter = 100000010,

        [Description("公吨  metric ton")]
        MetricTon = 100000011,

        [Description("毫克  milligram")]
        Milligram = 100078560,

        [Description("毫升  milliliter")]
        Milliliter = 100078596,

        [Description("毫米  millimeter")]
        Millimeter = 100078597,

        [Description("盎司  ounce")]
        Ounce = 100000012,

        [Description("包  pack/packs")]
        Pack = 100000014,

        [Description("双  pair")]
        Pair = 100000013,

        [Description("件/个  piece/pieces")]
        Piece = 100000015,

        [Description("磅  pound")]
        Pound = 100000016,

        [Description("夸脱  quart")]
        Quart = 100078603,

        [Description("套  set/sets")]
        Set = 100000017,

        [Description("美吨  short ton")]
        ShortTon = 100000018,

        [Description("平方英尺  square feet")]
        SquareFeet = 100078606,

        [Description("平方英寸  square inch")]
        SquareInch = 100078607,

        [Description("平方米  square meter")]
        SquareMeter = 100000019,

        [Description("平方码  square yard")]
        SquareYard = 100078609,

        [Description("吨  ton")]
        Ton = 100000020,

        [Description("码  yard/yards")]
        Yard = 100078558

    }
}
