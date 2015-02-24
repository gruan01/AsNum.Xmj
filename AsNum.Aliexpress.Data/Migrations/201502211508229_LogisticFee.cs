namespace AsNum.Xmj.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class LogisticFee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogisticFees",
                c => new
                    {
                        TrackNO = c.String(nullable: false, maxLength: 20),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TrackNO);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogisticFees");
        }
    }
}
