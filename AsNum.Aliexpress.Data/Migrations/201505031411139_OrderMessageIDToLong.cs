namespace AsNum.Xmj.Data.Migrations {
    using System;
    using System.Data.Entity.Migrations;

    public partial class OrderMessageIDToLong : DbMigration {
        public override void Up() {
            DropPrimaryKey("dbo.OrderMessages");
            AlterColumn("dbo.OrderMessages", "ID", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.OrderMessages", "ID");
        }

        public override void Down() {
            DropPrimaryKey("dbo.OrderMessages");
            AlterColumn("dbo.OrderMessages", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrderMessages", "ID");
        }
    }
}
