namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddWebShopStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebShops", "Status", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.WebShops", "Status");
        }
    }
}