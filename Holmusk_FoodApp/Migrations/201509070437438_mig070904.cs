namespace Holmusk_FoodApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig070904 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodLogs", "LogDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodLogs", "LogDate", c => c.Int(nullable: false));
        }
    }
}
