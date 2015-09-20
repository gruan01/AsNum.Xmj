namespace AsNum.Xmj.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLogisticType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FreightPartitionCountries", "CountryCode", "dbo.Countries");
            DropForeignKey("dbo.FreightPartitionCountries", "PartitionID", "dbo.FreightPartitions");
            DropIndex("dbo.FreightPartitionCountries", new[] { "PartitionID" });
            DropIndex("dbo.FreightPartitionCountries", new[] { "CountryCode" });
            DropPrimaryKey("dbo.OrdeLogistics");
            AlterColumn("dbo.OrdeLogistics", "LogisticCode", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.OrdeLogistics", new[] { "TrackNO", "LogisticCode", "OrderNO" });
            //DropColumn("dbo.OrdeLogistics", "LogisticsType");
            DropTable("dbo.FreightPartitionCountries");
            DropTable("dbo.FreightPartitions");
        }
        
        public override void Down()
        {
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
                "dbo.FreightPartitionCountries",
                c => new
                    {
                        PartitionID = c.Int(nullable: false),
                        CountryCode = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => new { t.PartitionID, t.CountryCode });
            
            AddColumn("dbo.OrdeLogistics", "LogisticsType", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.OrdeLogistics");
            AlterColumn("dbo.OrdeLogistics", "LogisticCode", c => c.String(maxLength: 20));
            AddPrimaryKey("dbo.OrdeLogistics", new[] { "TrackNO", "LogisticsType", "OrderNO" });
            CreateIndex("dbo.FreightPartitionCountries", "CountryCode");
            CreateIndex("dbo.FreightPartitionCountries", "PartitionID");
            AddForeignKey("dbo.FreightPartitionCountries", "PartitionID", "dbo.FreightPartitions", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FreightPartitionCountries", "CountryCode", "dbo.Countries", "CountryCode", cascadeDelete: true);
        }
    }
}
