namespace AsNum.Xmj.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogisticServiceCheckRegexLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LogisticServices", "CheckRegex", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LogisticServices", "CheckRegex", c => c.String(maxLength: 50));
        }
    }
}
