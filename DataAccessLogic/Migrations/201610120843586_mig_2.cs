namespace DataAccessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Goods", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Goods", "Producer_Id", "dbo.Producers");
            DropIndex("dbo.Goods", new[] { "Producer_Id" });
            DropIndex("dbo.Goods", new[] { "Category_Id" });
            AlterColumn("dbo.Goods", "Producer_Id", c => c.Int());
            AlterColumn("dbo.Goods", "Category_Id", c => c.Int());
            CreateIndex("dbo.Goods", "Category_Id");
            CreateIndex("dbo.Goods", "Producer_Id");
            AddForeignKey("dbo.Goods", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Goods", "Producer_Id", "dbo.Producers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goods", "Producer_Id", "dbo.Producers");
            DropForeignKey("dbo.Goods", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Goods", new[] { "Producer_Id" });
            DropIndex("dbo.Goods", new[] { "Category_Id" });
            AlterColumn("dbo.Goods", "Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Goods", "Producer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Goods", "Category_Id");
            CreateIndex("dbo.Goods", "Producer_Id");
            AddForeignKey("dbo.Goods", "Producer_Id", "dbo.Producers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Goods", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
