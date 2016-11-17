namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedwebshoptogood : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "WebShop_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Goods", "WebShop_Id");
            AddForeignKey("dbo.Goods", "WebShop_Id", "dbo.WebShops", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goods", "WebShop_Id", "dbo.WebShops");
            DropIndex("dbo.Goods", new[] { "WebShop_Id" });
            DropColumn("dbo.Goods", "WebShop_Id");
        }
    }
}
