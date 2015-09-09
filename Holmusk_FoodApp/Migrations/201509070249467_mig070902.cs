namespace Holmusk_FoodApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig070902 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        Foodid = c.Int(nullable: false, identity: true),
                        FoodName = c.String(),
                        Description = c.String(),
                        Unit = c.Int(nullable: false),
                        Cholesterol = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vitamins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Energy = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sugar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Protein = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Calcium = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Foodid);
            
            CreateTable(
                "dbo.FoodLogs",
                c => new
                    {
                        FoodLogId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        LogDate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FoodLogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FoodLogs");
            DropTable("dbo.Foods");
        }
    }
}
