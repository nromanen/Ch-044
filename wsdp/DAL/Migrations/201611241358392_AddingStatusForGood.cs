namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingStatusForGood : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "Status");
        }
    }
}
