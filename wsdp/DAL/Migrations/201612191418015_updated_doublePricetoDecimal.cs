namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_doublePricetoDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PriceFollowers", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PriceFollowers", "Price", c => c.Double());
        }
    }
}
