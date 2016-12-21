namespace WebServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        PromotionId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        VendorId = c.Int(nullable: false),
                        PromotionStartDate = c.DateTime(),
                        PromotionEndDate = c.DateTime(),
                        PromotionActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PromotionId)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Categories = c.Int(),
                        Promotion_PromotionId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Promotions", t => t.Promotion_PromotionId)
                .Index(t => t.Promotion_PromotionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        IsVendor = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CompanyName = c.String(),
                        Salt = c.String(),
                        Address_Country = c.String(),
                        Address_Province = c.String(),
                        Address_City = c.String(),
                        Address_PostalCode = c.String(),
                        Address_Street = c.String(),
                        Address_StreetNumber = c.String(),
                        Telephone_PersonalPhoneNumber = c.String(),
                        Telephone_BusinessPhoneNumber = c.String(),
                        Location_Lat = c.Double(),
                        Location_Lng = c.Double(),
                        IsVendor = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Promotions", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Categories", "Promotion_PromotionId", "dbo.Promotions");
            DropIndex("dbo.Categories", new[] { "Promotion_PromotionId" });
            DropIndex("dbo.Promotions", new[] { "VendorId" });
            DropTable("dbo.Vendors");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.Promotions");
        }
    }
}
