namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_PriceFollow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceFollowers", "Good_Id", c => c.Int(nullable: false));
            AddColumn("dbo.PriceFollowers", "User_Id", c => c.Int(nullable: false));
            DropColumn("dbo.PriceFollowers", "Url");
            DropColumn("dbo.PriceFollowers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceFollowers", "Email", c => c.String());
            AddColumn("dbo.PriceFollowers", "Url", c => c.String());
            DropColumn("dbo.PriceFollowers", "User_Id");
            DropColumn("dbo.PriceFollowers", "Good_Id");
        }
    }
}
