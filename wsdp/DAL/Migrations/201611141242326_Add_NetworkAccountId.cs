namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NetworkAccountId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Network", c => c.String());
            AddColumn("dbo.Users", "NetworkAccountId", c => c.String());
            DropColumn("dbo.Users", "SocialNetwork");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "SocialNetwork", c => c.String());
            DropColumn("dbo.Users", "NetworkAccountId");
            DropColumn("dbo.Users", "Network");
        }
    }
}
