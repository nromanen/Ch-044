namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_migration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Parsers", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Parsers", "WebShopId", "dbo.WebShops");
            DropIndex("dbo.Parsers", new[] { "CategoryId" });
            DropIndex("dbo.Parsers", new[] { "WebShopId" });
            DropTable("dbo.Parsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Parsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CategoryId = c.Int(nullable: false),
                        WebShopId = c.Int(nullable: false),
                        Priority = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IteratorSettings = c.String(nullable: false),
                        GrabberSettings = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Parsers", "WebShopId");
            CreateIndex("dbo.Parsers", "CategoryId");
            AddForeignKey("dbo.Parsers", "WebShopId", "dbo.WebShops", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Parsers", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
