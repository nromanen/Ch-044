namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableParser : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.WebShops", t => t.WebShopId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.WebShopId);
            
            DropColumn("dbo.Properties", "Characteristic_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Properties", "Characteristic_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Parsers", "WebShopId", "dbo.WebShops");
            DropForeignKey("dbo.Parsers", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Parsers", new[] { "WebShopId" });
            DropIndex("dbo.Parsers", new[] { "CategoryId" });
            DropTable("dbo.Parsers");
        }
    }
}
