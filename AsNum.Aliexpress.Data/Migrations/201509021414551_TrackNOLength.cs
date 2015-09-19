namespace AsNum.Xmj.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrackNOLength : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrdeLogistics");
            AlterColumn("dbo.OrdeLogistics", "TrackNO", c => c.String(nullable: false, maxLength: 30));
            AddPrimaryKey("dbo.OrdeLogistics", new[] { "TrackNO", "LogisticsType", "OrderNO" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OrdeLogistics");
            AlterColumn("dbo.OrdeLogistics", "TrackNO", c => c.String(nullable: false, maxLength: 20));
            AddPrimaryKey("dbo.OrdeLogistics", new[] { "TrackNO", "LogisticsType", "OrderNO" });
        }
    }
}
