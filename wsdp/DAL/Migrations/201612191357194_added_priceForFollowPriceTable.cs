namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_priceForFollowPriceTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceFollowers", "Price", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceFollowers", "Price");
        }
    }
}
