namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddonefieldtotableExecutingInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExecutingInfoes", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExecutingInfoes", "Date");
        }
    }
}
