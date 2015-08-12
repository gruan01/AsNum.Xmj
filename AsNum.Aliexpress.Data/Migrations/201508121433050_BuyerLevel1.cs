namespace AsNum.Xmj.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyerLevel1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuyerLevels",
                c => new
                    {
                        BuyerID = c.String(nullable: false, maxLength: 20),
                        Level = c.String(maxLength: 10),
                        UpdateOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BuyerID)
                .ForeignKey("dbo.Buyers", t => t.BuyerID)
                .Index(t => t.BuyerID);
            
            DropColumn("dbo.Buyers", "Level");
            DropColumn("dbo.Buyers", "LevelUpdateOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Buyers", "LevelUpdateOn", c => c.DateTime());
            AddColumn("dbo.Buyers", "Level", c => c.String(maxLength: 5));
            DropForeignKey("dbo.BuyerLevels", "BuyerID", "dbo.Buyers");
            DropIndex("dbo.BuyerLevels", new[] { "BuyerID" });
            DropTable("dbo.BuyerLevels");
        }
    }
}
