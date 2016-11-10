namespace DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WebShop20 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WebShops", "Name", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.WebShops", "Name", c => c.Int(nullable: false));
        }
    }
}