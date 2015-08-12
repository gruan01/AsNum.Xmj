namespace AsNum.Xmj.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyerLevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Buyers", "Level", c => c.String(maxLength: 5));
            AddColumn("dbo.Buyers", "LevelUpdateOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Buyers", "LevelUpdateOn");
            DropColumn("dbo.Buyers", "Level");
        }
    }
}
