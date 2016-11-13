namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated_good_Category_Id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Goods", "Category_Id");
            AddForeignKey("dbo.Goods", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            DropColumn("dbo.Goods", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "Category", c => c.Int(nullable: false));
            DropForeignKey("dbo.Goods", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Goods", new[] { "Category_Id" });
            DropColumn("dbo.Goods", "Category_Id");
        }
    }
}
