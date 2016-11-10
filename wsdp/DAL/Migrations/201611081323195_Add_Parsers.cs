namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Add_Parsers : DbMigration
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
        }

        public override void Down()
        {
            DropForeignKey("dbo.Parsers", "WebShopId", "dbo.WebShops");
            DropForeignKey("dbo.Parsers", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Parsers", new[] { "WebShopId" });
            DropIndex("dbo.Parsers", new[] { "CategoryId" });
            DropTable("dbo.Parsers");
        }
    }
}