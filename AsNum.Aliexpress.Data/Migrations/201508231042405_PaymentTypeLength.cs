namespace AsNum.Xmj.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentTypeLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PaymentType", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentType", c => c.String(maxLength: 10));
        }
    }
}
