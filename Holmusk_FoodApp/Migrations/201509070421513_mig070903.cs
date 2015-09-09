namespace Holmusk_FoodApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig070903 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodLogs", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodLogs", "UserId", c => c.Int(nullable: false));
        }
    }
}
