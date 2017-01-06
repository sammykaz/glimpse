namespace WebServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makingpromowork : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Promotions");
            AddPrimaryKey("dbo.Promotions", new[] { "PromotionId", "VendorId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Promotions");
            AddPrimaryKey("dbo.Promotions", "PromotionId");
        }
    }
}
