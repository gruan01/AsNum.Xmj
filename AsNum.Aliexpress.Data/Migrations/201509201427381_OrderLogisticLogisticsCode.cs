namespace AsNum.Xmj.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderLogisticLogisticsCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdeLogistics", "LogisticCode", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdeLogistics", "LogisticCode");
        }
    }
}
