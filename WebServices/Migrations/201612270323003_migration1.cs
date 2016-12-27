namespace WebServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        PromotionStartDate = c.DateTime(nullable: false),
                        PromotionEndDate = c.DateTime(nullable: false),
                        PromotionImage = c.Binary(),
                        Categories_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.PromotionId)
                .ForeignKey("dbo.Categories", t => t.Categories_CategoryId)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.Categories_CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Categories = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 20),
                        Password = c.String(),
                        CompanyName = c.String(),
                        Salt = c.String(),
                        Address = c.String(),
                        Telephone = c.String(),
                        Location_Lat = c.Double(nullable: false),
                        Location_Lng = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VendorId)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 20),
                        Password = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotions", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Promotions", "Categories_CategoryId", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Vendors", new[] { "Email" });
            DropIndex("dbo.Promotions", new[] { "Categories_CategoryId" });
            DropIndex("dbo.Promotions", new[] { "VendorId" });
            DropTable("dbo.Users");
            DropTable("dbo.Vendors");
            DropTable("dbo.Categories");
            DropTable("dbo.Promotions");
        }
    }
}
