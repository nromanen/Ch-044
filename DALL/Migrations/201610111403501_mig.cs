namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_Id = c.Int(),
                        Producer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Producers", t => t.Producer_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Producer_Id);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goods", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Goods", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Goods", new[] { "Producer_Id" });
            DropIndex("dbo.Goods", new[] { "Category_Id" });
            DropTable("dbo.Producers");
            DropTable("dbo.Goods");
            DropTable("dbo.Categories");
        }
    }
}
