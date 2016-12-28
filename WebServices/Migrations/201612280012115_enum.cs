namespace WebServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _enum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Promotions", "Categories_CategoryId", "dbo.Categories");
            DropIndex("dbo.Promotions", new[] { "Categories_CategoryId" });
            AddColumn("dbo.Promotions", "Category", c => c.Int(nullable: false));
            DropColumn("dbo.Promotions", "Categories_CategoryId");
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Categories = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Promotions", "Categories_CategoryId", c => c.Int());
            DropColumn("dbo.Promotions", "Category");
            CreateIndex("dbo.Promotions", "Categories_CategoryId");
            AddForeignKey("dbo.Promotions", "Categories_CategoryId", "dbo.Categories", "CategoryId");
        }
    }
}
