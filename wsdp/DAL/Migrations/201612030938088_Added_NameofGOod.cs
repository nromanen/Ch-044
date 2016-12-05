namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_NameofGOod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceHistories", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceHistories", "Name");
        }
    }
}
