namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredPropsInGoods : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Goods", "Price", c => c.Decimal(precision: 10));
            AddColumn("dbo.Goods", "ImgLink", c => c.String(nullable: false));
            AddColumn("dbo.Goods", "UrlLink", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "UrlLink");
            DropColumn("dbo.Goods", "ImgLink");
            DropColumn("dbo.Goods", "Price");
            DropColumn("dbo.Goods", "Name");
        }
    }
}
