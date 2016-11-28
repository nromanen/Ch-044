namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OldPrice_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "OldPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "OldPrice");
        }
    }
}
