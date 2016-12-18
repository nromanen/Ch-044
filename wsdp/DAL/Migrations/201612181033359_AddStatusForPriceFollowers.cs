namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusForPriceFollowers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceFollowers", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceFollowers", "Status");
        }
    }
}
