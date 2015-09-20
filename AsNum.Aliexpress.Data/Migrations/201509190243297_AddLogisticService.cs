namespace AsNum.Xmj.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogisticService : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogisticServices",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 20),
                        NameEn = c.String(maxLength: 50),
                        NameCn = c.String(maxLength: 50),
                        Company = c.String(maxLength: 30),
                        MiniProcessDays = c.Int(nullable: false),
                        MaxProcessDays = c.Int(nullable: false),
                        CheckRegex = c.String(maxLength: 50),
                        Order = c.Int(nullable: false),
                        IsUsual = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogisticServices");
        }
    }
}
