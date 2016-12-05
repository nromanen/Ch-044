namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed_pricehistory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PriceHistories", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PriceHistories", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PriceHistories", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.PriceHistories", "Date", c => c.Int(nullable: false));
        }
    }
}
