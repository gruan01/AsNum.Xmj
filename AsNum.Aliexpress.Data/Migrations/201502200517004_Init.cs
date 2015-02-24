namespace AsNum.Xmj.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Buyers",
                c => new
                    {
                        BuyerID = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(maxLength: 50),
                        CountryCode = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.BuyerID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryCode = c.String(nullable: false, maxLength: 5),
                        EnName = c.String(nullable: false, maxLength: 50),
                        ZhName = c.String(nullable: false, maxLength: 30),
                        PhoneCode = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.CountryCode);
            
            CreateTable(
                "dbo.EUBShippers",
                c => new
                    {
                        Flag = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 30),
                        Phone = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(nullable: false, maxLength: 6),
                        Province = c.String(nullable: false, maxLength: 30),
                        Street = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        County = c.String(nullable: false, maxLength: 50),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Flag);
            
            CreateTable(
                "dbo.FreightPartitionCountries",
                c => new
                    {
                        PartitionID = c.Int(nullable: false),
                        CountryCode = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => new { t.PartitionID, t.CountryCode })
                .ForeignKey("dbo.Countries", t => t.CountryCode, cascadeDelete: true)
                .ForeignKey("dbo.FreightPartitions", t => t.PartitionID, cascadeDelete: true)
                .Index(t => t.CountryCode)
                .Index(t => t.PartitionID);
            
            CreateTable(
                "dbo.FreightPartitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LogisticTypes = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        SubOrderNO = c.String(nullable: false, maxLength: 20),
                        ProductID = c.String(nullable: false, maxLength: 20),
                        ProductName = c.String(nullable: false, maxLength: 200),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitQty = c.Int(nullable: false),
                        Unit = c.String(nullable: false, maxLength: 20),
                        LotNum = c.Int(nullable: false),
                        SnapURL = c.String(nullable: false, maxLength: 200),
                        Image = c.String(maxLength: 200),
                        SKUCode = c.String(maxLength: 20),
                        PrepareDays = c.Int(nullable: false),
                        DeliveryTime = c.String(maxLength: 20),
                        Remark = c.String(maxLength: 1000),
                        LogisticsType = c.String(maxLength: 20),
                        LogisticAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderNO, t.SubOrderNO })
                .ForeignKey("dbo.Orders", t => t.OrderNO, cascadeDelete: true)
                .Index(t => t.OrderNO);
            
            CreateTable(
                "dbo.OrderDetailAttributes",
                c => new
                    {
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        SubOrderNO = c.String(nullable: false, maxLength: 20),
                        Order = c.Int(nullable: false),
                        AttrStr = c.String(maxLength: 100),
                        CompatibleStr = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => new { t.OrderNO, t.SubOrderNO, t.Order })
                .ForeignKey("dbo.OrderDetails", t => new { t.OrderNO, t.SubOrderNO }, cascadeDelete: true)
                .Index(t => new { t.OrderNO, t.SubOrderNO });
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        Status = c.Byte(nullable: false),
                        BuyerID = c.String(nullable: false, maxLength: 20),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.String(nullable: false, maxLength: 3),
                        PaymentType = c.String(maxLength: 10),
                        CreteOn = c.DateTime(nullable: false),
                        PaymentOn = c.DateTime(),
                        Account = c.String(maxLength: 20),
                        OffTime = c.DateTime(nullable: false),
                        InIssue = c.Boolean(nullable: false),
                        IsShamShipping = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderNO)
                .ForeignKey("dbo.Owners", t => t.Account)
                .ForeignKey("dbo.Buyers", t => t.BuyerID, cascadeDelete: true)
                .Index(t => t.Account)
                .Index(t => t.BuyerID);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Account = c.String(nullable: false, maxLength: 20),
                        QuickCode = c.String(maxLength: 3),
                        AccountType = c.Byte(nullable: false),
                        SenderCity = c.String(maxLength: 30),
                        SenderFax = c.String(maxLength: 30),
                        SenderMobi = c.String(maxLength: 30),
                        SenderPhone = c.String(maxLength: 30),
                        SenderName = c.String(maxLength: 30),
                        SenderProvince = c.String(maxLength: 30),
                        SenderAddress = c.String(maxLength: 90),
                        SenderPostCode = c.String(maxLength: 10),
                        PickupCity = c.String(maxLength: 30),
                        PickupFax = c.String(maxLength: 30),
                        PickupMobi = c.String(maxLength: 30),
                        PickupPhone = c.String(maxLength: 30),
                        PickupName = c.String(maxLength: 30),
                        PickupProvince = c.String(maxLength: 30),
                        PickupCounty = c.String(),
                        PickupAddress = c.String(maxLength: 90),
                        PickupPostCode = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Account);
            
            CreateTable(
                "dbo.AdjReceivers",
                c => new
                    {
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 200),
                        CountryCode = c.String(nullable: false, maxLength: 5),
                        City = c.String(nullable: false, maxLength: 100),
                        Province = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 100),
                        PhoneArea = c.String(maxLength: 50),
                        Mobi = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.OrderNO)
                .ForeignKey("dbo.Countries", t => t.CountryCode, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderNO)
                .Index(t => t.CountryCode)
                .Index(t => t.OrderNO);
            
            CreateTable(
                "dbo.OrdeLogistics",
                c => new
                    {
                        TrackNO = c.String(nullable: false, maxLength: 20),
                        LogisticsType = c.Int(nullable: false),
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        SendOn = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.TrackNO, t.LogisticsType, t.OrderNO })
                .ForeignKey("dbo.Orders", t => t.OrderNO, cascadeDelete: true)
                .Index(t => t.OrderNO);
            
            CreateTable(
                "dbo.OrderMessages",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        Content = c.String(maxLength: 2000),
                        CreateOn = c.DateTime(nullable: false),
                        Sender = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderNO, cascadeDelete: true)
                .Index(t => t.OrderNO);
            
            CreateTable(
                "dbo.OrderNotes",
                c => new
                    {
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        Note = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.OrderNO)
                .ForeignKey("dbo.Orders", t => t.OrderNO)
                .Index(t => t.OrderNO);
            
            CreateTable(
                "dbo.Receivers",
                c => new
                    {
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 200),
                        CountryCode = c.String(nullable: false, maxLength: 5),
                        City = c.String(nullable: false, maxLength: 100),
                        Province = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(maxLength: 100),
                        PhoneArea = c.String(maxLength: 50),
                        Mobi = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.OrderNO)
                .ForeignKey("dbo.Countries", t => t.CountryCode, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderNO)
                .Index(t => t.CountryCode)
                .Index(t => t.OrderNO);
            
            CreateTable(
                "dbo.PurchaseDetails",
                c => new
                    {
                        OrderNO = c.String(nullable: false, maxLength: 20),
                        Completed = c.Boolean(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.OrderNO)
                .ForeignKey("dbo.Orders", t => t.OrderNO)
                .Index(t => t.OrderNO);
            
            CreateTable(
                "dbo.PPCatelogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        IsLeaf = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderNO", "dbo.Orders");
            DropForeignKey("dbo.PurchaseDetails", "OrderNO", "dbo.Orders");
            DropForeignKey("dbo.Receivers", "OrderNO", "dbo.Orders");
            DropForeignKey("dbo.Receivers", "CountryCode", "dbo.Countries");
            DropForeignKey("dbo.OrderNotes", "OrderNO", "dbo.Orders");
            DropForeignKey("dbo.OrderMessages", "OrderNO", "dbo.Orders");
            DropForeignKey("dbo.OrdeLogistics", "OrderNO", "dbo.Orders");
            DropForeignKey("dbo.Orders", "BuyerID", "dbo.Buyers");
            DropForeignKey("dbo.AdjReceivers", "OrderNO", "dbo.Orders");
            DropForeignKey("dbo.AdjReceivers", "CountryCode", "dbo.Countries");
            DropForeignKey("dbo.Orders", "Account", "dbo.Owners");
            DropForeignKey("dbo.OrderDetailAttributes", new[] { "OrderNO", "SubOrderNO" }, "dbo.OrderDetails");
            DropForeignKey("dbo.FreightPartitionCountries", "PartitionID", "dbo.FreightPartitions");
            DropForeignKey("dbo.FreightPartitionCountries", "CountryCode", "dbo.Countries");
            DropIndex("dbo.OrderDetails", new[] { "OrderNO" });
            DropIndex("dbo.PurchaseDetails", new[] { "OrderNO" });
            DropIndex("dbo.Receivers", new[] { "OrderNO" });
            DropIndex("dbo.Receivers", new[] { "CountryCode" });
            DropIndex("dbo.OrderNotes", new[] { "OrderNO" });
            DropIndex("dbo.OrderMessages", new[] { "OrderNO" });
            DropIndex("dbo.OrdeLogistics", new[] { "OrderNO" });
            DropIndex("dbo.Orders", new[] { "BuyerID" });
            DropIndex("dbo.AdjReceivers", new[] { "OrderNO" });
            DropIndex("dbo.AdjReceivers", new[] { "CountryCode" });
            DropIndex("dbo.Orders", new[] { "Account" });
            DropIndex("dbo.OrderDetailAttributes", new[] { "OrderNO", "SubOrderNO" });
            DropIndex("dbo.FreightPartitionCountries", new[] { "PartitionID" });
            DropIndex("dbo.FreightPartitionCountries", new[] { "CountryCode" });
            DropTable("dbo.PPCatelogs");
            DropTable("dbo.PurchaseDetails");
            DropTable("dbo.Receivers");
            DropTable("dbo.OrderNotes");
            DropTable("dbo.OrderMessages");
            DropTable("dbo.OrdeLogistics");
            DropTable("dbo.AdjReceivers");
            DropTable("dbo.Owners");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetailAttributes");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.FreightPartitions");
            DropTable("dbo.FreightPartitionCountries");
            DropTable("dbo.EUBShippers");
            DropTable("dbo.Countries");
            DropTable("dbo.Buyers");
        }
    }
}
